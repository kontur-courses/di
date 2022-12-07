using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TagCloud2
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
           Application.Run(new Form1());
        }
    }
}