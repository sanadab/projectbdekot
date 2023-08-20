
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace projectbd
{
    public partial class Form1 : Form
    {
        private object row;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private bool IsUsernameValid(string username)
        {
            if (username.Length < 6 || username.Length > 8)
            {
                return false;
            }

            int digitCount = 0;
            foreach (char c in username)
            {
                if (char.IsDigit(c))
                {
                    digitCount++;
                }
                else if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            return digitCount <= 2;
        }
    

    private bool IsPasswordValid(string password)
    {
        if (password.Length > 10 || password.Length < 8)
        {
            return false;
        }

        int digitCount = 0;
        int specialCharCount = 0;
        foreach (char c in password)
        {
            if (char.IsDigit(c))
            {
                digitCount++;
            }
            else if (!char.IsLetterOrDigit(c))
            {
                specialCharCount++;
            }
        }

        return digitCount == 1 && specialCharCount == 1;
    }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            int length1 = username.Length;
            string password = textBox2.Text;
            int length2 = password.Length;
            string id = textBox3.Text;
            int length3 = id.Length;

            if (length1 == 0 || length2 == 0 || length3 == 0)
            {
                MessageBox.Show("Enter your details.");
                return;
            }

            if (!IsUsernameValid(username))
            {
                MessageBox.Show("Username must contain between 6 and 8 characters, with at most 2 digits and the rest should be letters.");
                return;
            }

            if (!IsPasswordValid(password))
            {
                MessageBox.Show("Password must have between 8 and 10 characters, with at least 1 number and 1 special character.");
                return;
            }

            if (length3 != 9)
            {
                MessageBox.Show("ID must have exactly 9 characters.");
                return;
            }

            // Create an instance of the Excel.Application
            Excel.Application excelApp = new Excel.Application();

            // Set the file path and name for the Excel database
            string filePath = @"C:\Users\sanad\Desktop\cs\aa112\signup_database.xlsx";

            // Open the existing workbook or create a new one if it doesn't exist
            Excel.Workbook workbook;
            if (File.Exists(filePath))
            {
                workbook = excelApp.Workbooks.Open(filePath);
            }
            else
            {
                workbook = excelApp.Workbooks.Add();
                // Add headers to the worksheet
                Excel.Worksheet worksheet = workbook.Sheets[1];
                worksheet.Range["A1"].Value = "id";
                worksheet.Range["B1"].Value = "username";
                worksheet.Range["C1"].Value = "password";
            }

            // Retrieve the reference to the first worksheet
            Excel.Worksheet existingWorksheet = workbook.Sheets[1];

            // Find the last used row in the worksheet
            int lastRow = existingWorksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;

            // Set the starting row index for new data
            int rowIndex = lastRow + 1;

            // Save the user's information in the next available row
            existingWorksheet.Range["A" + rowIndex].Value = id;
            existingWorksheet.Range["B" + rowIndex].Value = username;
            existingWorksheet.Range["C" + rowIndex].Value = password;

            // Save the workbook
            workbook.Save();

            // Close the workbook and Excel application
            workbook.Close();
            excelApp.Quit();

            // Release the COM objects to avoid memory leaks
            Marshal.ReleaseComObject(existingWorksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excelApp);

            // Show a success message to the user
            MessageBox.Show("Sign-up successful!");

            // Call the login method (assuming it exists) to proceed to the login page
            Program.login(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.login(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

