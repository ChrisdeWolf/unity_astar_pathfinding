using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinding : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GridObj[,] grid = new GridObj[10,10];
		List<GridObj> frontier = new List<GridObj>(); //List of objs for the frontier
		List<GridObj> explored = new List<GridObj>();  //list for explored
		List<GridObj> path = new List<GridObj>();

		GridObj startPos = new GridObj( new Vector3 (0,0,0), 0, 0);
		GridObj goalPos = new GridObj( new Vector3 (0,0,80f), 0, 8);

		Debug.Log ("Starting");
		for(int i=0;i<10;i++) {
			for(int j=0;j<10;j++) {
				
				GridObj g1 = new GridObj( new Vector3 (i*10f,0,j*10f), i, j);
				// make the 2D array and any i+1 or j+1 is the neighbor
				grid [i, j] = g1;     // set spot in 2D array to the newly made gridObj

			}
		}
			
		frontier.Add(startPos);
		Debug.Log("startPos made");
		//Debug.Log (startPos.height);

		GridObj CurrentObj = new GridObj( new Vector3 (0,0,0), 0, 0);
		       
		while (frontier.Count != 0) {

			CurrentObj = frontier [0];  //set current gridObj to first in frontier

			for(int i = 1; i < frontier.Count; i++) { //Loop through the frontier starting from the second object (will skip if startPos)

				if (frontier[i].FCost < CurrentObj.FCost || frontier[i].FCost == CurrentObj.FCost && frontier[i].hCost < CurrentObj.hCost)//If the f cost of that object is less than or equal to the f cost of the current obj
				{
					CurrentObj = frontier[i]; //Set the current obj to that object
					Debug.Log ("new CurrentObj");
					path.Add (CurrentObj);
				}
			}
			Debug.Log ("Frontier size:" + frontier.Count);
			Debug.Log ("Current Obj Coords:" + CurrentObj.i + ", " + CurrentObj.j);

			if( CurrentObj.position == goalPos.position){

				//show function to create spheres
				show( CurrentObj);

				Debug.Log ("Found Target!");
				Debug.Log(CurrentObj.position);
				break;
			} 

			frontier.Remove (CurrentObj);
			Debug.Log ("Removed CurrentObj");
			explored.Add (CurrentObj);

			//check neighbors - make sure to check boundaries, 4 neighbors, each check if in frontier (if exists in frontier) or explored (skip it if here) 
			if(!(CurrentObj.i >= 10)) {
				if( !(explored.Contains( grid[ CurrentObj.i+1, CurrentObj.j]))){
					if (!(frontier.Contains (grid [CurrentObj.i + 1, CurrentObj.j]))) {
					
						frontier.Add (grid [CurrentObj.i + 1, CurrentObj.j]);
						Debug.Log ("added neighbor to frontier +i");
						grid [CurrentObj.i+1, CurrentObj.j].ParentNode = CurrentObj;
					}
				}
			}

			if (!(CurrentObj.i <= 0)) {
				if (!(explored.Contains (grid [CurrentObj.i - 1, CurrentObj.j]))){
					if (!(frontier.Contains (grid [CurrentObj.i - 1, CurrentObj.j]))) {
					
						frontier.Add (grid [CurrentObj.i - 1, CurrentObj.j]);
						Debug.Log ("added neighbor to frontier -i");
						grid [CurrentObj.i-1, CurrentObj.j].ParentNode = CurrentObj;
					}
				}
			}

			if (!(CurrentObj.j >= 10)) {
				if( !(explored.Contains( grid[ CurrentObj.i, CurrentObj.j+1]))){
					if (!(frontier.Contains (grid [CurrentObj.i, CurrentObj.j + 1]))) {
					
						frontier.Add (grid [CurrentObj.i, CurrentObj.j + 1]);
						Debug.Log ("added neighbor to frontier +j");
						grid [CurrentObj.i, CurrentObj.j + 1].ParentNode = CurrentObj;
					}
				}
			}

			if (!(CurrentObj.j <= 0)) {
				if (!(explored.Contains (grid [CurrentObj.i, CurrentObj.j - 1]))){
					if (!(frontier.Contains (grid [CurrentObj.i, CurrentObj.j - 1]))) {
					
						frontier.Add (grid [CurrentObj.i, CurrentObj.j - 1]);
						Debug.Log ("added neighbor to frontier -j");
						grid [CurrentObj.i, CurrentObj.j - 1].ParentNode = CurrentObj;
					}
				}
			}
				
			frontier.Sort();
			Debug.Log( "Frontier has been sorted");
		}

		show( goalPos);
	}
		
	/* Create the Optimal path with GameObjects on the map */
	void show( GridObj current) {

		while( current != null ){
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			Debug.Log (current.position);
			sphere.transform.position = current.position + new Vector3(0,current.height,0);
			sphere.transform.localScale += new Vector3(3F, 3F, 3F);

			current = current.ParentNode;
		}
	}
		
	/* Update is called once per frame */
	void Update () {
		
	}
}
