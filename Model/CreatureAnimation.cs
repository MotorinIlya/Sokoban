using Avalonia;

namespace Sokoban.Model;

public class CreatureAnimation
{
	public CreatureCommand Command;
	public ICreature Creature;
	public Point Location;
	public Point TargetLogicalLocation;
}