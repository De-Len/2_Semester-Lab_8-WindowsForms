using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8_WindowsForms
{
    internal class Presenter
    {
        private List<string> _created, _deleted, _replaced;
        private IView _iView;
        private View _view;
        private Model _model;

        public Presenter(IView newView)
        {
            _iView = newView;
            _model = new Model();
            _iView.SyncFirstDirectory += new EventHandler<EventArgs>(Sync1);
            _iView.SyncSecondDirectory += new EventHandler<EventArgs>(Sync2);
        }

        private void Sync1(object sender, EventArgs e)
        {
            string path1 = _iView.TextBox1Text;
            string path2 = _iView.TextBox2Text;

            _model.SynsDirectory(path1, path2, DirectoryChoice.FirstDirectory, out _created, out _deleted, out _replaced);

            foreach (string file in _created)
            {
                _iView.Label3Text += $"Файл \"{file}\" создан в первой директории\n";

            }
            foreach (string file in _deleted)
            {
                _iView.Label3Text += $"Файл \"{file}\" удалён в первой директории\n";
            }
            foreach (string file in _replaced)
            {
                _iView.Label3Text += $"Файл \"{file}\" изменён в первой директории\n";
            }
        }

        private void Sync2(object sender, EventArgs e)
        {
            string path1 = _iView.TextBox1Text;
            string path2 = _iView.TextBox2Text;

            _model.SynsDirectory(path1, path2, DirectoryChoice.SecondDirectory, out _created, out _deleted, out _replaced);

            foreach (string file in _created)
            {
                _iView.Label3Text += $"Файл \"{file}\" создан во второй директории\n";

            }
            foreach (string file in _deleted)
            {
                _iView.Label3Text += $"Файл \"{file}\" удалён во второй директории\n";
            }
            foreach (string file in _replaced)
            {
                _iView.Label3Text += $"Файл \"{file}\" изменён во второй директории\n";
            }
        }

        public string GetPath1()
        {
            return _iView.TextBox1Text;
        }

        public string GetPath2()
        {
            return _iView.TextBox2Text;
        }
    }
}
