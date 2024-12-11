using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.VisualTree;
using Sokoban.Model;

namespace Sokoban.UI;

public class GameBoard : UserControl
{
    private const int TileSize = 50;

    private readonly Dictionary<char, IBrush> _tileBrushes = new()
    {
        { Blocks.Wall, Brushes.Gray },
        { Blocks.Floor, Brushes.White },
        { Blocks.Player, Brushes.Blue },
        { Blocks.Box, Brushes.Brown },
        { Blocks.Target, Brushes.Green },
        { Blocks.BoxOnTarget, Brushes.Red },
    };
    private MapChanger _mapChanger;

    public GameBoard()
    {
        Focusable = true;
        _mapChanger = new MapChanger();
    }

    public void HandleInput(Key key)
    {
        int dx = 0, dy = 0;

        switch (key)
        {
            case Key.Up:
                dy = -1;
                break;
            case Key.Down:
                dy = 1;
                break;
            case Key.Left:
                dx = -1;
                break;
            case Key.Right:
                dx = 1;
                break;
        }

        MovePlayer(dx, dy);
        InvalidateVisual();
    }

    private void MovePlayer(int dx, int dy)
    {
        _mapChanger.ChangeMap(dy, dx);
        if (_mapChanger.CheckWin())
        {
            ShowWinMessage();
            if (_mapChanger.HaveNextLevel())
            {
                _mapChanger.LoadLevel();
            }
        }
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        for (int y = 0; y < _mapChanger.HeightMap; y++)
        {
            for (int x = 0; x < _mapChanger.WidthMap; x++)
            {
                var rect = new Rect(x * TileSize, y * TileSize, TileSize, TileSize);
                context.FillRectangle(_tileBrushes[_mapChanger.GetPosition(y, x)], rect);
                context.DrawRectangle(new Pen(Brushes.Black, 1), rect);
            }
        }
    }

    private void ShowWinMessage()
    {
        var winDialog = new Window
        {
            Width = 300,
            Height = 200,
            Title = "Congratulations!",
            Content = new StackPanel
            {
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Children =
                {
                    new TextBlock
                    {
                        Text = "You won!",
                        FontSize = 24,
                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                    },
                    new Button
                    {
                        Content = "OK",
                        Width = 100,
                        Margin = new Thickness(0, 20, 0, 0),
                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                    }
                }
            }
        };

        var okButton = (Button)((StackPanel)winDialog.Content).Children[1];
        okButton.Click += (_, _) => winDialog.Close();

        _ = winDialog.ShowDialog(this.GetVisualRoot() as Window);
    }
}