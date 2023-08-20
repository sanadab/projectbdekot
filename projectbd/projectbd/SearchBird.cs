//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;
//using Application = Microsoft.Office.Interop.Excel.Application;
////using Application = Microsoft.Office.Interop.Excel.Application;
//using Excel = Microsoft.Office.Interop.Excel;
//using System.IO;
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
using System.Data.SqlClient;
using System.Diagnostics;

namespace projectbd
{
    public partial class SearchBird : Form
    {
        private SqlConnection con;
        public SearchBird()
        {
            InitializeComponent();
            string sql = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\hosni.mdf;Integrated Security=True;Connect Timeout=30";
            con = new SqlConnection(sql);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.options(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string searchKeyword1 = textBox1.Text;
            string searchKeyword2 = textBox2.Text;
            string searchKeyword3 = textBox3.Text;
            string searchKeyword4 = textBox4.Text;

            if (string.IsNullOrWhiteSpace(searchKeyword1)|| string.IsNullOrWhiteSpace(searchKeyword2)| string.IsNullOrWhiteSpace(searchKeyword3)| string.IsNullOrWhiteSpace(searchKeyword4))
            {
                MessageBox.Show("Please enter a search keyword.");
                return;
            }

            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\addbird.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //string query = "SELECT * FROM [Table] WHERE id = @searchKeyword1 AND zn = @searchKeyword2 AND  sex = @searchKeyword4   ";
                    string query = "SELECT * FROM [Table] WHERE id = @searchKeyword1 AND zn = @searchKeyword2 AND sex = @searchKeyword4 ORDER BY id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@searchKeyword1", searchKeyword1);
                        command.Parameters.AddWithValue("@searchKeyword2", searchKeyword2);
                        command.Parameters.AddWithValue("@searchKeyword3", searchKeyword3);
                        command.Parameters.AddWithValue("@searchKeyword4", searchKeyword4);

                        SqlDataReader reader = command.ExecuteReader();
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        reader.Close();

                        // Clear existing data in dataGridView1
                        dataGridView1.DataSource = null;
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();

                        // Set the new data source
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching: " + ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            // Retrieve the current running process
            Process currentProcess = Process.GetCurrentProcess();

            // Kill the process to stop debugging
            currentProcess.Kill();


        }

      
    }
}
