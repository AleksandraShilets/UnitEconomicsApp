using System;
using System.Windows.Forms;

namespace UnitEconomicsApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Запускаем проект со стартовой титульной формы
            Application.Run(new FormTitle());
        }
    }
}
