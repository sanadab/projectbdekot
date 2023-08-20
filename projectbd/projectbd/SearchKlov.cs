using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace projectbd
{
    public partial class SearchKlov : Form
    {
        public SearchKlov()
        {
            InitializeComponent();
            asd();
        }
        private void asd()
        {
            comboBox1.Items.AddRange(new string[] { "Iron", "Tree", "Plastic" });
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Program.options(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Retrieve the current running process
            Process currentProcess = Process.GetCurrentProcess();

            // Kill the process to stop debugging
            currentProcess.Kill();
        }

        private void SearchBirdHouse(int birdHouseId)
        {
            string searchKeyword1 = textBox1.Text;
            string searchKeyword2 = comboBox1.Text;


            if (string.IsNullOrWhiteSpace(searchKeyword1) )
            {
                MessageBox.Show("Please enter a search keyword.");
                return;
            }

            try
            {
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\sanad.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //string query = "SELECT * FROM [Table] WHERE id = @searchKeyword1 AND zn = @searchKeyword2 AND  sex = @searchKeyword4   ";
                    string query = "SELECT * FROM [Table] WHERE id = @searchKeyword1 AND homer = @searchKeyword2 ORDER BY id ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@searchKeyword1", searchKeyword1);
                        command.Parameters.AddWithValue("@searchKeyword2", searchKeyword2);
                     
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

            string birdHouseConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\sanad.mdf;Integrated Security=True;Connect Timeout=30";//klov
            string birdDetailsConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\addbird.mdf;Integrated Security=True;Connect Timeout=30";

            // Search BirdHouse in the second database
            using (SqlConnection birdHouseConnection = new SqlConnection(birdHouseConnectionString))
            {
                birdHouseConnection.Open();

                string birdHouseQuery = "SELECT * FROM [Table] WHERE id = @id";
                SqlCommand birdHouseCommand = new SqlCommand(birdHouseQuery, birdHouseConnection);
                birdHouseCommand.Parameters.AddWithValue("@id", birdHouseId);

                using (SqlDataReader birdHouseReader = birdHouseCommand.ExecuteReader())
                {
                    if (birdHouseReader.HasRows)
                    {
                        // BirdHouse found, retrieve bird details from the first database
                        using (SqlConnection birdDetailsConnection = new SqlConnection(birdDetailsConnectionString))
                        {
                            birdDetailsConnection.Open();

                            string birdDetailsQuery = "SELECT * FROM [Table] WHERE id = @numclov";
                            SqlCommand birdDetailsCommand = new SqlCommand(birdDetailsQuery, birdDetailsConnection);
                            birdDetailsCommand.Parameters.AddWithValue("@numclov",birdHouseId);

                            using (SqlDataReader birdDetailsReader = birdDetailsCommand.ExecuteReader()) 
                            
                            {
                                if (birdDetailsReader.HasRows)
                                {
                                    // Display bird details
                                    while (birdDetailsReader.Read())
                                    {
                                        int id = birdDetailsReader.GetInt32(birdDetailsReader.GetOrdinal("id"));
                                        string zn = birdDetailsReader.GetString(birdDetailsReader.GetOrdinal("zn"));
                                        string ttzn = birdDetailsReader.GetString(birdDetailsReader.GetOrdinal("ttzn"));
                                        string sex = birdDetailsReader.GetString(birdDetailsReader.GetOrdinal("sex"));
                                        DateTime datebkiaa = birdDetailsReader.GetDateTime(birdDetailsReader.GetOrdinal("datebkiaa"));
                                        int idDad = birdDetailsReader.GetInt32(birdDetailsReader.GetOrdinal("iddad"));
                                        int idMom = birdDetailsReader.GetInt32(birdDetailsReader.GetOrdinal("idmom"));
                                        string numClov = birdDetailsReader.GetString(birdDetailsReader.GetOrdinal("numclov"));


                                        // Display or use the bird details as needed
                                        MessageBox.Show($"ID: {id}\nZn: {zn}\nTtzn: {ttzn}\nSex: {sex}\nDate Bkiaa: {datebkiaa}\nID Dad: {idDad}\nID Mom: {idMom}\nNum Clov: {numClov}\n");
                                    }
                                }
                                else
                                {
                                    // Bird details not found
                                    MessageBox.Show("Bird details not found for the given BirdHouse ID.");
                                }
                            }
                        }
                    }
                    else
                    {
                        // BirdHouse not found
                        MessageBox.Show("BirdHouse not found.");
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            //string searchKeyword1 = textBox1.Text;
            //string searchKeyword2 = textBox2.Text;


            //if (string.IsNullOrWhiteSpace(searchKeyword1) || string.IsNullOrWhiteSpace(searchKeyword2))
            //{
            //    MessageBox.Show("Please enter a search keyword.");
            //    return;
            //}

            //try
            //{
            //    string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\sanad.mdf;Integrated Security=True;Connect Timeout=30";
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        string query = "SELECT * FROM [Table] WHERE id = @searchKeyword1 AND homer = @searchKeyword2 ORDER BY id  ";
            //        using (SqlCommand command = new SqlCommand(query, connection))
            //        {
            //            command.Parameters.AddWithValue("@searchKeyword1", searchKeyword1);
            //            command.Parameters.AddWithValue("@searchKeyword2", searchKeyword2);


            //            SqlDataReader reader = command.ExecuteReader();
            //            DataTable dataTable = new DataTable();
            //            dataTable.Load(reader);
            //            reader.Close();

            //            // Clear existing data in dataGridView1
            //            dataGridView1.DataSource = null;
            //            dataGridView1.Rows.Clear();
            //            dataGridView1.Columns.Clear();

            //            // Set the new data source
            //            dataGridView1.DataSource = dataTable;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An error occurred while searching: " + ex.Message);
            //}
            int birdHouseId;
            if (int.TryParse(textBox1.Text, out birdHouseId))
            {
                // Perform the BirdHouse search
                SearchBirdHouse(birdHouseId);
            }
            else
            {
                MessageBox.Show("Please enter a valid BirdHouse ID.");
            }
        }

    }
    }

