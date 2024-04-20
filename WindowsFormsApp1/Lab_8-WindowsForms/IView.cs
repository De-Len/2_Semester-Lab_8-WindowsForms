using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8_WindowsForms
{
    internal interface IView
    {
        string LogOutputLabel3Text { get; set; }
        string GetPath1InTextBox1Text {  get; set; }
        string GetPath2InTextBox2Text { get; set; }

        event EventHandler<EventArgs> SyncFirstDirectory;
        event EventHandler<EventArgs> SyncSecondDirectory;
    }
}