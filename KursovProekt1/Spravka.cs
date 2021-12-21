using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovProekt1
{
    public partial class Spravka : Form
    {
        private DbConn db;

        public Spravka()
        {
            InitializeComponent();
            db = new DbConn();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dateTime = dateTimePicker1.Value;

            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }

            dataGridView1.DataSource = db.displayData(dateTime.ToString("G"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
