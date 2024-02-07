using System;
using System.Drawing;
using System.Windows.Forms;
namespace Translator {
    public partial class Window : Form {
        private Button _sourseButton;
        private Button _acceptButton;
        private Button _targetButton;

        private TextBox _sourseTextBox;
        private TextBox _targetTextBox;

        private ListBox _sourseListBox;
        private ListBox _targetListBox;

        Language lang = new Language();
        Word words = new Word();

        public Window() {
            lang.AddSourseLanguage("Русский");
            lang.AddSourseLanguage("Английский");
            lang.AddSourseLanguage("Французский");
            lang.AddSourseLanguage("Японский");

            lang.AddTargetLanguage("Русский","Английский");
            lang.AddTargetLanguage("Русский","Французский");
            lang.AddTargetLanguage("Русский","Японский");

            lang.AddTargetLanguage("Английский","Русский");
            lang.AddTargetLanguage("Английский","Французский");
            lang.AddTargetLanguage("Английский","Японский");

            words.AddSourseWord("Слово");
            words.AddSourseWord("Ниггер");
            words.AddTargetWord("Слово","Word");
            words.AddTargetWord("Ниггер","Nigger");

            InitializationWindow();
            InitializationButton();
            InitializationListBox();
            InitializationTextBox();
        }
        private void InitializationWindow() {
            Size = new Size(_windowWidth,_windowHeight);
            MaximumSize = new Size(Width,Height);
            MinimumSize = new Size(Width,Height);
        }
        private void InitializationButton() {
            _sourseButton = CreateButton(_sourseButtonWidth,_sourseButtonHeight,_sourseButtonXPosition,_sourseButtonYPosition);
            _acceptButton = CreateButton(_acceptButtonWidth,_acceptButtonHeight,_acceptButtonXPosition,_acceptButtonYPosition);
            _targetButton = CreateButton(_targetButtonWidth,_targetButtonHeight,_targetButtonXPosition,_targetButtonYPosition);

            _sourseButton.Click += SourseButton_Clicked;
            _acceptButton.Click += AcceptButton_Clicked;
            _targetButton.Click += TargetButton_Clicked;

            Controls.Add(_sourseButton);
            Controls.Add(_acceptButton);
            Controls.Add(_targetButton);
        }
        private void InitializationTextBox() {
            _sourseTextBox = CreateTextBox(_sourseTextBoxWidth,_sourseTextBoxHeight,_sourseTextBoxXPosition,_sourseTextBoxYPosition);
            _targetTextBox = CreateTextBox(_targetTextBoxWidth,_targetTextBoxHeight,_targetTextBoxXPosition - 1,_targetTextBoxYPosition);

            _sourseTextBox.TextChanged += SourseTextBox_TextChanged;

            Controls.Add(_sourseTextBox);
            Controls.Add(_targetTextBox);
        }
        private void InitializationListBox() {
            _sourseListBox = CreateListBox(_sourseListBoxWidth - 2,_sourseListBoxHeight,_sourseListBoxXPosition + 1,_sourseListBoxYPosition);
            _targetListBox = CreateListBox(_targetListBoxWidth - 2,_targetListBoxHeight,_targetListBoxXPosition + 1,_targetListBoxYPosition);

            _sourseListBox.Click += SourseListBox_Clicked;
            _targetListBox.Click += TargetListBox_Clicked;

            Controls.Add(_sourseListBox);
            Controls.Add(_targetListBox);
        }

        private void SourseButton_Clicked(object sender,EventArgs e) {
            _sourseListBox.Items.Clear();
            _sourseListBox.Items.AddRange(lang.GetSourseLanguages().ToArray());
            _sourseListBox.Items.Add("Добавить новый язык");
            _sourseListBox.Height = _sourseListBox.Items.Count * 15;
            _sourseListBox.Visible = SwitchListBox(_sourseListBox);
            _targetListBox.Visible = false;
        }
        private void AcceptButton_Clicked(object sender,EventArgs e) {
            if(!string.IsNullOrEmpty(_sourseListBox.Text) && !string.IsNullOrEmpty(_targetButton.Text)) {
                _sourseTextBox.ReadOnly = false;
            }
        }
        private void TargetButton_Clicked(object sender,EventArgs e) {
            if(!string.IsNullOrEmpty(_sourseButton.Text)) {
                _targetListBox.Items.Clear();
                _targetListBox.Items.AddRange(lang.GetTargetLanguages(_sourseButton.Text).ToArray());
                _targetListBox.Items.Add("Добавить новый язык");
                _targetListBox.Height = _targetListBox.Items.Count * 15;
                _targetListBox.Visible = SwitchListBox(_targetListBox);
                _sourseListBox.Visible = false;
            }
        }

        private void SourseListBox_Clicked(object sender,EventArgs e) {
            if(_sourseListBox.SelectedIndex != _sourseListBox.Items.Count - 1) {
                _sourseButton.Text = _sourseListBox.SelectedItem.ToString();
            }
            _sourseListBox.Visible = SwitchListBox(_sourseListBox);
        }
        private void TargetListBox_Clicked(object sender,EventArgs e) {
            if(_targetListBox.SelectedIndex != _targetListBox.Items.Count - 1) {
                _targetButton.Text = _targetListBox.SelectedItem.ToString();
            }
            _targetListBox.Visible = SwitchListBox(_targetListBox);
        }

        private void SourseTextBox_TextChanged(object sender, EventArgs e) {
            foreach(string word in words.GetSourseWords()) {
                if(_sourseTextBox.Text == word) {
                    _targetTextBox.Text = words.GetTargetWord(_sourseTextBox.Text);
                    break;
                } else {
                    _targetTextBox.Text = "Добавить новое слово";
                }
            }
            if(string.IsNullOrEmpty(_sourseTextBox.Text)) {
                _targetTextBox.Text = null;
            }
        }

        private bool SwitchListBox(ListBox listBox) {
            return !listBox.Visible;
        }
        private Button CreateButton(int width,int height,int Xposition,int Yposition) {
            return new Button() {
                Width = width,
                Height = height,
                Location = new Point(Xposition,Yposition),
                Visible = true,
            };
        }
        private TextBox CreateTextBox(int width,int height,int Xposition,int Yposition) {
            return new TextBox() {
                Width = width,
                Height = height,
                Location = new Point(Xposition,Yposition),
                Visible = true,
                Multiline = true,
                ReadOnly = true,
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
//Добавить реализацию при смене sourselanguage убирать из targetlanguage язык
//Добавить реализацию при смене языка запрета на запись слово в soruseTextBox
//Добавить реализацию загрузку из файла при нажатии на acceptButton
//Добавить реализацию добавления языков sourse/target
//Добавить реализацию добавления слов sourse/target
//Добавить реализацию сохранения файлов/загрузки файлов