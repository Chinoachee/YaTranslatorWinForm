﻿using System.Windows.Forms;
using System.Drawing;
namespace Translator {
    public partial class Window : Form {
        private Button _sourseButton;
        private Button _acceptButton;
        private Button _targetButton;

        private TextBox _sourseTextBox;
        private TextBox _targetTextBox;

        private ListBox _sourseListBox;
        private ListBox _targetListBox;
        public Window() {
            InitializationWindow();
            InitializationButton();
            InitializationListBox();
            InitializationTextBox();
        }
        private void InitializationWindow() {
            Size = new Size(_windowWidth, _windowHeight);
            MaximumSize = new Size(Width, Height);
            MinimumSize = new Size(Width, Height);
        }
        private void InitializationButton() {
            _sourseButton = CreateButton(_sourseButtonWidth, _sourseButtonHeight,_sourseButtonXPosition,_sourseButtonYPosition);
            _acceptButton = CreateButton(_acceptButtonWidth,_acceptButtonHeight,_acceptButtonXPosition,_acceptButtonYPosition);
            _targetButton = CreateButton(_targetButtonWidth,_targetButtonHeight,_targetButtonXPosition,_targetButtonYPosition);
            Controls.Add(_sourseButton);
            Controls.Add(_acceptButton);
            Controls.Add(_targetButton);
        }
        private void InitializationTextBox() {
            _sourseTextBox = CreateTextBox(_sourseTextBoxWidth,_sourseTextBoxHeight,_sourseTextBoxXPosition,_sourseTextBoxYPosition,false);
            _targetTextBox = CreateTextBox(_targetTextBoxWidth,_targetTextBoxHeight,_targetTextBoxXPosition - 1,_targetTextBoxYPosition,true);
            Controls.Add(_sourseTextBox);
            Controls.Add(_targetTextBox);
        }
        private void InitializationListBox() {
            _sourseListBox = CreateListBox(_sourseListBoxWidth - 2,_sourseListBoxHeight,_sourseListBoxXPosition + 1,_sourseListBoxYPosition);
            _targetListBox = CreateListBox(_targetListBoxWidth - 2,_targetListBoxHeight,_targetListBoxXPosition + 1,_targetListBoxYPosition);
            Controls.Add(_sourseListBox);
            Controls.Add(_targetListBox);
        }
        private Button CreateButton(int width,int height,int Xposition,int Yposition) {
            return new Button() {
                Width = width,
                Height = height,
                Location = new Point(Xposition,Yposition),
                Visible = true,
            };
        }
        private TextBox CreateTextBox(int width,int height,int Xposition,int Yposition,bool readOnly) {
            return new TextBox() {
                Width = width,
                Height = height,
                Location = new Point(Xposition,Yposition), 
                Visible = true,
                Multiline = true,
                ReadOnly = readOnly,
            };
        }
        private ListBox CreateListBox(int width,int height,int Xposition,int Yposition) {
            return new ListBox() {
                MaximumSize = new Size(width,height),
                MinimumSize = new Size(width,20),
                Location = new Point(Xposition,Yposition),
                Visible = false,
            };
        }
    }
}
