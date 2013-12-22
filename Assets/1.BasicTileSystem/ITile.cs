using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ITile : MonoBehaviour {

	protected int m_X; // Aligned with X plane
	protected int m_Y; // Aligned with Z plane

	// List of internal components
	protected List<IGameEntity> m_GameEntities = new List<IGameEntity>();

	// This should be replaced with the right implementation of the iterator system
	public virtual ITileIterator GetIterator()
	{
		return new ITileIterator(this, m_X, m_Y);
	}

	// Initialization process
	public virtual void Initialize()
	{
		// Request the position information of this item
		ITileManager.instance.RequestPosition(this, out m_X, out m_Y);
	}

	// Add a new entity
	public bool AddNewEntity(IGameEntity gameEntity)
	{
		foreach(IGameEntity ge in m_GameEntities) 
		{
			if(!ge.CanAddEntity(gameEntity))
				return false;
		}

		m_GameEntities.Add(gameEntity);
		gameEntity.tile = this;
		return true;
	}

	// remove entitiy
	public bool RemoveEntity(IGameEntity gameEntity)
	{
		if(m_GameEntities.Contains(gameEntity) && gameEntity.CanRemoveEntity())
		{
			m_GameEntities.Remove(gameEntity);
			return true;
		}
		return false;
	}

	// Run a specific command on all the entities
	public void RunCommand(EntityCommand<Object> command)
	{
		foreach(IGameEntity ge in m_GameEntities) 
		{
			command(ge);
		}
	}
}
