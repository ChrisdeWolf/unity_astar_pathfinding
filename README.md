# unity_astar_pathfinding
Implementation of the A* search algorithm in Unity 3D to generate an optimal path, very useful in AI pathfinding for video games. This unity project will find the optimal path, given a randomly "elevated" map and a target point. The calculated optimal path can be seen in the below images, represented by white spheres.

O(b^d) run-time and space complexity -- often best choice pathfinding algorithm for video game pathfinding which often require dynamic graph traversing.

This implementation uses a custom grid object defined by the GridObj class found in GridObj.cs, and the A* algorithm is then implemented using the GridObj class in pathfinding.cs.

## Randomly Elevated Map
![Alt text](images/random_map.png?raw=true "Randomly Elevated Map")

## Generated Optimal Path
![Alt text](images/optimal_path1.png?raw=true "Optimal Path")
![Alt text](images/optimal_path2.png?raw=true "Optimal Path (Different Angle)")


