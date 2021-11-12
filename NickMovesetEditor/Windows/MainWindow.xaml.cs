// Now, now, you need to calm down <3
// What good's this energy, when you devote it to me,
// Why not be a little more friendly?
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using NmeLib;

namespace NickMovesetEditor.Windows
{
    public partial class MainWindow
    {
        // global variable spam because i can't think of a cleaner way to implement this
        public static string? GamePath;
        public static BitmapImage? CharacterIcon;
        public static string CharacterName = "Select Character";
        public static string CharacterState = "Select State";
        public static int StateNumber;
        public static int StateActionNumber;
        public static int StateSubActionNumber;
        public static string MovesetPath;
        public static List<string> StateTags = new List<string>();
        public static List<string> CharacterStates = new List<string>();
        public static List<string> StateSubActions = new List<string>();
        private JObject _statesJson;

        public MainWindow()
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
        
        private void Open(object sender, RoutedEventArgs e)
        {
            // TODO: Make this less bad somehow, this feels like bad UX
            GameDirSelector gameDirSelector = new GameDirSelector();
            gameDirSelector.Show();
            Close();
        }

        public static T KeyByValue<T, W>(Dictionary<T, W> dict, W val)
        {
            T key = default;
            foreach (KeyValuePair<T, W> pair in dict)
            {
                if (EqualityComparer<W>.Default.Equals(pair.Value, val))
                {
                    key = pair.Key;
                    break;
                }
            }
            return key;
        }

        private string GetCharacterPath(string characterName)
        {
            return GamePath + "\\BepInEx\\Movesets\\char_" + KeyByValue(NmeLib.NmeLib.CharacterList, characterName) + ".bsa";
        }

        private void GetStateNumber()
        {
            for (int i = 0; i < _statesJson["States"].ToList().Count; i++)
            {
                if (_statesJson["States"][i]["Id"].ToString() == CharacterState)
                {
                    StateNumber = i;
                }
            }
        }

        private void StatesFromJson()
        {
            for (int i = 0; i < _statesJson["States"].ToArray().Length; i++)
            {
                CharacterStates.Add(_statesJson["States"][i]["Id"].ToString());
            }
        }
        
        private void SelectCharacter(object sender, RoutedEventArgs e)
        {
            ListSelector listSelector = new ListSelector("character");
            listSelector.ShowDialog();
            
            MovesetPath = GetCharacterPath(CharacterName);
            if (File.Exists(MovesetPath))
            {
                SelectedCharacter.Content = CharacterName;
                SelectedCharacterIcon.Source = CharacterIcon;

                _statesJson = NmeLib.NmeLib.Deserialize(MovesetPath);
                StatesFromJson();
                ReloadState();
            }
            else
            {
                MessageBox.ShowMessage("Character File Not Found.\n" +
                                       "Make Sure the Companion Plugin is Installed,\n" +
                                       "And You've Played with the Selected Character");
            }
        }

        private void SelectAction(object sender, RoutedEventArgs e)
        {
            SubActionList.Items.Clear();
            // Stupid check to prevent UB
            StateActionNumber = (ActionList.SelectedIndex == -1) ? 0 : ActionList.SelectedIndex;

            // This cast hack is required to access the ".ContainsKey" method
            JObject tempActions = (JObject) _statesJson["States"][StateNumber]["State"]["Timeline"][StateActionNumber]["Action"];
            
            // Check if "Actions" element exists, if so iterate through it and display it's contents
            if (tempActions.ContainsKey("Actions"))
            {
                foreach (var stateSubAction in _statesJson["States"][StateNumber]["State"]["Timeline"][StateActionNumber]["Action"]["Actions"])
                {
                    SubActionList.Items.Add(stateSubAction["TID"]);
                }
            }
            SubActionList.UnselectAll();
        }

        private void SelectSubAction(object sender, RoutedEventArgs e)
        {
            SubSubActionList.Items.Clear();
            
            // Stupid check to prevent UB
            StateSubActionNumber = (SubActionList.SelectedIndex == -1) ? 0 : SubActionList.SelectedIndex;
            
            // This cast hack is required to access the ".ContainsKey" method
            JObject tempActions = (JObject) _statesJson["States"][StateNumber]["State"]["Timeline"][StateActionNumber]["Action"]["Actions"][StateSubActionNumber];
            
            // Check if "Actions" element exists, if so iterate through it and display it's contents
            if (tempActions.ContainsKey("Actions"))
            {
                foreach (var stateSubSubAction in _statesJson["States"][StateNumber]["State"]["Timeline"][StateActionNumber]["Action"]["Actions"][StateSubActionNumber]["Actions"])
                {
                    SubSubActionList.Items.Add(stateSubSubAction["TID"]);
                }        
            }
            SubSubActionList.UnselectAll();
        }

