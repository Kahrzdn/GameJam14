using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Graph : MonoBehaviour {

	public GameObject nodePrefab;
	public GameObject connectionPrefab;
	[System.NonSerialized]
	public static List<Node> allNodes = new List<Node>();
	public float startDelay = 2;
	public int dataPoints = 40;
	public Vector3[] nodePositions;

	void Awake() {
		allNodes.Clear();
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

		DataSpawner newSpawn = allNodes[0].gameObject.AddComponent<DataSpawner>();
		newSpawn.startDelay = startDelay;
		newSpawn.amountOfData = dataPoints;

		allNodes[allNodes.Count - 1].gameObject.AddComponent<DestinationNode>();
		allNodes[0].transform.localScale *= 1.5f;
		allNodes[allNodes.Count - 1].transform.localScale *= 1.5f;

		ConnectNodes();
	}

	protected abstract void ConnectNodes();

	public float nodesSpringiness = 25;
	public float nodesDamper = 5;
	public float nodesMinDist = 3;
	public float nodesMaxDist = 5;
	protected void Connect(Node node1, Node node2) {
		node1.neighbours.Add(node2);
		node2.neighbours.Add(node1);

		SpringJoint joint1 = node1.gameObject.AddComponent<SpringJoint>();
		joint1.connectedBody = node2.rigidbody;
		joint1.spring = nodesSpringiness;
		joint1.damper = nodesDamper;
		joint1.minDistance = nodesMinDist;
		joint1.maxDistance = nodesMaxDist;


		LineScript line1 = (Instantiate(connectionPrefab) as GameObject).GetComponent<LineScript>();
		line1.startNode = node1.gameObject;
		line1.endNode = node2.gameObject;
	}

	protected void Connect(int node1, int node2) {
		Connect(allNodes[node1], allNodes[node2]);
	}

	protected void Fasten(Node node) {
		node.rigidbody.isKinematic = true;
	}

	protected void MakeNSA(int node) {
		MakeNSA(allNodes[node]);
	}

	protected void MakeNSA(Node node) {
		node.gameObject.AddComponent<NSASpy>();
	}
}
