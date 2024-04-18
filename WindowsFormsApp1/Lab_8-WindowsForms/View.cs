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

            button1.Click += button1_Click;
            button2.Click += button2_Click;
        }

        public event EventHandler<EventArgs> SyncFirstDirectory;
        public event EventHandler<EventArgs> SyncSecondDirectory;

        string IView.GetPath1()
        {
            return _presenter.GetPath1();
        }

        string IView.GetPath2()
        {
            return _presenter.GetPath2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Получение данных от пользователя (данные директории)
            string path1 = ((IView)this).GetPath1();
            string path2 = ((IView)this).GetPath2();

            SyncFirstDirectory(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получение данных от пользователя (данные директории)
            string path1 = ((IView)this).GetPath1();
            string path2 = ((IView)this).GetPath2();

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
