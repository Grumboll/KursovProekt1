using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KursovProekt1
{
    public partial class Spravka : Form
    {
        private DbConn db = DbConn.getInstance();

        public Spravka()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Таблицата е празна");
                return;
            }
            StringBuilder text = new StringBuilder();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                foreach (DataGridViewCell cell in row.Cells)
                {
                    text.Append(cell.Value.ToString() + " ");
                }
                text.AppendLine();
            }

            File.WriteAllText("Spravka.txt", text.ToString());
        }
    }
}
