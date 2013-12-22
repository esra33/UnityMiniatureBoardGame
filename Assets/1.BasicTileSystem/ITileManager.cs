using UnityEngine;
using System.Collections;

public abstract class ITileManager : MonoBehaviour {

	//--------------------------------------------------
	// Singleton access procedure
	//--------------------------------------------------
	protected static ITileManager m_ManagerInstance = null;
	public static ITileManager instance
	{
		get 
		{
			return m_ManagerInstance;
		}
	}

	//--------------------------------------------------
	// Punlic Inspector variables
	//--------------------------------------------------
	public int m_TilesWidth = 1;
	public int m_TilesHeight = 1;

	public GameObject m_TilePrefab = null;

	//--------------------------------------------------
	// Internal components used to construct the system
	//--------------------------------------------------
	protected ITile[] m_Board;

	// Associated method factory, this is created on the start
	protected abstract ITileManager GetTileManagerInstance();

	/**
	 * procedure called before initizling the system
	 */
	protected virtual void PreInitialization(){}

	/*
	 * procedure called after inializing the system
	 */
	protected virtual void PostInitialization(){}

	/*
	 * Position tile in the World space relative to the position of the board
	 */ 
	protected abstract Vector3 RequestWorldPosition(int x, int y, Vector3 tileSize);

	// Use this for initialization
	void Start () 
	{
		m_ManagerInstance = GetTileManagerInstance();

		PreInitialization();

		// Create board here
		m_Board = new ITile[m_TilesWidth * m_TilesHeight];

		// get tile measures
		Vector3 tileSize = m_TilePrefab.renderer.bounds.size;

		GameObject tileObj;
		ITile tile;
		for(int i = 0; i < m_TilesWidth; i++)
		{
			for(int j = 0; j < m_TilesHeight; j++)
			{
				tileObj = (GameObject)Instantiate(m_TilePrefab, RequestWorldPosition(i, j, tileSize), Quaternion.identity);
				tile = tileObj.GetComponent<ITile>();
				m_Board[CalculatePosition(i,j)] = tile;
				tile.Initialize();
			}
		}

		PostInitialization();
	}

	// simple calculation for position
	protected int CalculatePosition(int x, int y)
	{
		int _x = (int)Mathf.Clamp((float)x, 0, (float)m_TilesWidth - 1);
		int _y = (int)Mathf.Clamp((float)y, 0, (float)m_TilesHeight - 1);
		return _x + _y*m_TilesWidth;
	}

	// The board will allways return an iterator that points to the actual tile
	public ITileIterator GetIteratorAt(int x, int y) 
	{
		return m_Board[CalculatePosition(x,y)].GetIterator();
	}

	// Request the position of an item
	public bool RequestPosition(ITile tile, out int x, out int y) 
	{
		for(int i = 0; i < m_TilesWidth; i++)
		{
			for(int j = 0; j < m_TilesHeight; j++)
			{
				if(m_Board[CalculatePosition(i,j)] == tile)
				{
					x = i;
					y = j;
					return true;
				}
			}
		}
		x = -1;
		y = -1;
		return false;
	}
}
