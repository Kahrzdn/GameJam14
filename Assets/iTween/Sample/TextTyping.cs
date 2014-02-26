using UnityEngine;
using System.Collections;

public class TextTyping : MonoBehaviour {
	
	public float letterPause = 0.2f;
	public AudioClip[] audioArray = new AudioClip[4];
	string message;
	private float guitextX;
	private float guitextY;
	private Vector2 screenCoord = new Vector2(((float)Screen.width / 4f), ((float)Screen.height / 2f)); 
	// Use this for initialization
	void Start () {
		//audioArray = new AudioClip[4];

//		guiText.pixelOffset = new Vector2((Screen.width / 40f), (Screen.height / 1.1f));
		guiText.text = "Privacy is rare.\nSomeone is always listening.\n\nRoute your communication\nthrough untapped nodes.";
		message = guiText.text;

		guiText.text = "";
		StartCoroutine("TypeText");
	}
	
	IEnumerator TypeText () {
		foreach (char letter in message.ToCharArray()) {
			guiText.text += letter;
			int i = Random.Range(0, 3);
			if (audioArray[i])
				audio.PlayOneShot (audioArray[i]);
			yield return 0;
			yield return new WaitForSeconds (Random.Range(0.05f, 0.2f));
		}
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			StopCoroutine("TypeText");
			guiText.text = message;
			StartCoroutine(NextLevel());
		}
	}

	IEnumerator NextLevel() {
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}