﻿using System;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AutoClicker.Enums;
using AutoClicker.Models;
using AutoClicker.Services;

namespace AutoClicker.Pages
{
    public partial class BasicClickerView : UserControl
    {
        private readonly BasicClicker _basicClicker;
        private readonly Bindable _bindable;
        private readonly MouseManager _mouseManager;
        private const int PositionCheckInterval = 50;

        public BasicClickerView(BasicClicker basicClicker, Bindable bindable, MouseManager mouseManager)
        {
            InitializeComponent();
            _basicClicker = basicClicker;
            _bindable = bindable;
            _mouseManager = mouseManager;
            PopulateComboBoxes();
            SetBindings();
            SetTags();
            SetEvents();
        }

        private void PopulateComboBoxes()
        {
            ButtonTypeComboBox.DataSource = Enum.GetValues(typeof(ButtonType));
            ClickTypeComboBox.DataSource = Enum.GetValues(typeof(ClickType));
        }

        private void SetTags()
        {
            RepeatTimesButton.Tag = ClickRepeatType.RepeatTimes;
            RepeatUntilStoppedButton.Tag = ClickRepeatType.RepeatUntilStopped;
        }

        private void SetEvents()
        {
            _basicClicker.CheckCursorPosition.Timer.Interval = PositionCheckInterval;
            _basicClicker.CheckCursorPosition.Timer.Tick += OnElapsedCheckPos;

            _basicClicker.ClickPosition.Timer.Interval = PositionCheckInterval;
            _basicClicker.ClickPosition.Timer.Tick += OnElapsedCheckPos;

            _basicClicker.BasicClickerTimer.Interval = 1;
            _basicClicker.BasicClickerTimer.Tick += OnElapsedClick;
        }

        private void SetBindings()
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
            ClickOptionsTopLeftNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickOptions.RandomizeClickingBounds, "Top"));
            ClickOptionsTopRightNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickOptions.RandomizeClickingBounds, "Bot"));
            ClickOptionsBotLeftNumericBox.DataBindings.Add(
                _bindable.CreateTextBind(_basicClicker.ClickOptions.RandomizeClickingBounds, "Left"));
            ClickOptionsBotRightNumericBox.DataBindings.Add(
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
            ClickOptionsTopLeftNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsTopRightNumericBox.DataBindings.Add(
                _bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsBotLeftNumericBox.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsBotRightNumericBox.DataBindings.Add(
                _bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsTopLeftLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsTopRightLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsBotLeftLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
            ClickOptionsBotRightLabel.DataBindings.Add(_bindable.CreateEnabledBindFromChecked(RandomizeXYCheckBox));
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
            _basicClicker.ClickPosition.ClickPositionType = ClickPositionType.CurrentPosition;
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
            _basicClicker.ClickTimes = (int) _basicClicker.ClickOptions.ClickType + 1;

            if (!_basicClicker.BasicClickerTimer.Enabled)
            {
                TimesClickedNumericBox.Value = 0;
                _basicClicker.BasicClickerTimer.Start();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _basicClicker.BasicClickerTimer.Stop();
        }

        private void OnElapsedCheckPos(object sender, EventArgs e)
        {
            var position = _mouseManager.GetCursorPosition();

            CursorPositionXNumericBox.Value = position.X;
            CursorPositionYNumericBox.Value = position.Y;
        }

        private void OnElapsedClick(object sender, EventArgs e)
        {
            _mouseManager.Click(_basicClicker);

            HandleClickRepeat();

            ButtonTypeLabel.Text = _basicClicker.ClickRepeat.ClickRepeatTimes.ToString();
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
    }
}