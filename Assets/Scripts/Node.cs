using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	public enum NodeState {
		visited,
		notVisited,
		active,
		nsa
	}

	public SpecialNode imSpecial = null;
	public List<Node> neighbours = new List<Node>();
	private NodeState _nodeState = NodeState.notVisited;
	public NodeState nodeState {
		get {return _nodeState;}
		set {
			_nodeState = value;
			switch(value) {
			case NodeState.notVisited:
				renderer.material.color = Color.gray;
				break;
			case NodeState.visited:
				renderer.material.color = Color.gray;
				break;
			case NodeState.active:
				renderer.material.color = new Color(0.14f, 0.33f, 0.53f, 1f);
				break;
			case NodeState.nsa:
				renderer.material.color = Color.black;
				break;
			}
		}
	}

	void Start() {
		nodeState = NodeState.notVisited;
	}

	public virtual void ReceivedData(DataPoint data) {
		GetComponent<Pulse>().ScaleSecond();
		if(imSpecial) {
			imSpecial.OnReceivedData(data);
		}
		else {
			nodeState = NodeState.active;
			data.FindNewTarget(this);
			timeToLoneliness = 2.3f;
		}
	}

	[System.NonSerialized]public float timeToLoneliness = -1;
	void Update() {
		if(timeToLoneliness > 0) {
			timeToLoneliness -= Time.deltaTime;
			if(timeToLoneliness <= 0) {
				nodeState = NodeState.visited;
			}
		}
	}
}
