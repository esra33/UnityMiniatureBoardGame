using UnityEngine;
using System.Collections;

public class ITileIterator {

	protected ITile m_ParentTile;
	public ITile tile
	{
		get {return m_ParentTile;}
	}

	protected int m_X;
	protected int m_Y;

	delegate ITileIterator GetSubtile();
	GetSubtile []m_Functionalities;
	
	// Delta management
	public enum TileDelta
	{
		NegOne = -1,
		Zerr = 0,
		PosOne = 1
	}

	// Constructor
	public ITileIterator(ITile parentTile, int x, int y)
	{
		m_ParentTile = parentTile;
		m_X = x;
		m_Y = y;

		GetSubtile []functionalities = {GetTopLeft, GetTop, GetTopRight, GetLeft, GetCurrent, GetRight, GetBottomLeft, GetBottom, GetBottomRight};
		m_Functionalities = functionalities;
	}

	// retieve an iterator using deltas from this iterator
	public ITileIterator GetTileIterator(TileDelta x, TileDelta y)
	{
		int dx = (int)Mathf.Clamp((float)x, -1, 1);
		int dy = (int)Mathf.Clamp((float)y, -1, 1);

		return m_Functionalities[dx + 4 + (dy*3)]();
	}

	// Top left
	public virtual ITileIterator GetTopLeft()
	{
		return ITileManager.instance.GetIteratorAt(m_X - 1, m_Y - 1);
	}

	// Top Center
	public virtual ITileIterator GetTop()
	{
		return ITileManager.instance.GetIteratorAt(m_X, m_Y - 1);
	}

	// Top Right
	public virtual ITileIterator GetTopRight()
	{
		return ITileManager.instance.GetIteratorAt(m_X + 1, m_Y - 1);
	}

	// Center Left
	public virtual ITileIterator GetLeft()
	{
		return ITileManager.instance.GetIteratorAt(m_X - 1, m_Y);
	}

	// Center Center
	public virtual ITileIterator GetCurrent()
	{
		return this;
	}

	// Center Right
	public virtual ITileIterator GetRight()
	{
		return ITileManager.instance.GetIteratorAt(m_X + 1, m_Y);
	}

	// Bottom Left
	public virtual ITileIterator GetBottomLeft()
	{
		return ITileManager.instance.GetIteratorAt(m_X - 1, m_Y + 1);
	}

	// Bottom
	public virtual ITileIterator GetBottom()
	{
		return ITileManager.instance.GetIteratorAt(m_X, m_Y + 1);
	}

	// Bottom Right
	public virtual ITileIterator GetBottomRight()
	{
		return ITileManager.instance.GetIteratorAt(m_X + 1, m_Y + 1);
	}
}
