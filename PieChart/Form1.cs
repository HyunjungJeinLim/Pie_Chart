using System;
using System.Drawing;
using System.Windows.Forms;

namespace PieChart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Label";
            dataGridView1.Columns[1].Name = "Value";

            btnUpdate.Click += btnUpdate_Click;
        }

        private void UpdateChart()
        {
            try
            {
                // Retrieve values from DataGridView
                int rowCount = dataGridView1.Rows.Count - 1; // Exclude last empty row
                float[] values = new float[rowCount];
                string[] labels = new string[rowCount];

                for (int i = 0; i < rowCount; i++)
                {
                    labels[i] = dataGridView1.Rows[i].Cells[0].Value?.ToString() ?? "";
                    values[i] = float.Parse(dataGridView1.Rows[i].Cells[1].Value?.ToString() ?? "0");
                }

                Color[] predefinedColors = new Color[]
                {
                    Color.LightBlue,
                    Color.LightCyan,
                    Color.LightGray,
                    Color.LightSalmon,
                    Color.LightSeaGreen,
                    Color.LightYellow,
                    Color.LightGreen,
                    Color.LightCoral,
                    Color.LightGoldenrodYellow,
                    Color.LightPink
                };

                Color[] colors = new Color[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    colors[i] = predefinedColors[i % predefinedColors.Length];
                }
                pieChartControl1.SetValues(values, labels, colors);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input format. Please ensure all values are numbers.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateChart();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                }
                UpdateChart();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }

        }
    }
}
