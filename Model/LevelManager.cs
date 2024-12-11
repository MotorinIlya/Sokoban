using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Sokoban.Model;

public class LevelManager
{
    private readonly List<Level> _levels = [];
    public int CurrentLevelIndex { get; private set; } = 0;

    public LevelManager(string levelsDirectory)
    {
        foreach (var file in Directory.GetFiles(levelsDirectory, "*.json"))
        {
            using var reader = new StreamReader(file);
            using var jsonReader = new JsonTextReader(reader);
            var serializer = new JsonSerializer();

            var level = serializer.Deserialize<Level>(jsonReader);
            if (level != null)
                _levels.Add(level);
        }
    }

    public Level GetCurrentLevel() => _levels[CurrentLevelIndex];

    public bool HaveNextLevel() => (CurrentLevelIndex < _levels.Count);

    public char[,] LoadLevel()
    {
        var level = GetCurrentLevel();
        var map = new char[level.Height, level.Width];
        for (int y = 0; y < level.Height; y++)
        {
            for (int x = 0; x < level.Width; x++)
            {
                map[y, x] = level.Map[y][x];
            }
        }
        CurrentLevelIndex++;
        return map;
    }

    public char[,] LoadDefaultLevel()
    {
        char[,] map =
        {
            { '#', '#', '#', '#', '#', '#', '#' },
            { '#', '.', '.', '.', 'T', '.', '#' },
            { '#', '.', '#', '#', '.', '.', '#' },
            { '#', '.', '.', 'P', 'B', '.', '#' },
            { '#', '.', '#', '#', '.', '.', '#' },
            { '#', '.', '.', '.', '.', '.', '#' },
            { '#', '#', '#', '#', '#', '#', '#' },
        };
        return map;
    }
}
