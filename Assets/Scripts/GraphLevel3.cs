using UnityEngine;
using System.Collections;

public class GraphLevel3 : Graph {

	protected override void ConnectNodes ()
	{
		Fasten(allNodes[0]);
		Fasten(allNodes[19]);

		Connect(0, 1);
		Connect(0, 3);
		Connect(0, 8);
		Connect(1, 2);
		Connect(1, 3);
		Connect(2, 3);
		Connect(3, 4);
		Connect(3, 5);
		Connect(3, 6);
		Connect(4, 11);
		Connect(4, 12);
		Connect(5, 6);
		Connect(5, 8);
		Connect(6, 7);
		Connect(6, 11);
		Connect(7, 8);
		Connect(7, 9);
		Connect(7, 11);
		Connect(9, 10);
		Connect(10, 11);
		Connect(10, 13);
		Connect(10, 15);
		Connect(11, 12);
		Connect(11, 13);
		Connect(12, 13);
		Connect(13, 16);
		Connect(13, 17);
		Connect(14, 15);
		Connect(14, 16);
		Connect(16, 17);
		Connect(16, 18);
		Connect(17, 19);
		Connect(18, 19);

		MakeNSA(11);
		MakeNSA(18);
		MakeNSA(5);
		MakeNSA(15);
	}

}