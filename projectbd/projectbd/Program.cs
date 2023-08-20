using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectbd
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
    
        public static void login(Form f)
        {
            f.Hide();
            Login login = new Login();
            login.Show();
        }
        public static void signup(Form f)
        {
            f.Hide();
            Form1 signup = new Form1();
            signup.Show();
        }
        public static void options(Form o)
        {
            o.Hide();
            Options options = new Options();
            options.Show();
        }
        public static void addbird(Form a)
        {
            a.Hide();
            AddBird addbird = new AddBird();
            addbird.Show();
        }
        public static void addklov(Form k)
        {
            k.Hide();
            AddKlov addklov = new AddKlov();
            addklov.Show();
            
        }
        public static void searchbird(Form s)
        {
            s.Hide();
            SearchBird searchBird = new SearchBird();
            searchBird.Show();
        }
        public static void searchklov(Form k)
        {
            k.Hide();
            SearchKlov searchklov = new SearchKlov();
            searchklov.Show();
        }




        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Application.Run(new Form2());
        }
    }
}
