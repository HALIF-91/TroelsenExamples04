using System;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace FunWithCSharpAsync
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnCallMethod_Click(object sender, EventArgs e)
        {
            this.Text = await DoWork();
        }

        private Task<string> DoWork()
        {
            return Task.Run(() => 
            {
                Thread.Sleep(10000);
                return "Done with work!";
            });
        }

        private async void btnMultiAwaits_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
            });
            MessageBox.Show("Done with first Task!");

            await Task.Run(() =>
            {
                Thread.Sleep(2000);
            });
            MessageBox.Show("Done with second Task!");

            await Task.Run(() =>
            {
                Thread.Sleep(2000);
            });
            MessageBox.Show("Done with third Task!");
        }
    }
}
