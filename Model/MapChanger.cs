namespace Sokoban.Model;

public class MapChanger
{
    public MapChanger()
    {
        _levelManager = new LevelManager(_levelDirectory);
        if (_levelManager.HaveNextLevel())
        {
            _map = _levelManager.LoadLevel();
        }
        else
        {
            _map = _levelManager.LoadDefaultLevel();
        }
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

    public void LoadLevel()
    {
        _map = _levelManager.LoadLevel();
        _targetFinder.Clear();
        _targetFinder.AddTargets(_map);
        ChangePlayerPosition();
    }

    public bool HaveNextLevel() => _levelManager.HaveNextLevel();
    public bool CheckWin() => _targetFinder.CheckWin(_map);
    public char GetPosition(int y, int x) => _map[y, x];
    public int WidthMap => _map.GetLength(1);
    public int HeightMap => _map.GetLength(0);

    private readonly string _levelDirectory = "./Levels";
    private TargetFinder _targetFinder;
    private LevelManager _levelManager;
    private char[,] _map;

    private int _playerX = 3;
    private int _playerY = 3;
    private void ChangePlayerPosition()
    {
        for (var i = 0; i < _map.GetLength(0); i++)
        {
            for (var j = 0; j < _map.GetLength(1); j++)
            {
                if (_map[i, j] == Blocks.Player)
                {
                    _playerY = i;
                    _playerX = j;
                    break;
                }
            }
        }
    }
}