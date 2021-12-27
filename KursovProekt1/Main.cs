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
using System.IO;

namespace KursovProekt1
{
    public partial class Main : Form
    {
        private DbConn db = DbConn.getInstance();
        private int id;
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            reloadCars();
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
                id = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                reloadOrders();
            }
        }

        private void изходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void колаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCars frm = new frmCars();
            frm.setMain(this);
            frm.ShowDialog();
        }
        private void поръчкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrders frm = new frmOrders();
            frm.setMain(this);
            frm.setId(id);
            frm.ShowDialog();
        }

        public void reloadCars()
        {
            dataGridView1.DataSource = db.displayCars();
        }

        public void reloadOrders()
        {
            dataGridView2.DataSource = db.displayOrders(id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder text = new StringBuilder();
            for(int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                foreach(DataGridViewCell cell in row.Cells)
                {
                    text.Append(cell.Value.ToString() + " ");
                }
                text.AppendLine();
            }

            File.WriteAllText("Table.txt", text.ToString());
        }
    }
}
