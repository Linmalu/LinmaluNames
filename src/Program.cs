using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LinmaluNames
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if(Process.GetProcessesByName("LinmaluNames").Length > 1)
            {
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Names());
        }
    }
}
