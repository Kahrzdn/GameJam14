using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataSpawner : SpecialNode {

	public int amountOfData = 64;
	private List<DataPoint> myData = new List<DataPoint>();
	public float coolDown = 2;
	public GameObject dataPrefab;
	private float cdTimer = 1;
	public float startDelay = 5;

	public override void Start() {
		base.Start();
		GameHandler.dataSpawner = this;
		GameHandler.instance.allData = amountOfData;
		GameObject Parent = GameObject.Instantiate(new GameObject("Data")) as GameObject;
		for(int i = 0; i < amountOfData; i++) {
			DataPoint newData = (Instantiate(GameHandler.instance.dataPrefab) as GameObject).GetComponent<DataPoint>();
			newData.transform.position = myNode.transform.position + Vector3.right * 2;
			newData.transform.parent = Parent.transform;
			myData.Add(newData);
		}
		cdTimer = startDelay;
		renderer.material.color = new Color(0.14f, 0.33f, 0.53f, 1f);
	}

	void Update() {
		if(myData.Count > 0) {
			cdTimer -= Time.deltaTime;
			if(cdTimer <= 0) {
				cdTimer += coolDown;
				SpawnData();
			}

			foreach(DataPoint d in myData) {
				d.transform.position = myNode.transform.position;
			}
		}
		else {
			myNode.imSpecial = null;
			myNode.timeToLoneliness = 2.3f;
			Destroy(this);
		}
	}

	private void SpawnData() {
		DataPoint newData = myData[0];

		newData.FindNewTarget(myNode);

		myData.Remove(newData);
	}
}
