using UnityEngine;
using System.Collections;

public abstract class SpecialNode : MonoBehaviour {

	public Node myNode;

	public virtual void Start() {
		myNode = GetComponent<Node>();
		myNode.imSpecial = this;

	}

	public virtual void OnReceivedData(DataPoint data) {
		data.FindNewTarget(myNode);
	}
}
