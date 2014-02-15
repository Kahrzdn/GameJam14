using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Graph : MonoBehaviour {

	public GameObject nodePrefab;
	[System.NonSerialized]
	public List<Node> allNodes = new List<Node>();
	public Vector3[] nodePositions;

	void Awake() {
		InitGraph();
	}

	public void InitGraph() {
		foreach(Vector3 v3 in nodePositions) {
			GameObject newNode = Instantiate(nodePrefab) as GameObject;
			allNodes.Add(newNode.GetComponent<Node>());
			newNode.transform.position = v3;
			newNode.transform.rotation = Quaternion.identity;
			newNode.transform.parent = transform;
		}

		ConnectNodes();
	}

	protected abstract void ConnectNodes();

	protected void Connect(Node node1, Node node2) {
		node1.neighbours.Add(node2);
		node2.neighbours.Add(node1);
		SpringJoint joint1 = node1.gameObject.AddComponent<SpringJoint>();
		SpringJoint joint2 = node2.gameObject.AddComponent<SpringJoint>();
		joint1.connectedBody = node2.rigidbody;
		joint2.connectedBody = node1.rigidbody;
		joint1.minDistance = 1;
		joint2.minDistance = 1;
		joint1.maxDistance = 2;
		joint2.maxDistance = 2;
		//Add springjoints and graphics
	}
}
