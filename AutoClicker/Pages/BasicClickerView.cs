using System;
using System.Timers;
using System.Windows.Forms;
using AutoClicker.Enums;
using AutoClicker.Models;
using AutoClicker.Models.ClickerModels;
using AutoClicker.Models.EventModels;
using AutoClicker.Models.SettingsModels;
using AutoClicker.Services;

namespace AutoClicker.Pages
{
    public partial class BasicClickerView : UserControl
    {
        private readonly BasicClicker _basicClicker;
        private readonly Bindable _bindable;
        private readonly MouseManager _mouseManager;
        private readonly Settings _settings;

        private const int PositionCheckInterval = 50;

        public BasicClickerView(BasicClicker basicClicker, Bindable bindable, MouseManager mouseManager,
            KeyboardManager keyboardManager, Settings settings)
        {
            InitializeComponent();
            _basicClicker = basicClicker;
            _bindable = bindable;
            _mouseManager = mouseManager;
            _settings = settings;

            PopulateComboBoxes();
            HandleModelBindings();
            HandleButtonTags();
            HandleTimerEvents();
            HandleBindEvents();
        }

        private void HandleBindEvents()
        {
            _settings.BasicClickerBinds.StartBind.KeyPressed += Bind_StartPressed;
        }

        private void PopulateComboBoxes()
        {
            ButtonTypeComboBox.DataSource = Enum.GetValues(typeof(ButtonType));
            ClickTypeComboBox.DataSource = Enum.GetValues(typeof(ClickType));
        }

        private void HandleButtonTags()
        {
            RepeatTimesButton.Tag = ClickRepeatType.RepeatTimes;
            RepeatUntilStoppedButton.Tag = ClickRepeatType.RepeatUntilStopped;
        }

        private void HandleTimerEvents()
        {
            _basicClicker.CheckCursorPosition.Timer.Interval = PositionCheckInterval;
            _basicClicker.CheckCursorPosition.Timer.Tick += OnElapsedCheckPos;

            _basicClicker.ClickPosition.Timer.Interval = PositionCheckInterval;
            _basicClicker.ClickPosition.Timer.Tick += OnElapsedGetPosClick;

            _basicClicker.BasicClickerTimer.Tick += OnElapsedClick;
        }

        private void HandleModelBindings()
        {
            SetTimeIntervalBindings();
            SetCursorPositionCheckBindings();
            SetRepeatBindings();
            SetClickPositionBindings();
            SetClickOptionBindings();
            SetStatisticsBinds();
            SetVisualBinds();
        }

        private void SetTimeIntervalBindings()
        {
            HoursNumericBox.DataBindings.Add(_bindable.CreateTextBind(_basicClicker.TimeInterval, "Hours"));
            MinutesNumericBox.DataBindings.Add(_bindable.CreateTextBind(_basicClicker.TimeInterval, "Minutes"));
            SecondsNumericBox.DataBindings.Add(_bindable.CreateTextBind(_basicClicker.TimeInterval, "Seconds"));
            MilisecondsNumericBox.DataBindings.Add(_bindable.CreateTextBind(_basicClicker.TimeInterval,
                "Milliseconds"));
        }

        private void SetRepeatBindings()
        {
            RepeatTimesNumericBox.DataBindings.Add(_bindable.CreateTextBind(_basicClicker.ClickRepeat,
                "ClickRepeatTimes"));
        }

