using System.Windows.Forms;
using System.Drawing;
namespace Translator {
    public class Window : Form {
        private const int _windowWidth = 600;
        private const int _windowHeight = 400;
        public Window() {
            InitializationWindow();
        }
        private void InitializationWindow() {
            Width = _windowWidth; 
            Height = _windowHeight;
            MaximumSize = new Size(Width, Height);
            MinimumSize = new Size(Width, Height);
        }
    }
}
