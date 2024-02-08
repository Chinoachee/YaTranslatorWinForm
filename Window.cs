using System;
using System.Drawing;
using System.Windows.Forms;
namespace Translator {
    public partial class Window : Form {
        private Button _sourseButton;
        private Button _acceptButton;
        private Button _saveLanguagesButton;
        private Button _targetButton;

        private TextBox _sourseTextBox;
        private TextBox _targetTextBox;

        private TextBox _sourseLanguageTextBox;
        private TextBox _targetLanguageTextBox;

        private ListBox _sourseListBox;
        private ListBox _targetListBox;

        Language lang = new Language();
        Word words = new Word();

        public Window() {
            lang.LoadLanguages("languages");
            InitializationListBox();
            InitializationTextBox();
            InitializationWindow();
            InitializationButton();
        }
        private void InitializationWindow() {
            Size = new Size(_windowWidth,_windowHeight);
            MaximumSize = new Size(Width,Height);
            MinimumSize = new Size(Width,Height);
        }
        private void InitializationButton() {
            _sourseButton = CreateButton(_sourseButtonWidth,_sourseButtonHeight,_sourseButtonXPosition,_sourseButtonYPosition,true);
            _acceptButton = CreateButton(_acceptButtonWidth,_acceptButtonHeight,_acceptButtonXPosition,_acceptButtonYPosition,true);
            _saveLanguagesButton = CreateButton(_saveLanguageButtonWidth,_saveLanguageButtonHeight,_saveLanguageButtonXPosition,_saveLanguageButtonYPosition,false);
            _targetButton = CreateButton(_targetButtonWidth,_targetButtonHeight,_targetButtonXPosition,_targetButtonYPosition,true);

            _sourseButton.Click += SourseButton_Clicked;
            _acceptButton.Click += AcceptButton_Clicked;
            _saveLanguagesButton.Click += SaveLanguageButton_Clicked;
            _targetButton.Click += TargetButton_Clicked;

            _sourseButton.TextChanged += SourseButton_TextChanged;

            Controls.Add(_sourseButton);
            Controls.Add(_acceptButton);
            Controls.Add(_saveLanguagesButton);
            Controls.Add(_targetButton);
        }
        private void InitializationTextBox() {
            _sourseTextBox = CreateTextBox(_sourseTextBoxWidth,_sourseTextBoxHeight,_sourseTextBoxXPosition,_sourseTextBoxYPosition,true,true,true);
            _targetTextBox = CreateTextBox(_targetTextBoxWidth,_targetTextBoxHeight,_targetTextBoxXPosition - 1,_targetTextBoxYPosition,true,true,true);
            _sourseLanguageTextBox = CreateTextBox(_sourseLanguageTextBoxWidth,_sourseLanguageTextBoxHeight
                ,_sourseLanguageTextBoxXPosition,_sourseLanguageTextBoxYPosition,false,false,false);
            _targetLanguageTextBox = CreateTextBox(_targetLanguageTextBoxWidth,_targetLanguageTextBoxHeight
                ,_targetLanguageTextBoxXPosition,_targetLanguageTextBoxYPosition,false,false,false);

            _sourseTextBox.TextChanged += SourseTextBox_TextChanged;

            Controls.Add(_sourseTextBox);
            Controls.Add(_targetTextBox);

            Controls.Add(_sourseLanguageTextBox);
            Controls.Add(_targetLanguageTextBox);
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
                words.LoadWords(_sourseButton.Text + _targetButton.Text);
                _sourseTextBox.ReadOnly = false;

            }
        }
        private void SaveLanguageButton_Clicked(object sender,EventArgs e) {
            if(string.IsNullOrEmpty(_sourseButton.Text)) {
                if(!string.IsNullOrEmpty(_sourseLanguageTextBox.Text)) {
                    lang.AddSourseLanguage(_sourseLanguageTextBox.Text);
                    _sourseLanguageTextBox.Text = null;
                    _sourseLanguageTextBox.Visible = false;   
                }
            } else {
                if(!string.IsNullOrEmpty(_targetLanguageTextBox.Text)) {
                    lang.AddTargetLanguage(_sourseButton.Text,_targetLanguageTextBox.Text);
                    _targetLanguageTextBox.Text = null;
                    _targetLanguageTextBox.Visible = false;
                }
            }
            _saveLanguagesButton.Visible = false;
            _acceptButton.Visible = true;
            lang.SaveLanguages("languages");
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

        private void SourseButton_TextChanged(object sender,EventArgs e) {
            _targetButton.Text = null;
            _sourseTextBox.Text = null;
            _sourseTextBox.ReadOnly = true;
        }

        private void SourseListBox_Clicked(object sender,EventArgs e) {
            if(_sourseListBox.SelectedIndex != _sourseListBox.Items.Count - 1) {
                _sourseButton.Text = _sourseListBox.SelectedItem.ToString();
                if(!_acceptButton.Visible) {
                    _acceptButton.Visible = true;
                    _saveLanguagesButton.Visible = false;
                }
                _sourseLanguageTextBox.Text = null;
                _sourseLanguageTextBox.Visible = false;
            } else {
                _sourseButton.Text = null;
                _acceptButton.Visible = false;
                _saveLanguagesButton.Visible = true;
                _sourseLanguageTextBox.Visible = true;
            }
            _sourseListBox.Visible = SwitchListBox(_sourseListBox);
        }
        private void TargetListBox_Clicked(object sender,EventArgs e) {
            if(_targetListBox.SelectedIndex != _targetListBox.Items.Count - 1) {
                _targetButton.Text = _targetListBox.SelectedItem.ToString();
                if(!_acceptButton.Visible) {
                    _acceptButton.Visible = true;
                    _saveLanguagesButton.Visible = false;
                }
                _targetLanguageTextBox.Text = null;
                _targetLanguageTextBox.Visible = false;
            } else {
                _targetButton.Text = null;
                _acceptButton.Visible = false;
                _saveLanguagesButton.Visible = true;
                _targetLanguageTextBox.Visible = true;
            }
            _targetListBox.Visible = SwitchListBox(_targetListBox);
        }

        private void SourseTextBox_TextChanged(object sender, EventArgs e) {
            _targetTextBox.Text = "Добавить новое слово";
            foreach(string word in words.GetSourseWords()) {
                if(_sourseTextBox.Text == word) {
                    _targetTextBox.Text = words.GetTargetWord(_sourseTextBox.Text);
                    break;
                }
            }
            if(string.IsNullOrEmpty(_sourseTextBox.Text)) _targetTextBox.Text = null;
        }

        private bool SwitchListBox(ListBox listBox) {
            return !listBox.Visible;
        }
        private Button CreateButton(int width,int height,int Xposition,int Yposition,bool isVisible) {
            return new Button() {
                Width = width,
                Height = height,
                Location = new Point(Xposition,Yposition),
                Visible = isVisible,
            };
        }
        private TextBox CreateTextBox(int width,int height,int Xposition,int Yposition,bool isVisible,bool isMultiLine,bool isReadOnly) {
            return new TextBox() {
                Width = width,
                Height = height,
                Location = new Point(Xposition,Yposition),
                Visible = isVisible,
                Multiline = isMultiLine,
                ReadOnly = isReadOnly,
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
//Добавить реализацию при смене sourselanguage убирать из targetlanguage язык //complete
//Добавить реализацию при смене языка запрета на запись слово в soruseTextBox //complete
//Добавить реализацию загрузку из файла при нажатии на acceptButton //complete
//Добавить реализацию добавления языков sourse/target  //complete
//Добавить реализацию добавления слов sourse/target
//Добавить реализацию сохранения файлов/загрузки файлов //complete 50/50