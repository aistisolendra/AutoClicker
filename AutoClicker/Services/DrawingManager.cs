using System.Drawing;
using System.Windows.Forms;
using AutoClicker.Models.ClickerModels;

namespace AutoClicker.Services
{
    public class DrawingManager
    {
        public void Visualize(Form visualizationForm, Bounds bounds, Color color)
        {
            if (visualizationForm == null || visualizationForm.IsDisposed)
                visualizationForm = CreateForm(color);

            DrawBounds(visualizationForm, bounds);
            AddCloseButton(visualizationForm);

            visualizationForm.ShowDialog();
        }

        private static void DrawBounds(Control visualizationForm, Bounds bounds)
        {
            visualizationForm.Paint += (sender, e) =>
            {
                var rectangle = new Rectangle(bounds.Left, bounds.Top, bounds.Right - bounds.Left,
                    bounds.Bot - bounds.Top);
                e.Graphics.DrawRectangle(Pens.Red, rectangle);
            };
        }

        private void AddCloseButton(Form visualizationForm)
        {
            var btn = new Button
            {
                Text = "Close",
                Size = new Size(100, 100)
            };

            btn.Click += (sender, args) => { CloseForm(visualizationForm); };
            btn.Location = new Point(visualizationForm.Width - btn.Width, 0);

            visualizationForm.Controls.Add(btn);
        }

        private static Form CreateForm(Color color)
        {
            return new()
            {
                TransparencyKey = color,
                FormBorderStyle = FormBorderStyle.None,
                Bounds = Screen.PrimaryScreen.Bounds,
                TopMost = true,
                WindowState = FormWindowState.Maximized
            };
        }

        private static void CloseForm(Form form)
        {
            form.Close();
        }
    }
}