using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace NickMovesetEditor.Windows
{
    public partial class GameDirSelector
    {
        private string _gamePath = "path to game executable";
        public GameDirSelector()
        {
            InitializeComponent();
        }

        private void FilePrompt(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == true)
            {
                _gamePath= openFileDialog.FileName;
                GamePathXaml.Text = _gamePath;
            }
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            if (IsGamePathValid(GamePathXaml.Text))
            {
                MainWindow mainWindow = new MainWindow();
                MainWindow.GamePath = Path.GetDirectoryName(GamePathXaml.Text);
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.ShowMessage("Game path is not valid\n" +
                                       "Is the companion plugin installed?\n");          
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

        private static bool IsGamePathValid(string gamePath)
        {
            var gameFolder = Path.GetDirectoryName(gamePath) ?? gamePath;
            var gameExecutable = Path.GetFileName(gamePath);
            string movesetPath = Path.Combine(gameFolder, "BepInEx", "Movesets");
            string companionPluginPath = Path.Combine(gameFolder, "BepInEx", "plugins", "Coal-NMECompanion");
            return Directory.Exists(movesetPath) && Directory.Exists(companionPluginPath) && gameExecutable == "Nickelodeon All-Star Brawl.exe";
        }
    } 
}