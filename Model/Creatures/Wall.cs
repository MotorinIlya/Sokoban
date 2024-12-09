namespace Sokoban.Model;


public class Wall : ICreature
{
    public CreatureCommand Act(int x, int y) => new();

    public bool DeadInConflict(ICreature conflictedObject) => false;

    public int GetDrawingPriority() => 1;

    public string GetImageFileName() => "wall.png";
}
