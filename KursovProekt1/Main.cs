using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovProekt1
{
    public partial class Main : Form
    {
        private DbConn db;
        public Main()
        {
            InitializeComponent();
            db = new DbConn();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = db.displayCars();
        }

        private void справкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spravka spravka = new Spravka();
            spravka.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                dataGridView2.DataSource = db.displayOrders(Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));    
            }
        }

        private void изходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void колаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCars frm = new frmCars();
            frm.ShowDialog();
        }
    }
}
