using C1TrueDBGridStylesImageExtractor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C1TrueDBGridStylesImageExtractorUI
{
    public partial class Form1 : Form
    {
        BackgroundWorker worker;

        public Form1()
        {
            InitializeComponent();
            Console.SetOut(new ControlWriter(richTextBox1));
            worker = new BackgroundWorker { WorkerReportsProgress = true };
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
        }

        private void btnFrx_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolder.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string folderPath = txtFolder.Text;
            FolderReader folderReader = new FolderReader();
            try
            {
                folderReader.ProcessFrxFiles(folderPath);
                System.Windows.Forms.MessageBox.Show("Images extracted");
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message + " Aborting operation");
            }
            if (!folderPath.Equals(""))
            {
                folderReader.SaveLog(folderPath);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}
