using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectbd
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.addbird(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.addklov(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.searchbird(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.searchklov(this);
        }
    }
}
