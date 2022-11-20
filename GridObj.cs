using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObj : System.IComparable {
	public float height;
	public Vector3 position;
	public HashSet<GridObj> connected = new HashSet<GridObj> ();
	public int i;
	public int j;
								
	// For the AStar algorithm, stores what gridobj it previously
	// came from so it can trace the shortest path
	public GridObj ParentNode; 

	public int gCost = 1 ; //The cost of moving to the next square
	public int hCost; //The distance to the goal from this gridObj
	public int FCost; // to add G cost and H Cost, and since we'll never need to edit FCost

	public GridObj( Vector3 a_position, int ai, int aj){    
		position = a_position;
		i = ai;
		j = aj;
		gCost = 1;
		hCost = distance (i, j); 

		height = Terrain.activeTerrain.SampleHeight (position);

		FCost = gCost + (int)height + hCost;
	}

	public int CompareTo( object obj){
		GridObj temp = (GridObj)obj;
		if(height > temp.height) 
			return 1;
		else if( height == temp.height)
			return 0;
		else
			return -1;
	}

	public int distance( int curri, int currj){
		int ii = Mathf.Abs(curri - 0); //i1-i2
		int ij = Mathf.Abs(currj - 8); //j1-j2

		return ii + ij;
	}
}
