using System.Windows.Forms;
using AutoClicker.Services;

namespace AutoClicker.Models
{
    public class BasicClickerBinds
    {
        public KeyboardManager StartCheckingBind { get; set; }
        public KeyboardManager CopyPositionBind { get; set; }
        public KeyboardManager StartBind { get; set; }
        public KeyboardManager StopBind { get; set; }
        public KeyboardManager VisualizeBind { get; set; }

        public BasicClickerBinds()
        {
            StartCheckingBind = new KeyboardManager();
            CopyPositionBind = new KeyboardManager();
            StartBind = new KeyboardManager();
            StopBind = new KeyboardManager();
            VisualizeBind = new KeyboardManager();
            OnCreateBind();
        }

        private void OnCreateBind()
        {
            StartCheckingBind.RegisterHotKey(ModifierKeys.Control, Keys.F5);
            CopyPositionBind.RegisterHotKey(ModifierKeys.Control, Keys.F6);
            StartBind.RegisterHotKey(ModifierKeys.Control, Keys.F1);
            StopBind.RegisterHotKey(ModifierKeys.Control, Keys.F2);
            VisualizeBind.RegisterHotKey(ModifierKeys.Control, Keys.F3);
        }
    }
}