namespace Sokoban.Model;

public class MapChanger
{
    private TargetFinder _targetFinder;
    private char[,] _map =
    {
        { '#', '#', '#', '#', '#', '#', '#' },
        { '#', '.', '.', '.', 'T', '.', '#' },
        { '#', '.', '#', '.', 'B', '.', '#' },
        { '#', '.', '.', 'P', 'B', '.', '#' },
        { '#', '.', '#', '#', '.', '.', '#' },
        { '#', '.', '.', '.', '.', '.', '#' },
        { '#', '#', '#', '#', '#', '#', '#' },
    };

    private int _playerX = 3;
    private int _playerY = 3;

    public MapChanger()
    {
        _targetFinder = new TargetFinder();
        _targetFinder.AddTargets(_map);
    }

    public void ChangeMap(int dy, int dx)
    {
        int newX = _playerX + dx;
        int newY = _playerY + dy;

        if (_map[newY, newX] == Blocks.Wall)
        {
            return;
        }

        if (_map[newY, newX] == Blocks.Box || _map[newY, newX] == Blocks.BoxOnTarget)
        {
            int boxNewX = newX + dx;
            int boxNewY = newY + dy;

            if (_map[boxNewY, boxNewX] == Blocks.Floor)
            {
                _map[boxNewY, boxNewX] = Blocks.Box;
            }
            else if (_map[boxNewY, boxNewX] == Blocks.Target)
            {
                _map[boxNewY, boxNewX] = Blocks.BoxOnTarget;
            }
            else return;
        }

        _map[newY, newX] = Blocks.Player;
        if (_targetFinder.IsTargetPosition(_playerY, _playerX))
        {
            _map[_playerY, _playerX] = Blocks.Target;
        }
        else 
        {
            _map[_playerY, _playerX] = Blocks.Floor;
        }
        _playerY = newY;
        _playerX = newX;
    }

    public char GetPosition(int y, int x) => _map[y, x];
    public int WidthMap => _map.GetLength(1);
    public int HeightMap => _map.GetLength(0);
}