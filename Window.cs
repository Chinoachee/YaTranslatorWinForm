using System.Windows.Forms;
using System.Drawing;
namespace Translator {
    public partial class Window : Form {
        private Button _sourseButton;
        private Button _acceptButton;
        private Button _targetButton;
        public Window() {
            InitializationWindow();
            InitializationButton();
        }
        private void InitializationWindow() {
            Size = new Size(_windowWidth, _windowHeight);
            MaximumSize = new Size(Width, Height);
            MinimumSize = new Size(Width, Height);
        }
        private Button CreateButton(int width,int height,int Xposition, int Yposition) {
            return new Button() {
                Width = width,
                Height = height,
                Location = new Point(Xposition, Yposition),
                Visible = true,
            };
        }

        private void InitializationButton() {
            _sourseButton = CreateButton(_sourseButtonWidth, _sourseButtonHeight,_sourseButtonXPosition,_sourseButtonYPosition);
            _acceptButton = CreateButton(_acceptButtonWidth,_acceptButtonHeight,_acceptButtonXPosition,_acceptButtonYPosition);
            _targetButton = CreateButton(_targetButtonWidth,_targetButtonHeight,_targetButtonXPosition,_targetButtonYPosition);
            Controls.Add(_sourseButton);
            Controls.Add(_acceptButton);
            Controls.Add(_targetButton);
        }
    }
}
