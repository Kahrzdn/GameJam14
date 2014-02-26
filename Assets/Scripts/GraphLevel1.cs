using UnityEngine;
using System.Collections;

public class GraphLevel1 : Graph {

	protected override void ConnectNodes ()
	{
		Fasten(allNodes[0]);
		Fasten(allNodes[6]);
		Connect(0, 1);
		Connect(1, 2);
		Connect(0, 2);
		Connect(1, 3);
		Connect(2, 4);
		Connect(3, 5);
		Connect(4, 5);
		Connect(5, 6);
	}

}