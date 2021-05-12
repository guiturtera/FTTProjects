using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GambleGame
{
    public partial class Statistics : Form
    {
        DataTable table;
        private int apple = 0;
        private int banana = 0;
        private int orange = 0;
        private int defeat = 0;
        public Statistics()
        {
            InitializeComponent();
            InitializeEsperanca();
        }

        private void InitializeEsperanca()
        {
            table = new DataTable();
            table.Columns.Add("X", typeof(double));
            table.Columns.Add("P(X)", typeof(double));
            table.Columns.Add("E(X) [X * P(X)]", typeof(double));

            AddRow(4, 0.001);
            AddRow(2, 0.027);
            AddRow(0, 0.216);
            AddRow(-1, 0.756);
            SomaEsperanca();

            dataGridView1.DataSource = table;
        }

        private void SomaEsperanca()
        {
            double count = 0;
            DataRow row = table.NewRow();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                count += table.Rows[i].Field<double>("E(X) [X * P(X)]");
            }
            row["E(X) [X * P(X)]"] = count;
            table.Rows.Add(row);
        }

        private void AddRow(double x, double px)
        {
            DataRow row = table.NewRow();
            row["X"] = x;//4;
            row["P(X)"] = px;
            row["E(X) [X * P(X)]"] = x * px;
            table.Rows.Add(row);
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            foreach (int a in Combination.CombinationFinish)
            {
                if (a == 1)
                    apple++;
                if (a == 3)
                    banana++;
                if (a == 5)
                    orange++;
                else
                    defeat++;
            }
            Barras.Series["Apple"].Points.AddXY("Maçã", apple.ToString());
            Barras.Series["Banana"].Points.AddXY("Banana", banana.ToString());
            Barras.Series["Orange"].Points.AddXY("Laranja", orange.ToString());
            Barras.Series["Defeat"].Points.AddXY("Derrota", defeat.ToString());
        }
    }
}
