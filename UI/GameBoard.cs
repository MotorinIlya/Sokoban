using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
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
}