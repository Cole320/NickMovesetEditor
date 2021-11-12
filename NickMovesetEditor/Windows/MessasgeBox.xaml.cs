using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace NickMovesetEditor.Windows
{
    public partial class MessageBox : Window
    {
        public MessageBox()
        {
            InitializeComponent();
        }
        
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #pragma warning disable 108,114
        public static void ShowMessage(string message)
        {
            MessageBox messageBox = new MessageBox();
            messageBox.Message.Text = message;
            messageBox.ShowDialog();
        }
    }
}