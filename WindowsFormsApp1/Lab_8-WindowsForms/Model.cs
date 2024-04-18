using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Комменты и маненечко кода от Persona Grata - ChatGPT (или claude 3 Haiku)
namespace Lab_8_WindowsForms
{
    internal class Model
    {
        public void SynsDirectory(string Path1, string Path2, DirectoryChoice directoryChoice, out List<string> createdFiles, out List<string> deletedFiles, out List<string> replacedFiles)
        {

            createdFiles = new List<string>();
            deletedFiles = new List<string>();
            replacedFiles = new List<string>();

            List<string> filesPath1 = Directory.GetFiles(Path1).ToList<string>();
            List<string> filesPath2 = Directory.GetFiles(Path2).ToList<string>();

            // Сравниваем имена файлов в директориях
            var fileNames1 = filesPath1.Select(file => Path.GetFileName(file));
            var fileNames2 = filesPath2.Select(file => Path.GetFileName(file));
            if (fileNames1.SequenceEqual(fileNames2))
            {
                MessageBox.Show("Директории уже синхронизированы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Файлы, которые есть в filesPath1, но нет в filesPath2
            List<string> missingFilesInPath2 = fileNames1.Except(fileNames2).ToList();
            // Файлы, которые есть в filesPath2, но нет в filesPath1
            List<string> missingFilesInPath1 = fileNames2.Except(fileNames1).ToList();

            // Файлы, которые есть в обеих директориях, но имеют разное содержимое 
            List<string> modifiedFiles = filesPath1.Intersect(filesPath2)
                                                 .Where(file => !FilesAreEqual(file, Path.Combine(Path2, Path.GetFileName(file))))
                                                 .ToList();

            if (directoryChoice == DirectoryChoice.FirstDirectory)
            {
                // Копировать недостающие файлы из filesPath2 в filesPath1
                foreach (string fileName in missingFilesInPath1)
                {
                    string dirFile2 = Path.Combine(Path2, fileName);
                    string dirFile1 = Path.Combine(Path1, fileName);
                    File.Copy(dirFile2, dirFile1); // Копировать файл из 2 директории в 1
                    createdFiles.Add(fileName);

                }
                // Удалить файлы из filesPath1, которых нет в filesPath2
                foreach (string fileName in missingFilesInPath2)
                {
                    string DirFile = Path.Combine(Path1, fileName);
                    File.Delete(DirFile);
                    deletedFiles.Add(fileName);
                }

                // Заменить разные файлы 1 директории файлами 2 директории
                foreach (string fileName in modifiedFiles)
                {
                    string dirFile1 = Path.Combine(Path1, fileName);
                    string dirFile2 = Path.Combine(Path2, fileName);
                    File.Delete(dirFile1);
                    File.Copy(dirFile2, dirFile1);
                    replacedFiles.Add(fileName);
                }
            }

            else if (directoryChoice == DirectoryChoice.SecondDirectory)
            {
                // Копировать недостающие файлы из filesPath1 в filesPath2
                foreach (string fileName in missingFilesInPath2)
                {
                    string dirFile1 = Path.Combine(Path1, fileName);
                    string dirFile2 = Path.Combine(Path2, fileName);
                    File.Copy(dirFile1, dirFile2); // Копировать файл из 1 директории во 2
                    createdFiles.Add(fileName);
                }

                // Удалить файлы из filesPath2, которых нет в filesPath1
                foreach (string fileName in missingFilesInPath1)
                {
                    string DirFile = Path.Combine(Path2, fileName);
                    File.Delete(DirFile);
                    deletedFiles.Add(fileName);
                }

                // Заменить разные файлы 2 директории файлами 1 директории
                foreach (string fileName in modifiedFiles)
                {
                    string dirFile1 = Path.Combine(Path1, fileName);
                    string dirFile2 = Path.Combine(Path2, fileName);
                    File.Delete(dirFile2);
                    File.Copy(dirFile1, dirFile2);
                    replacedFiles.Add(fileName);
                }
            }
        }

        static bool FilesAreEqual(string file1, string file2)
        {
            byte[] bytes1 = File.ReadAllBytes(file1);
            byte[] bytes2 = File.ReadAllBytes(file2);
            return bytes1.SequenceEqual(bytes2);
        }

    }

    // "Кичливость философии не совмещается со смирением"
    public enum DirectoryChoice
    {
        FirstDirectory,
        SecondDirectory
    }

}
