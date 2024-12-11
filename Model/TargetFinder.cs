using System.Collections.Generic;

namespace Sokoban.Model;


public class TargetFinder
{
    private List<(int, int)> _coordiinates;

    public TargetFinder()
    {
        _coordiinates = [];
    }

    public void AddTargets(char[,] map)
    {
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 'T')
                {
                    _coordiinates.Add((i , j));
                }
            }
        }
    }

    public bool IsTargetPosition(int y, int x)
    {
        foreach (var coord in _coordiinates)
        {
            var (pos_y, pos_x) = coord;
            if (pos_y == y && pos_x == x)
            {
                return true;
            }
        }
        return false;
    }
}