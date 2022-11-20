using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinding : MonoBehaviour {
	/* for initialization */
	void Start () {
		GridObj[,] grid = new GridObj[10,10];
		List<GridObj> frontier = new List<GridObj>();
		List<GridObj> explored = new List<GridObj>();
		List<GridObj> path = new List<GridObj>();

		GridObj startPos = new GridObj( new Vector3 (0,0,0), 0, 0);
		GridObj goalPos = new GridObj( new Vector3 (0,0,80f), 0, 8);

		for(int i=0;i<10;i++) {
			for(int j=0;j<10;j++) {
				GridObj g1 = new GridObj( new Vector3 (i*10f,0,j*10f), i, j);
				grid [i, j] = g1;
			}
		}
			
		frontier.Add(startPos);
		GridObj CurrentObj = new GridObj( new Vector3 (0,0,0), 0, 0);
		
		while (frontier.Count != 0) {
			CurrentObj = frontier [0];
			for(int i = 1; i < frontier.Count; i++) {
				if (frontier[i].FCost < CurrentObj.FCost || frontier[i].FCost == CurrentObj.FCost && frontier[i].hCost < CurrentObj.hCost) {
					CurrentObj = frontier[i];
					path.Add (CurrentObj);
				}
			}

			if( CurrentObj.position == goalPos.position){
				show( CurrentObj); // create spheres
				break;
			}

			frontier.Remove (CurrentObj);
			explored.Add (CurrentObj);

			// check boundaries and the 4 neighbors. For each check if it exists in frontier or explored (skip it)
			if(!(CurrentObj.i >= 10)) {
				if( !(explored.Contains( grid[ CurrentObj.i+1, CurrentObj.j]))){
					if (!(frontier.Contains (grid [CurrentObj.i + 1, CurrentObj.j]))) {
						frontier.Add (grid [CurrentObj.i + 1, CurrentObj.j]);
						grid [CurrentObj.i+1, CurrentObj.j].ParentNode = CurrentObj;
					}
				}
			}

			if (!(CurrentObj.i <= 0)) {
				if (!(explored.Contains (grid [CurrentObj.i - 1, CurrentObj.j]))){
					if (!(frontier.Contains (grid [CurrentObj.i - 1, CurrentObj.j]))) {
						frontier.Add (grid [CurrentObj.i - 1, CurrentObj.j]);
						grid [CurrentObj.i-1, CurrentObj.j].ParentNode = CurrentObj;
					}
				}
			}

			if (!(CurrentObj.j >= 10)) {
				if( !(explored.Contains( grid[ CurrentObj.i, CurrentObj.j+1]))){
					if (!(frontier.Contains (grid [CurrentObj.i, CurrentObj.j + 1]))) {
						frontier.Add (grid [CurrentObj.i, CurrentObj.j + 1]);
						grid [CurrentObj.i, CurrentObj.j + 1].ParentNode = CurrentObj;
					}
				}
			}

			if (!(CurrentObj.j <= 0)) {
				if (!(explored.Contains (grid [CurrentObj.i, CurrentObj.j - 1]))){
					if (!(frontier.Contains (grid [CurrentObj.i, CurrentObj.j - 1]))) {
						frontier.Add (grid [CurrentObj.i, CurrentObj.j - 1]);
						grid [CurrentObj.i, CurrentObj.j - 1].ParentNode = CurrentObj;
					}
				}
			}
				
			frontier.Sort();
		}

		show( goalPos);
	}
		
	/* Create the Optimal path with GameObjects on the map */
	void show( GridObj current) {
		while( current != null ){
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = current.position + new Vector3(0,current.height,0);
			sphere.transform.localScale += new Vector3(3F, 3F, 3F);
			current = current.ParentNode;
		}
	}
		
	/* Update is called once per frame */
	void Update () {}
}
