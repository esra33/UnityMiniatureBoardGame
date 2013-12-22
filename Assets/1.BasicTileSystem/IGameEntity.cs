using UnityEngine;
using System.Collections;

public delegate T EntityCommand<T> (IGameEntity entity);
public abstract class IGameEntity : MonoBehaviour {

	ITile m_ParentTile;
	public ITile tile
	{
		get{return tile;}
		set{m_ParentTile = value;}
	}

	// Checks if a new entity can be added to a list in a tile
	public abstract bool CanAddEntity(IGameEntity entity);

	// checks if this entity can be removed from a list in a tile
	public abstract bool CanRemoveEntity(bool bForce);
	public bool CanRemoveEntity()
	{
		return CanRemoveEntity(false);
	}

	// Checks if this entity can be selected
	public virtual bool CanSelect(EntityCommand<bool> provider)
	{
		return provider(this);
	}

	// What should the entity do in the main time
	public virtual void Run()
	{

	}
}
