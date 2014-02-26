using UnityEngine;
using System.Collections;

public class ButtonInput : MonoBehaviour {
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel(0);
		}
	}

    // Testing changes
}
