using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_8_WindowsForms
{
    public partial class View : Form, IView
    {
        private Presenter _presenter;

        public View()
        {
            InitializeComponent();
            _presenter = new Presenter(this);

            syncFirstDirectoryButton.Click += syncFirstDirectoryButton_Click;
            syncSecondDirectoryButton.Click += SyncFirstDirectoryButton_Click;
        }

        public event EventHandler<EventArgs> SyncFirstDirectory;
        public event EventHandler<EventArgs> SyncSecondDirectory;

        private void syncFirstDirectoryButton_Click(object sender, EventArgs e)
        {
            // Получение данных от пользователя (данные директории)
            string path1 = _presenter._iView.GetPath1InTextBox1Text;
            string path2 = _presenter._iView.GetPath2InTextBox2Text;

            SyncFirstDirectory(sender, e);
        }

        private void SyncFirstDirectoryButton_Click(object sender, EventArgs e)
        {
            // Получение данных от пользователя (данные директории)
            string path1 = _presenter._iView.GetPath1InTextBox1Text;
            string path2 = _presenter._iView.GetPath2InTextBox2Text;

            SyncSecondDirectory(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
