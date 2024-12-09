using Sokoban.Model;

namespace Sokoban;

public class Box : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        throw new System.NotImplementedException();
    }

    public int GetDrawingPriority()
    {
        throw new System.NotImplementedException();
    }

    public string GetImageFileName()
    {
        throw new System.NotImplementedException();
    }
}
