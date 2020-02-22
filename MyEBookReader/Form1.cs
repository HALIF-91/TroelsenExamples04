using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;

namespace MyEBookReader
{
    public partial class Form1 : Form
    {
        private string theEBook;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += (s, eArgs) =>
            {
                theEBook = eArgs.Result;
                txtBook.Text = theEBook;
                txtBook.ScrollBars = ScrollBars.Vertical;
            };

            // Загрузить электронную книгу "A Tale of Two Cities" Чарльза Диккенса
            wc.DownloadStringAsync(new Uri("http://www.gutenberg.org/files/98/98-8.txt"));
        }

        private void btnGetStats_Click(object sender, EventArgs e)
        {
            // Получить слова из электронной книги
            string[] words = theEBook.Split(new char[] { ' ', '\u000A', ',', '.', ';', ':', '-', '?', '/' }, StringSplitOptions.RemoveEmptyEntries);

            string[] tenMostCommon = null;

            string longestWord = string.Empty;

            Parallel.Invoke(
                (Action)delegate 
            {
                // Найти 10 наиболее часто встречающихся слов
                tenMostCommon = FindTenMostCommon(words);
            },
                (Action)delegate
            {
                // Получить самое длинное слово
                longestWord = FindLongestWord(words);
            });

            // Когда все задачи завершены, построить строку
            // показывающую всю статистику в окне сообщений
            StringBuilder bookStats = new StringBuilder("Ten Most Common Words are:\n");
            foreach (string s in tenMostCommon)
            {
                bookStats.AppendLine(s);
            }

            bookStats.AppendFormat("Longest word is: {0}", longestWord);
            bookStats.AppendLine();
            MessageBox.Show(bookStats.ToString(), "Book info");
        }

        private string[] FindTenMostCommon(string[] words)
        {
            var frequencyOrder = from word in words
                                 where word.Length > 6
                                 group word by word into g
                                 orderby g.Count() descending
                                 select g.Key;

            string[] commonWords = (frequencyOrder.Take(10)).ToArray();
            return commonWords;
        }
        private string FindLongestWord(string[] words)
        {
            return (from w in words orderby w.Length descending select w).FirstOrDefault();
        }
    }
}
