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
    public partial class frEsperanca : Form
    {
        public frEsperanca()
        {
            InitializeComponent();
        }

        DataTable table;

        private void frEsperanca_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("X", typeof(double));
            table.Columns.Add("P(X)", typeof(double));
            table.Columns.Add("E(X) [X * P(X)]", typeof(double));

            AddRow(4, 0.001);
            AddRow(2, 0.027);
            AddRow(0, 0.216);
            AddRow(-1, 0.756);

            dataGridView1.DataSource = table;
        }

        private void AddRow(double x, double px)
        {
            DataRow row = table.NewRow();
            row["X"] = x;//4;
            row["P(X)"] = px;
            row["E(X) [X * P(X)]"] = x * px;
            table.Rows.Add(row);
            table.Rows.Add();
        }
    }
}
