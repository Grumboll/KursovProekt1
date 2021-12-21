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
    public partial class frmCars : Form
    {
        private DbConn db;
        public frmCars()
        {
            InitializeComponent();
            db = new DbConn();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string regNumber = textBox1.Text;
            string mark = textBox2.Text;
            int seats = Int32.Parse(textBox3.Text);
            bool luggage = checkBox1.Checked;
            string driver = textBox5.Text;
            db.insertCar(regNumber, mark, seats, luggage, driver);
        }
    }
}
