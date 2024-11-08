using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PieChart
{
    public partial class PieChartControl : UserControl
    {
        private float[] values = new float[0];  
        private string[] labels = new string[0];
        private Color[] colors = new Color[0];

        public PieChartControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true; 
        }

        public void SetValues(float[] newValues, string[] newLabels, Color[] newColors)
        {
            values = newValues;
            labels = newLabels;
            colors = newColors;
            Invalidate(); 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (values.Length > 0)
            {
                DrawPieChart(e.Graphics);
            }
        }

        private void DrawPieChart(Graphics g)
        {
            if (values.Length == 0) return;

            float total = values.Sum();
            int size = Math.Min(this.Width, this.Height) - 50;
            Rectangle chartArea = new Rectangle(10, 10, size, size);
            float startAngle = 0;

            using (Font boldFont = new Font("Arial", 10, FontStyle.Bold))
            {
                for (int i = 0; i < values.Length; i++)
                {
                    float sweepAngle = (values[i] / total) * 360;

                    // Set gradient brush for pie slice
                    using (LinearGradientBrush brush = new LinearGradientBrush(
                        chartArea,
                        ControlPaint.Light(colors[i], 0.7f), 
                        colors[i], 
                        startAngle))
                    {
                        g.FillPie(brush, chartArea, startAngle, sweepAngle);
                    }

                    // Display label
                    float midAngle = startAngle + sweepAngle / 2;
                    float labelX = chartArea.X + (chartArea.Width / 2) + (float)(chartArea.Width / 3 * Math.Cos(midAngle * Math.PI / 180));
                    float labelY = chartArea.Y + (chartArea.Height / 2) + (float)(chartArea.Height / 3 * Math.Sin(midAngle * Math.PI / 180));
                    g.DrawString($"{labels[i]}: {values[i]}", boldFont, Brushes.Black, labelX - 15, labelY - 10);

                    startAngle += sweepAngle;
                }

                // Draw legend
                int legendX = chartArea.Right + 10;
                for (int i = 0; i < labels.Length; i++)
                {
                    g.FillRectangle(new SolidBrush(colors[i]), legendX, 20 + (i * 20), 15, 15);
                    g.DrawString(labels[i], boldFont, Brushes.Black, legendX + 20, 20 + (i * 20));
                }
            }
        }
    }
}