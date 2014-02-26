using UnityEngine;
using System.Collections;

public class NSASpy : SpecialNode {

	public override void Start ()
	{
		base.Start ();
		AudioSource newSource = gameObject.AddComponent<AudioSource>();
		newSource.loop = false;
		newSource.playOnAwake = false;
		newSource.Stop();
		newSource.clip = GameHandler.instance.nsaSound;
	}

	public override void OnReceivedData (DataPoint data)
	{
		base.OnReceivedData(data);

		audio.Stop();
		audio.Play();

		if(!data.contaminated) {
			data.contaminated = true;
			data.renderer.material.color = Color.red;
			GameHandler.instance.dataContaminated++;
		}
		myNode.nodeState = Node.NodeState.nsa;
		myNode.timeToLoneliness = 2.3f;
	}
}
