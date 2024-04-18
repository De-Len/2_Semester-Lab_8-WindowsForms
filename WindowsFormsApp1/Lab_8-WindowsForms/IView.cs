using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8_WindowsForms
{
    internal interface IView
    {
        string Label3Text { get; set; }
        string TextBox1Text {  get; set; }
        string TextBox2Text { get; set; }


        string GetPath1();
        string GetPath2();

        event EventHandler<EventArgs> SyncFirstDirectory;
        event EventHandler<EventArgs> SyncSecondDirectory;
    }
}