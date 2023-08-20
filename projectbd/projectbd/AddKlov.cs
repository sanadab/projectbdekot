using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = Microsoft.Office.Interop.Excel.Application;
//using Application = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace projectbd
{
    public partial class AddKlov : Form
    {
        public AddKlov()
        {
            InitializeComponent();
            LoadHomerOptions();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.options(this);
        }
        private void LoadHomerOptions()
        {
            // Add the homer options to the combobox
            comboBox1.Items.AddRange(new string[] { "Iron", "Tree", "Plastic" });
        }
        private void button1_Click(object sender, EventArgs e)
        {



        
            string sql = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\sanad.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(sql);
            con.Open();

            string query = "INSERT INTO [Table] (id, aorch, rohav, govah, homer) VALUES (@id, @aorch, @rohav, @govah, @homer)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@aorch", textBox2.Text);
            cmd.Parameters.AddWithValue("@rohav", textBox3.Text);
            cmd.Parameters.AddWithValue("@govah", textBox4.Text);
            cmd.Parameters.AddWithValue("@homer", comboBox1.Text); // Use the selected value from the combobox
            cmd.ExecuteNonQuery();

            con.Close();
        }





    


    private void save()
        {
            Excel.Application app = new Excel.Application(); 
            Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheets = null;
            app.Visible = true;
            worksheets = workbook.Sheets[1];
            worksheets = workbook.ActiveSheet;
            worksheets.Name = "addklov";
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheets.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;

            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheets.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            workbook.Save();
            workbook.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            save();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // Retrieve the current running process
            Process currentProcess = Process.GetCurrentProcess();

            // Kill the process to stop debugging
            currentProcess.Kill();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\sanad.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT * FROM [Table]";
            SqlCommand cmd = new SqlCommand(query, con);
            var reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\sanad.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "UPDATE [Table] set aorch=@aorch,rohav=@rohav,govah=@govah,homer=@homer WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@aorch", textBox2.Text);
            cmd.Parameters.AddWithValue("@rohav", textBox3.Text);
            cmd.Parameters.AddWithValue("@govah", textBox4.Text);
            cmd.Parameters.AddWithValue("@homer", comboBox1.Text);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);



            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data updated successfully");
                }
                else
                {
                    MessageBox.Show("No matching rows found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }





        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
