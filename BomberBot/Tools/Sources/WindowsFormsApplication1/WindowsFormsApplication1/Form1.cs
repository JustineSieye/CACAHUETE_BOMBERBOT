/*****************
 * Justine Sieye *
 * ***************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            for(int i = 0; i < Convert.ToInt32(width.Text); i++)
            {
                dataGridView1.Columns.Add("","");
            }

            for (int i = 0; i < Convert.ToInt32(height.Text); i++)
            {
                dataGridView1.Rows.Add("", "");
            }

            foreach (DataGridViewColumn data in dataGridView1.Columns)
            {
                data.Width = 30;
            }

            foreach (DataGridViewRow data in dataGridView1.Rows)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    data.Cells[j].Value = "0";
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            if (Breakable.Checked)
            {
                CellStyle.BackColor = Color.SandyBrown;
                dataGridView1.CurrentCell.Value = "2";
            }

            if (Unbreakable.Checked)
            {
                CellStyle.BackColor = Color.Black;
                dataGridView1.CurrentCell.Value = "1";
            }

            if (Flag.Checked)
            {
                CellStyle.BackColor = Color.Bisque;
                dataGridView1.CurrentCell.Value = "3";
            }

            if (RedHQ.Checked)
            {
                CellStyle.BackColor = Color.Red;
                dataGridView1.CurrentCell.Value = "5";
            }

            if (GreenHQ.Checked)
            {
                CellStyle.BackColor = Color.Green;
                dataGridView1.CurrentCell.Value = "7";
            }

            if (BlueHQ.Checked)
            {
                CellStyle.BackColor = Color.Blue;
                dataGridView1.CurrentCell.Value = "6";
            }

            if (YellowHQ.Checked)
            {
                CellStyle.BackColor = Color.Yellow;
                dataGridView1.CurrentCell.Value = "4";
            }

            if (None.Checked)
            {
                CellStyle.BackColor = Color.White;
                dataGridView1.CurrentCell.Value = "0";
            }

            CellStyle.SelectionBackColor = Color.Transparent;

            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = CellStyle;
        }

        private void generate_Click(object sender, EventArgs e)
        {
            writeBinary();
        }

        public void writeBinary()
        {
            string nom = tbName.Text + ".bytes"; byte i;
            BinaryReader br = null;
            BinaryWriter bw = null;
            FileStream fs = null;
            //Ecriture d'octets dans le fichier
            bw = new BinaryWriter(File.Create(nom));

            i = Convert.ToByte(width.Text);
            bw.Write(i);

            i = Convert.ToByte(height.Text);
            bw.Write(i);

            i = Convert.ToByte(random.Checked);
            bw.Write(i);

            for (int j = 3; j < Convert.ToInt32(width.Text); j++)
            {
                bw.Write(Convert.ToSByte(0));
            }


            foreach (DataGridViewRow data in dataGridView1.Rows)
            {
                for(int j = 0; j < dataGridView1.Columns.Count;j++)
                {
                    i = Convert.ToByte(data.Cells[j].Value);

                    bw.Write(i);
                }
            }

            bw.Close();
        }

    }
}
