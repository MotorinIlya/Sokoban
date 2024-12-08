using Avalonia.Input;

namespace Sokoban.Model;

public static class Game
{
    private static string gameMap = "";
    public static ICreature[,] Map;
	public static int Scores;
	public static bool IsOver;

	public static Key KeyPressed;
	public static int MapWidth => Map.GetLength(0);
	public static int MapHeight => Map.GetLength(1);

    public static void CreateMap()
	{
		Map = CreatureMapCreator.CreateMap(gameMap);
	}
}