        private void SelectState(object sender, RoutedEventArgs e)
        {
            ListSelector listSelector = new ListSelector("state");
            listSelector.ShowDialog();
            SelectedState.Content = CharacterState;
            GetStateNumber();
            ActionList.Items.Clear();
            
            foreach (var stateTag in _statesJson["States"][StateNumber]["Tags"])
            {
                StateTags.Add(stateTag.ToString());
            }

            foreach (var stateAction in _statesJson["States"][StateNumber]["State"]["Timeline"])
            {
                var frameNumber = stateAction["AtFrame"];
                ActionList.Items.Add( stateAction["Action"]["TID"] + " (Frame " + frameNumber + ")");
            }

        }

        private void ReloadState()
        {
            ActionList.Items.Clear();
            
            foreach (var stateTag in _statesJson["States"][StateNumber]["Tags"])
            {
                StateTags.Add(stateTag.ToString());
            }

            foreach (var stateAction in _statesJson["States"][StateNumber]["State"]["Timeline"])
            {
                var frameNumber = stateAction["AtFrame"];
                ActionList.Items.Add( stateAction["Action"]["TID"] + " (Frame " + frameNumber + ")");
            }
        }

        private void EditAction(object sender, RoutedEventArgs e)
        {
            int selectedItem;
            List<string> actionPath;
            ListBox currentActionList;
            
            if (ActionList.SelectedIndex != -1)
            {
                if (SubActionList.SelectedIndex != -1)
                {
                    if (SubSubActionList.SelectedIndex != -1)
                    {
                        selectedItem = SubSubActionList.SelectedIndex;
                        actionPath = new List<string> {"States", StateNumber.ToString(), "State", "Timeline", StateActionNumber.ToString(), "Action", "Actions", StateSubActionNumber.ToString(), "Actions", selectedItem.ToString()};
                        currentActionList = SubSubActionList;
                    }
                    else
                    {
                        selectedItem = SubActionList.SelectedIndex;
                        actionPath = new List<string> {"States", StateNumber.ToString(), "State", "Timeline", StateActionNumber.ToString(), "Action", "Actions", StateSubActionNumber.ToString()};
                        currentActionList = SubActionList;
                    }
                }
                else
                {
                    selectedItem = ActionList.SelectedIndex;
                    actionPath = new List<string> {"States", StateNumber.ToString(), "State", "Timeline", StateActionNumber.ToString()};
                    currentActionList = ActionList;
                }
            }
            else
            {
                MessageBox.ShowMessage("Please select an action");
                return;
            }
            EditMenu editMenu = new EditMenu(ref _statesJson, actionPath);
            editMenu.MainLayoutGrid.Children.Clear();
            editMenu.Title.Text = currentActionList.SelectedItem.ToString();

            // If is root, add option to edit frame data 
            if (Helper.IsActionRoot(actionPath))
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = "AtFrame:",
                    Style = editMenu.MainLayoutGrid.FindResource("LeftTextBlock") as Style
                };

                TextBox textBox = new TextBox
                {
                    Name = "AtFrame",
                    Text = Helper.GetActionValue(_statesJson, actionPath, "AtFrame"),
                    Style = editMenu.MainLayoutGrid.FindResource("RightTextBox") as Style
                };
                
                editMenu.MainLayoutGrid.Children.Add(textBlock);
                editMenu.MainLayoutGrid.Children.Add(textBox);
            }

            if (Helper.GetActionTid(_statesJson, actionPath) == "ConfigHitboxId")
            {
                TextBlock radiusTextBlock = new TextBlock
                {
                    Text = "Radius:",
                    Style = editMenu.MainLayoutGrid.FindResource("LeftTextBlock") as Style
                };

                TextBox radiusTextBox = new TextBox
                {
                    Name = "Radius",
                    Text = Helper.GetActionValue(_statesJson, actionPath, "Radius"),
                    Style = editMenu.MainLayoutGrid.FindResource("RightTextBox") as Style
                };
                
                editMenu.MainLayoutGrid.Children.Add(radiusTextBlock);
                editMenu.MainLayoutGrid.Children.Add(radiusTextBox);
            }
                
            editMenu.ShowDialog();
            // hack
            if (Helper.IsActionRoot(actionPath))
            {
                ReloadState();
                SubActionList.Items.Clear();
                SubSubActionList.Items.Clear();   
            }
        }

        private void WriteMoveset(object sender, RoutedEventArgs e)
        {
            NmeLib.NmeLib.Serialize(_statesJson.ToString(), MovesetPath);
            MessageBox.ShowMessage("Game Saved!");
        }

        private void LaunchGame(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c start steam://run/1414850";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}