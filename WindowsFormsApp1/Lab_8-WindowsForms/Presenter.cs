using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_8_WindowsForms
{
    internal class Presenter
    {
        List<string> createdFiles = new List<string>();
        List<string> deletedFiles = new List<string>();
        List<string> replacedFiles = new List<string>();
        public IView _iView;
        private View _view;
        private Model _model;

        public Presenter(IView newView)
        {
            _iView = newView;
            _model = new Model();
            _iView.SyncFirstDirectory += new EventHandler<EventArgs>((sender, e) => Sync(sender, e, DirectoryChoice.FirstDirectory));
            _iView.SyncSecondDirectory += new EventHandler<EventArgs>((sender, e) => Sync(sender, e, DirectoryChoice.SecondDirectory));
        }

        private void Sync(object sender, EventArgs e, DirectoryChoice directoryChoice)
        {
            string path1 = _iView.FirstPath();
            string path2 = _iView.SecondPath();

            List<string> result = _model.SyncDirectory(path1, path2, directoryChoice);

            if (result[0] == "Ошибка: директории уже синхронизированы") 
            {
                MessageBox.Show("Директории уже синхронизированы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (result[0] == "Ошибка: бяка в пути 1 директории")
            {
                MessageBox.Show("У вас бяка в пути 1 директории", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (result[0] == "Ошибка: бяка в пути 2 директории")
            {
                MessageBox.Show("У вас бяка в пути 2 директории", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<string> createdFiles = new List<string>();
            List<string> deletedFiles = new List<string>();
            List<string> replacedFiles = new List<string>();

            foreach (string file in result)
            {
                if (file.StartsWith("CREATED: "))
                {
                    createdFiles.Add(file.Substring(9));
                }
                else if (file.StartsWith("DELETED: "))
                {
                    deletedFiles.Add(file.Substring(9));
                }
                else if (file.StartsWith("REPLACED: "))
                {
                    replacedFiles.Add(file.Substring(10));
                }
            }

            foreach (string fileInList in createdFiles)
            {
                _iView.LogOutput += $"Файл \"{fileInList}\" создан в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории \n";
            }
            foreach (string fileInList in deletedFiles)
            {
                _iView.LogOutput += $"Файл \"{fileInList}\" удалён в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории \n";
            }
            foreach (string fileInList in replacedFiles)
            {
                _iView.LogOutput += $"Файл \"{fileInList}\" изменён в {(directoryChoice == DirectoryChoice.FirstDirectory ? "первой" : "второй")} директории \n";
            }
        }
    }
}
