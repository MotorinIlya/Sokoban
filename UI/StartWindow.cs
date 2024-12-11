using Avalonia.Controls;

namespace Sokoban.UI;


class StartWindow : Window
{
    public StartWindow()
    {
        Width = 400;
        Height = 300;
        Title = "Sokoban - Start";

        var stackPanel = new StackPanel
        {
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            Spacing = 20
        };

        var playButton = new Button
        {
            Content = "Play",
            Width = 100,
            Height = 50
        };

        playButton.Click += (_, _) =>
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        };

        stackPanel.Children.Add(playButton);
        Content = stackPanel;
    }
}