using UnityEngine;
using System.Collections;

public class DataPoint : MonoBehaviour {

	public float moveSpeed = 5;
	public bool contaminated;
	private Node lastNode = null;
	private Node currentTarget;
	private float wayToTarget = 0f;

	public void ArrivedAtTarget() {
		currentTarget.ReceivedData(this);
	}

	public void FindNewTarget(Node currentNode) {
		if(currentNode.neighbours.Count > 1) {
			float shortDist = -1;
			foreach(Node n in currentNode.neighbours) {
				if(n != lastNode) {
					if(shortDist == -1 || shortDist > (transform.position - n.transform.position).magnitude) {
						shortDist = (transform.position - n.transform.position).magnitude;
						currentTarget = n;
					}
				}
			}

			lastNode = currentNode;
			wayToTarget = 0f;
		}
		else {
			currentTarget = currentNode.neighbours[0];
		}
	}

	void Update() {
		if(currentTarget != null) {
			wayToTarget = Mathf.Clamp(wayToTarget + (Time.deltaTime * moveSpeed / (lastNode.transform.position - currentTarget.transform.position).magnitude), 0f, 1f);
			transform.position = lastNode.transform.position + wayToTarget * (currentTarget.transform.position - lastNode.transform.position);
//				wayToTarget * (currentTarget.transform.position + lastNode.transform.position);
			if(wayToTarget >= 1) {
				ArrivedAtTarget();
			}
		}
	}
}
