using UnityEngine;
using System.Collections;

public class DestinationNode : SpecialNode {

	public override void Start ()
	{
		AudioSource newSource = gameObject.AddComponent<AudioSource>();
		newSource.loop = false;
		newSource.playOnAwake = false;
		newSource.Stop();
		newSource.clip = GameHandler.instance.dataGatheredSound;
		renderer.material.color = new Color(0.3f, 0.58f, 0.12f, 1f);
		base.Start ();
	}

	public override void OnReceivedData (DataPoint data)
	{
		audio.Stop();
		if(data.contaminated) {
			audio.clip = GameHandler.instance.badGatheredSound;
			audio.Play();
			GameHandler.instance.contaminatedDataCollected++;
		}
		else {
			audio.clip = GameHandler.instance.dataGatheredSound;
			audio.Play();
			GameHandler.instance.dataCollected++;
		}
		Destroy(data.gameObject);
	}
}
