using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataParallelismWithForEach
{
    public partial class MainFrom : Form
    {
        // Новая переменная уровня Form
        private CancellationTokenSource cancelToken = new CancellationTokenSource();
        public MainFrom()
        {
            InitializeComponent();
        }

        private void btnProcessImages_Click(object sender, EventArgs e)
        {
            // Запустить новую "задачу" для обработки файлов
            Task.Factory.StartNew(() =>
            {
                ProcessFiles();
            });
        }

        private void ProcessFiles()
        {
            // Использовать экземпляр ParallelOptions для хранения CancellationToken
            ParallelOptions parOpts = new ParallelOptions();
            parOpts.CancellationToken = cancelToken.Token;
            parOpts.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

            // Загрузить все файлы *.jpg и создать новую папку для модифицированных данных
            string[] files = Directory.GetFiles(@"D:\CONTENT\Oboi_full_HD_1920_1080_[torrents.ru]", "*.jpg", SearchOption.AllDirectories);
            string newDir = @"C:\ModifiedPictures";
            Directory.CreateDirectory(newDir);

            try
            {
                // Обработать данные изображений в параллельном режиме
                Parallel.ForEach(files, parOpts, currentFile =>
                {
                    parOpts.CancellationToken.ThrowIfCancellationRequested();

                    string filename = Path.GetFileName(currentFile);

                    using (Bitmap bitmap = new Bitmap(currentFile))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, filename));

                        // Вывести идентификатор потока, обрабатывающего текущее изображение
                        // Вызвать Invoke на объекте Form, чтобы позволить вторичным потокам
                        // получать доступ к элементам управления в безопасной к потокам манере
                        this.Invoke((Action)delegate
                        {
                            this.Text = string.Format("Processing {0} on thread {1}", filename, Thread.CurrentThread.ManagedThreadId);
                        });

                        // Так как Invoke запускается в первичном потоке, номер потока будет всегда только 1
                        // Реально используемые потоки можно увидеть с помощью консоли
                        Console.WriteLine("Processing {0} on thread {1}", filename, Thread.CurrentThread.ManagedThreadId);
                    }
                });
            }
            catch (OperationCanceledException ex)
            {
                this.Invoke((Action)delegate
                {
                    this.Text = ex.Message;
                });
            } 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Используется для сообщения всем рабочим потокам о необходимости остановки
            cancelToken.Cancel();
        }
    }
}
