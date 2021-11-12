using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NickMovesetEditor.Windows
{
    public partial class ListSelector
    {
        private string _type;
        public ListSelector(string type)
        {
            InitializeComponent();
            _type = type;
            switch (type)
            {
                case "character":
                {
                    foreach (var character in NmeLib.NmeLib.CharacterList)
                    {
                        ItemListBox.Items.Add(character.Value);
                    }
                    SelectTitle.Text = "Select Character";
                    
                    break;
                }
                case "state":
                {
                    foreach (var state in MainWindow.CharacterStates)
                    {
                        ItemListBox.Items.Add(state);
                    }
                    SelectTitle.Text = "Select State";

                    break;
                }
            }
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
        
        private void SelectItem(object sender, RoutedEventArgs e)
        {
            // TODO: <3
            switch (_type)
            {
                case "character":
                {
                    var characterName = ItemListBox.SelectedItem ?? "Spongebob";
                    MainWindow.CharacterName = characterName.ToString();
                    BitmapImage characterIcon = new BitmapImage(new Uri("../Images/CharacterIcons/" + characterName + ".png", UriKind.Relative));
                    MainWindow.CharacterIcon = characterIcon;
                    Close();

                    break;
                }
                case "state":
                {
                    var characterState = ItemListBox.SelectedItem ?? "Select State";
                    MainWindow.CharacterState = characterState.ToString();
                    Close();
                
                    break;
                }
            }
        }

    } 
}