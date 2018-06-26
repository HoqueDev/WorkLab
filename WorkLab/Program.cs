using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkLab.Error;
using WorkLab.Model;

namespace WorkLab
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //testing
            //test();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);

            new Form5();
            new Form2();
            new Form3();
            new Form1();
           
            Application.Run(new Form1());
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string title = "Unknown Error";
            string msg = "An unexpected error ocurred";

            if(e.ExceptionObject is Exception)
            {
                Exception ex = default(Exception);
                ex = (Exception)e.ExceptionObject;

                if (ex is TargetInvocationException)
                {
                   
                    ex = ex.InnerException;
                }


                Console.WriteLine("Type of ex is " + ex.GetType().ToString());

                if (ex is ApiException)
                {
                    title = "Error";
                    msg = ((ApiException)ex).Message;
                }
            }
            
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
