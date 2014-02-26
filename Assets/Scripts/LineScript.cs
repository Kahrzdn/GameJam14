using UnityEngine;
using System.Collections;

public class LineScript : MonoBehaviour {
	public GameObject startNode;
	public GameObject endNode;
	public LineRenderer lineRenderer;

	void Start() {
		lineRenderer.SetWidth(0.1f, 0.1f);
	}

	void Update () {
		lineRenderer.SetPosition(0, startNode.transform.position + Vector3.forward * 1.5f);
		lineRenderer.SetPosition(1, endNode.transform.position + Vector3.forward * 1.5f);

	}
}