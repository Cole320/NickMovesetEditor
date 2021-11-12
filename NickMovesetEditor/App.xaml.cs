using System.Windows;
using NickMovesetEditor.Windows;

namespace NickMovesetEditor
{
    internal sealed partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var gameDirWindow = new GameDirSelector();
            gameDirWindow.Show();
        }
    }
}