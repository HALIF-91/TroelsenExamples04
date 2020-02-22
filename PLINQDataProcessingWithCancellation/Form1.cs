using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLINQDataProcessingWithCancellation
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cancelToken = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Thread is {0}", Thread.CurrentThread.ManagedThreadId);
            // Запустить новую задачу для обработки целых чисел
            Task.Factory.StartNew(() => 
            {
                Console.WriteLine("Thread is {0}", Thread.CurrentThread.ManagedThreadId);
                ProcessIntData();
            });
        }

        private void ProcessIntData()
        {
            // Получить очень большой массив целых чисел
            int[] source = Enumerable.Range(1, 10000000).ToArray();

            // Найти числа для кторых истинно условие num % 3 == 0 и возвратить их
            // в порядке убывания, воспользоваться расширяющим методом AsParallel()
            int[] modThreeIsZero = null;
            try
            {
                modThreeIsZero = (from num
                                  in source.AsParallel().WithCancellation(cancelToken.Token)
                                  where num % 3 == 0
                                  orderby num descending
                                  select num).ToArray();

                MessageBox.Show(string.Format("Found {0} numbers that match query!", modThreeIsZero.Count()));
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
            cancelToken.Cancel();
        }
    }
}
