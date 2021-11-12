// Look
// Oh, look at what you've done now to me 
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Newtonsoft.Json.Linq;

namespace NickMovesetEditor.Windows
{
    public partial class EditMenu : Window
    {
        private List<string> _actionPath;
        private JObject _statesJson;

        public EditMenu(ref JObject statesJson, List<string> actionPath)
        {
            _actionPath = actionPath;
            _statesJson = statesJson;
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

        private void ApplyChanges(object sender, RoutedEventArgs e)
        {
            // TODO: Find a way to do this without recursion
            var textBoxes = MainLayoutGrid.Children.OfType<TextBox>();
            
            foreach (var textBox in textBoxes)
            {
                Debug.WriteLine(textBox.Text);
                if (textBox.Name == "AtFrame")
                {
                    Helper.SetActionValue(ref _statesJson, _actionPath, "AtFrame", textBox.Text);
                }
                
                if (textBox.Name == "Radius")
                {
                    MessageBox.ShowMessage(textBox.Text);
                    Helper.SetActionValue(ref _statesJson, _actionPath, "Radius", textBox.Text);
                }
            }
            Close();
        }
        
    }
}