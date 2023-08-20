using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;

namespace projectbd
{
    public partial class Login : Form
    {
        private const string ExcelPath = @"C:\Users\sanad\Desktop\cs\aa11.xlsx";
        public Login()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<List<string>> exelData = ReadExcelFile(ExcelPath);
                List<string> user = exelData.FirstOrDefault(x => x[1] == username && x[2] == password);

                if (user == null)
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.options(this);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public List<List<string>> ReadExcelFile(string filePath)
        {
            List<List<string>> excelData = new List<List<string>>();

            // Create an ExcelDataReader object by opening the Excel file
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Read the Excel file row by row
                    while (reader.Read())
                    {
                        List<string> rowData = new List<string>();

                        // Read each column in the current row
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            // Add the cell value to the row data list
                            rowData.Add(reader.GetValue(i)?.ToString());
                        }

                        // Add the row data to the excelData list
                        excelData.Add(rowData);
                    }
                }
            }

            return excelData;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.signup(this);
        }
    }
}