        private void SetCursorPositionCheckBindings()
        {
            CursorPositionXNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.CheckCursorPosition.MousePos, "X"));
            CursorPositionYNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.CheckCursorPosition.MousePos, "Y"));
        }

        private void SetClickPositionBindings()
        {
            ClickPositionTopNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickPosition.ClickPositionBounds, "Top"));
            ClickPositionBotNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickPosition.ClickPositionBounds, "Bot"));
            ClickPositionLeftNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickPosition.ClickPositionBounds, "Left"));
            ClickPositionRightNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickPosition.ClickPositionBounds, "Right"));
            ClickPositionXNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickPosition.MousePositionToClick, "X"));
            ClickPositionYNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickPosition.MousePositionToClick, "Y"));
        }

        private void SetClickOptionBindings()
        {
            ButtonTypeComboBox.DataBindings.Add(_bindable.CreateSelectedBind(_basicClicker.ClickOptions,
                "ButtonType"));
            ButtonTypeLabel.DataBindings.Add(_bindable.CreateTextBind(_basicClicker.ClickOptions,
                "ButtonType"));
            ClickWorkingCheckBox.DataBindings.Add(_bindable.CreateCheckedBind(_basicClicker.ClickOptions,
                "ClickWorkingEnabled"));
            ClickWorkingPercentageNumericBox.DataBindings.Add(_bindable.CreateTextBind(_basicClicker.ClickOptions,
                "ClickWorkingPercentage"));
            ClickTypeComboBox.DataBindings.Add(_bindable.CreateSelectedBind(_basicClicker.ClickOptions, "ClickType"));
            TimeBetweenClicksNumericBox.DataBindings.Add(_bindable.CreateTextBind(_basicClicker.ClickOptions,
                "TimeBetweenClickTypes"));
            RandomizeXYCheckBox.DataBindings.Add(_bindable.CreateCheckedBind(_basicClicker.ClickOptions,
                "RandomizeClickingEnabled"));
            ClickOptionsTopNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickOptions.RandomizeClickingBounds, "Top"));
            ClickOptionsBotNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickOptions.RandomizeClickingBounds, "Bot"));
            ClickOptionsLeftNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickOptions.RandomizeClickingBounds, "Left"));
            ClickOptionsRightNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickOptions.RandomizeClickingBounds, "Right"));
        }

        private void SetVisualBinds()
        {
            // Click Repeat
            TimesLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RepeatTimesButton));
            RepeatTimesNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RepeatTimesButton));

            // Click Position
            ClickPositionTopNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickBoundsButton));
            ClickPositionBotNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickBoundsButton));
            ClickPositionLeftNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickBoundsButton));
            ClickPositionRightNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickBoundsButton));
            ClickPositionTopLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickBoundsButton));
            ClickPositionBotLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickBoundsButton));
            ClickPositionLeftLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickBoundsButton));
            ClickPositionRightLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickBoundsButton));
            ClickPositionXNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickOnPositionButton));
            ClickPositionYNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickOnPositionButton));
            ClickPositionXLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickOnPositionButton));
            ClickPositionYLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickOnPositionButton));
            GetPositionButton.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickOnPositionButton));

            // Click Options
            ClickWorkingPercentageNumericBox.DataBindings.Add(
                _bindable.CreateEnabledBindFromChecked(ClickWorkingCheckBox));
            ClickWorkingPercentageLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickWorkingCheckBox));
            RandomizeXYCheckBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(ClickOnPositionButton));
            ClickOptionsTopNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsBotNumericBox.DataBindings.Add(
                _bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsLeftNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsRightNumericBox.DataBindings.Add(
                _bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsTopLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsBotLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsLeftLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsRightLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
        }

        private void SetStatisticsBinds()
        {
            TimesClickedNumericBox.DataBindings.Add(_bindable.CreateTextBind(_basicClicker.BasicStatistics,
                "TimesClicked"));
        }

        private void RepeatTimesButton_CheckedChanged(object sender, EventArgs e)
        {
            RepeatUntilStoppedButton.Checked = !RepeatTimesButton.Checked;
            _basicClicker.ClickRepeat.ClickRepeatType = ClickRepeatType.RepeatTimes;
        }

        private void RepeatUntilStoppedButton_CheckedChanged(object sender, EventArgs e)
        {
            RepeatTimesButton.Checked = !RepeatUntilStoppedButton.Checked;
            _basicClicker.ClickRepeat.ClickRepeatType = ClickRepeatType.RepeatUntilStopped;
        }

        private void ClickCurrentPositionButton_Click(object sender, EventArgs e)
        {
            bool state = !ClickCurrentPositionButton.Checked;
            ClickBoundsButton.Checked = state;
            ClickOnPositionButton.Checked = state;
            _basicClicker.ClickPosition.ClickPositionType = ClickPositionType.CurrentPosition;
        }

        private void ClickBoundsButton_Click(object sender, EventArgs e)
        {
            bool state = !ClickBoundsButton.Checked;
            ClickCurrentPositionButton.Checked = state;
            ClickOnPositionButton.Checked = state;
            _basicClicker.ClickPosition.ClickPositionType = ClickPositionType.BetweenBounds;
        }

        private void ClickOnPositionButton_Click(object sender, EventArgs e)
        {
            bool state = !ClickOnPositionButton.Checked;
            ClickCurrentPositionButton.Checked = state;
            ClickBoundsButton.Checked = state;
            _basicClicker.ClickPosition.ClickPositionType = ClickPositionType.OnPosition;
        }

        private void CheckCursorPositionButton_Click(object sender, EventArgs e)
        {
            bool state = _basicClicker.CheckCursorPosition.Timer.Enabled;
            if (!state)
            {
                CheckCursorPositionButton.Text = "Click To Stop";
                _basicClicker.CheckCursorPosition.Timer.Start();
            }
            else
            {
                CheckCursorPositionButton.Text = "Start Checking";
                _basicClicker.CheckCursorPosition.Timer.Stop();
            }
        }

        private void CheckCursorCopyPosition_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(
                $"{_basicClicker.CheckCursorPosition.MousePos.X},{_basicClicker.CheckCursorPosition.MousePos.Y}");
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            HandleClick();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _basicClicker.BasicClickerTimer.Stop();
        }

        private void HandleClick()
        {
            _basicClicker.ClickTimes = (int) _basicClicker.ClickOptions.ClickType + 1;
            _basicClicker.BasicClickerTimer.Interval = _basicClicker.TimeInterval.ToMs();

            if (!_basicClicker.BasicClickerTimer.Enabled)
            {
                TimesClickedNumericBox.Value = 0;
                _basicClicker.BasicClickerTimer.Start();
            }
        }

        private void HandleClickRepeat()
        {
            switch (_basicClicker.ClickRepeat.ClickRepeatType)
            {
                case ClickRepeatType.RepeatUntilStopped:
                    ++TimesClickedNumericBox.Value;
                    break;

                case ClickRepeatType.RepeatTimes:
                {
                    decimal temp = ++TimesClickedNumericBox.Value;

                    if (temp >= _basicClicker.ClickRepeat.ClickRepeatTimes)
                        _basicClicker.BasicClickerTimer.Stop();
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GetPositionButton_Click(object sender, EventArgs e)
        {
            if (!_basicClicker.ClickPosition.Timer.Enabled)
                _basicClicker.ClickPosition.Timer.Start();
        }

        private void Bind_StartPressed(object sender, KeyPressedEventArgs e)
        {
            HandleClick();
        }

        private void OnElapsedCheckPos(object sender, EventArgs e)
        {
            var position = _mouseManager.GetCursorPosition();

            CursorPositionXNumericBox.Value = position.X;
            CursorPositionYNumericBox.Value = position.Y;
        }

        private void OnElapsedGetPosClick(object sender, EventArgs e)
        {
            var position = _mouseManager.GetCursorPosition();

            ClickPositionXNumericBox.Value = position.X;
            ClickPositionYNumericBox.Value = position.Y;
        }

        private void OnElapsedClick(object sender, EventArgs e)
        {
            _mouseManager.Click(_basicClicker);

            HandleClickRepeat();
        }
    }
}