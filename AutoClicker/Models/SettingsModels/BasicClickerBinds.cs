using AutoClicker.Enums;
using AutoClicker.Services;
using Keys = System.Windows.Forms.Keys;

namespace AutoClicker.Models.SettingsModels
{
    public class BasicClickerBinds
    {
        public KeyboardManager StartCheckingBind { get; set; }
        public KeyboardManager CopyPositionBind { get; set; }
        public KeyboardManager StartBind { get; set; }
        public KeyboardManager StopBind { get; set; }
        public KeyboardManager VisualizeBind { get; set; }
        public KeyboardManager ClickPositionBind { get; set; }

        public BasicClickerBinds()
        {
            StartCheckingBind = new KeyboardManager();
            CopyPositionBind = new KeyboardManager();
            StartBind = new KeyboardManager();
            StopBind = new KeyboardManager();
            VisualizeBind = new KeyboardManager();
            ClickPositionBind = new KeyboardManager();
            OnCreationSetBinds();
        }

        private void OnCreationSetBinds()
        {
            StartCheckingBind.RegisterHotKey(KeyboardModifiers.Control, Keys.F5);
            CopyPositionBind.RegisterHotKey(KeyboardModifiers.Control, Keys.F6);
            StartBind.RegisterHotKey(KeyboardModifiers.Control, Keys.F1);
            StopBind.RegisterHotKey(KeyboardModifiers.Control, Keys.F2);
            VisualizeBind.RegisterHotKey(KeyboardModifiers.Control, Keys.F3);
            ClickPositionBind.RegisterHotKey(KeyboardModifiers.Control, Keys.F4);
        }
    }
}