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
                    Helper.SetActionValue(ref _statesJson, _actionPath, "Radius", textBox.Text);
                }

                if (textBox.Name.Contains("SetFloatId"))
                {
                    // TODO: make this better
                    // THIS IS SO BAD-
                    List<string> tempActionPath = new List<string>(_actionPath); 
                    tempActionPath.Add("Sets");
                    tempActionPath.Add(textBox.Name[textBox.Name.Length - 1].ToString());
                    tempActionPath.Add("Source");
                    Debug.WriteLine(tempActionPath.ToString());
                    Helper.SetActionValue(ref _statesJson, tempActionPath, "Value", textBox.Text);
                }
            }
            Close();
        }
        
    }
}