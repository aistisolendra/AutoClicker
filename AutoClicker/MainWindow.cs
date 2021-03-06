using AutoClicker.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutoClicker.Models.ClickerModels;
using AutoClicker.Models.SettingsModels;
using AutoClicker.Models.VisualizationModels;
using AutoClicker.Pages;
using AutoClicker.Services;

namespace AutoClicker
{
    public partial class MainForm : Form
    {
        // Services
        private static readonly Bindable Bindable = new();
        private static readonly MouseManager MouseManager = new();
        private static readonly DrawingManager DrawingManager = new();

        // Models
        private static readonly BasicClicker BasicClicker = new();
        private static readonly Settings Settings = new();
        private static readonly Visualization Visualization = new();

        // Views
        private readonly BasicClickerView _basicClickerViewPage =
            new(BasicClicker, Bindable, MouseManager, DrawingManager, Settings, Visualization);

        private readonly AdvancedClicker _advancedClickerPage = new();
        private readonly List<UserControl> _userControls = new();

        public MainForm()
        {
            InitializeComponent();
            CreateUserControlsOnStartup();
            GetUserControls();
            ShowSingleControl(_basicClickerViewPage);
        }

        private void CreateUserControlsOnStartup()
        {
            PageContainer.Controls.Add(_basicClickerViewPage);
            PageContainer.Controls.Add(_advancedClickerPage);
        }

        private void GetUserControls()
        {
            foreach (object control in PageContainer.Controls)
            {
                if (control is not UserControl userControl)
                    throw new UserControlException("Control is not of UserControl Type");

                _userControls.Add(userControl);
            }
        }

        private void HideControls()
        {
            _userControls.ForEach(c => c.Hide());
        }

        private void ShowSingleControl<T>(T control)
        {
            HideControls();

            var userControl = _userControls.FirstOrDefault(u => u.GetType() == control.GetType());
            userControl?.Show();
        }

        private void BasicClicker_PageLoad_Click(object sender, EventArgs e)
        {
            ShowSingleControl(_basicClickerViewPage);
        }

        private void AdvancedClicker_PageLoad_Click(object sender, EventArgs e)
        {
            ShowSingleControl(_advancedClickerPage);
        }

        private void button3_Click(object sender, EventArgs e) { }
    }
}