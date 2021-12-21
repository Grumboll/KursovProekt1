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
    public partial class frmOrders : Form
    {
        private DbConn db = DbConn.getInstance();
        private Main main;
        private int id;

        public void setId(int id)
        {
            this.id = id;
        }

        public void setMain(Main main)
        {
            this.main = main;
        }

        public frmOrders()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string address = textBox1.Text;
            int distance = Int32.Parse(textBox2.Text);
            int fare = Int32.Parse(textBox3.Text);
            string orderTime = dateTimePicker1.Value.ToString("G");

            db.insertOrder(id, address, distance, fare, orderTime);
            main.reloadOrders();
            this.Close();
        }
    }
}
