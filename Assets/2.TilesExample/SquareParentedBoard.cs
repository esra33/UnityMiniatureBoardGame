using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SquareParentedBoard : ITileManager {

	protected override ITileManager GetTileManagerInstance()
	{
		return this;
	}
	
	// Parent all the tiles on this object
	protected override void PostInitialization()
	{
		foreach(ITile tile in m_Board)
		{
			tile.transform.parent = transform;
		}
	}

	protected override Vector3 RequestWorldPosition(int x, int y, Vector3 tileSize)
	{
		return new Vector3(transform.position.x + (x - m_TilesWidth*0.5f)*tileSize.x + tileSize.x*0.5f, 
		                   transform.position.y, 
		                   transform.position.z + (y - m_TilesHeight*0.5f)*tileSize.z + tileSize.z*0.5f);
	}
}
