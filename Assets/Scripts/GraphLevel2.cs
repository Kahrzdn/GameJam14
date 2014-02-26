using UnityEngine;
using System.Collections;

public class GraphLevel2 : Graph {

	protected override void ConnectNodes ()
	{
		Fasten(allNodes[0]);
		Fasten(allNodes[7]);
		Connect(0, 1);
		Connect(1, 2);
		Connect(0, 2);
		Connect(1, 3);
		Connect(2, 4);
		Connect(3, 5);
		Connect(4, 5);
		Connect(1, 6);
		Connect(6, 7);
		Connect(5, 7);
		MakeNSA(3);
		MakeNSA(4);
	}

}