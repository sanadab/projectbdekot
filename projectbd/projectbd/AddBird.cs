using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.IO;
using DataTable = System.Data.DataTable;
using projectbd; // Add this line if 'EditBird' is in a different namespace
using System.Collections;

namespace projectbd
{
    public partial class AddBird : Form
    {
        private Dictionary<string, List<string>> ttznOptions;
        public AddBird()
        {
            InitializeComponent();


            Loadmen();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.options(this);
        }
  
        private void LoadZnOptions()
        {
            
            comboBox1.Items.AddRange(new string[] { "American", "European", "Australian" });
        }

        private void LoadTtznOptions()
        {

      
            comboBox2.Items.AddRange(new string[] { "North", "Central", "South", "East", "West", "Center", "CoastalC" });
        }
        private void Loadmen()
        {
            comboBox3.Items.AddRange(new string[] { "Male", "Female" });
        }
    


        private void button1_Click(object sender, EventArgs e)
        {
       
            //if (!IsValidId(textBox1.Text) || !IsValidLetters(comboBox1.Text) || !IsValidLetters(comboBox2.Text))
            //{
            //    MessageBox.Show("Invalid input. Please enter valid values.");
            //    return;
            //}
            string species = textBox1.Text;
            string zn = comboBox1.Text;
            string ttzn = comboBox2.Text;
            string datebkiaa = textBox3.Text;
            string sex = comboBox3.Text;
            string numclov = textBox6.Text;
            string iddad = textBox7.Text;
            string idmom = textBox8.Text;

            // Perform the necessary database operations to add the bird
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\addbird.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO [Table] (id, zn, ttzn, datebkiaa, sex, numclov, iddad, idmom) VALUES (@id, @zn, @ttzn, @datebkiaa, @sex, @numclov, @iddad, @idmom)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", species);
                cmd.Parameters.AddWithValue("@zn", zn);
                cmd.Parameters.AddWithValue("@ttzn", ttzn);
               // cmd.Parameters.AddWithValue("@datebkiaa", datebkiaa);
                List<string> date = textBox3.Text.Split('/').ToList();
                cmd.Parameters.AddWithValue("@datebkiaa", new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0])));
                cmd.Parameters.AddWithValue("@sex", sex);
                cmd.Parameters.AddWithValue("@numclov", numclov);
                cmd.Parameters.AddWithValue("@iddad", iddad);
                cmd.Parameters.AddWithValue("@idmom", idmom);
                cmd.ExecuteNonQuery();

                con.Close();
            }

     
        }
    

    public bool IsValidId(string id)
    {
        // Validate the bird ID (e.g., must be a valid format)
        // Implement your validation logic here
        return !string.IsNullOrEmpty(id);
    }

    public bool IsValidLetters(string text)
        {
            return !string.IsNullOrEmpty(text) && text.All(char.IsLetter);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {



        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddBird_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] { "American", "European", "Australian" });
            comboBox2.Enabled = false;



        }
        private void SearchBirdHouse(int birdHouseId)
        {
            string birdHouseConnectionString = "Your_BirdHouse_Database_Connection_String";
            string birdDetailsConnectionString = "Your_BirdDetails_Database_Connection_String";

            // Search BirdHouse in the second database
            using (SqlConnection birdHouseConnection = new SqlConnection(birdHouseConnectionString))
            {
                birdHouseConnection.Open();

                string birdHouseQuery = "SELECT * FROM BirdHouseTable WHERE id = @BirdHouseId";
                SqlCommand birdHouseCommand = new SqlCommand(birdHouseQuery, birdHouseConnection);
                birdHouseCommand.Parameters.AddWithValue("@BirdHouseId", birdHouseId);

                using (SqlDataReader birdHouseReader = birdHouseCommand.ExecuteReader())
                {
                    if (birdHouseReader.HasRows)
                    {
                        // BirdHouse found, retrieve bird details from the first database
                        using (SqlConnection birdDetailsConnection = new SqlConnection(birdDetailsConnectionString))
                        {
                            birdDetailsConnection.Open();

                            string birdDetailsQuery = "SELECT * FROM BirdDetailsTable WHERE numclov = @NumClov";
                            SqlCommand birdDetailsCommand = new SqlCommand(birdDetailsQuery, birdDetailsConnection);
                            birdDetailsCommand.Parameters.AddWithValue("@NumClov", birdHouseId);

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
                                        int numClov = birdDetailsReader.GetInt32(birdDetailsReader.GetOrdinal("numclov"));

                                        // Display or use the bird details as needed
                                        MessageBox.Show($"ID: {id}\nZn: {zn}\nTtzn: {ttzn}\nSex: {sex}\nDate Bkiaa: {datebkiaa}\nID Dad: {idDad}\nID Mom: {idMom}\nNum Clov: {numClov}");
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            // Retrieve the current running process
            Process currentProcess = Process.GetCurrentProcess();

            // Kill the process to stop debugging
            currentProcess.Kill();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\addbird.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "UPDATE [Table] set zn=@zn,ttzn=@ttzn,sex=@sex,numclov=@numclov,datebkiaa=@datebkiaa ,iddad=@iddad,idmom=@idmom WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@zn", comboBox1.Text);
            cmd.Parameters.AddWithValue("@ttzn", comboBox2.Text);
            cmd.Parameters.AddWithValue("@sex", comboBox3.Text);
            cmd.Parameters.AddWithValue("@numclov", textBox6.Text);
            cmd.Parameters.AddWithValue("@iddad", textBox7.Text);
            List<string> date = textBox3.Text.Split('/').ToList();
            cmd.Parameters.AddWithValue("@datebkiaa", new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0])));
            cmd.Parameters.AddWithValue("@idmom", textBox8.Text);
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




        private void button6_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\addbird.mdf;Integrated Security=True;Connect Timeout=30";
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

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sanad\\Documents\\addbird.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "UPDATE [Table] set  gozal=@gozal WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, con);

            
            cmd.Parameters.AddWithValue("@gozal",textBox2.Text );
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();

            if (comboBox1.SelectedItem.ToString() == "American")
            {
                comboBox2.Items.AddRange(new string[] { "North", "Center", "South" });
            }
            else if (comboBox1.SelectedItem.ToString() == "European")
            {
                comboBox2.Items.AddRange(new string[] { "East", "West" });
            }
            else if (comboBox1.SelectedItem.ToString() == "Australian")
            {
                comboBox2.Items.AddRange(new string[] { "Center", "CoastalC" });
            }

            comboBox2.Enabled = true;

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
