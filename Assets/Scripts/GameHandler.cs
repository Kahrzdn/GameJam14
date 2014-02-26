using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

	public static GameHandler instance;
	public static DataSpawner dataSpawner;

	public int maxContamination = 10;
	public int maxData = 64;
	[System.NonSerialized]
	public int allData = 64;

	public GUIText guiDataCompromised;
	public GUIText guiDataCollected;

	[System.NonSerialized]
	public bool gameEnded = false;
	private bool gameWon = false;

	private int _contaminatedDataCollected = 0;
	public int contaminatedDataCollected {
		get {
			return _contaminatedDataCollected;
		}
		set {
			_contaminatedDataCollected = value;
			guiDataCollected.text = (value + dataCollected).ToString() + "/" + allData.ToString() + " packages collected";
		}
	}

	private int _dataCollected = 0;
	public int dataCollected {
		get {
			return _dataCollected;
		}
		set {
			_dataCollected = value;
			guiDataCollected.text = (value + contaminatedDataCollected).ToString() + "/" + allData.ToString() + " packages collected";
			if(value > maxData) {
				WinGame();
			}
		}
	}

	private int _dataContaminated = 0;
	public int dataContaminated {
		get {
			return _dataContaminated;
		}
		set {
			_dataContaminated = value;
			guiDataCompromised.text = (maxContamination - value).ToString() + " packages to privacy breach";
			if(value >= maxContamination) {
				LoseGame();
			}
		}
	}

	public GameObject dataPrefab;
	public AudioClip dataGatheredSound;
	public AudioClip winSound;
	public AudioClip loseSound;
	public AudioClip nsaSound;
	public AudioClip badGatheredSound;

	void Start() {
		instance = this;
		contaminatedDataCollected = 0;
		dataCollected = 0;
		dataContaminated = 0;
	}

	public void WinGame() {
		if(!gameEnded) {
			gameEnded = true;
			gameWon = true;
			audio.clip = winSound;
			audio.Play();
			StartCoroutine(NextLevel(5f));
		}
	}

	public void LoseGame() {
		if(!gameEnded) {
			gameEnded = true;
			gameWon = false;
			audio.clip = loseSound;
			audio.Play();
			StartCoroutine(NextLevel(5f, 0));
		}
	}

	IEnumerator NextLevel(float waitSome) {
		float t = 0;
		while(t < waitSome) {
			yield return new WaitForSeconds(Time.deltaTime);
			if(Input.GetMouseButtonDown(0)) {
				Application.LoadLevel(Application.loadedLevel < Application.levelCount - 1 ? Application.loadedLevel + 1 : 0);
				yield break;
			}
		}
		Application.LoadLevel(Application.loadedLevel < Application.levelCount - 1 ? Application.loadedLevel + 1 : 0);
	}

	IEnumerator NextLevel(float waitSome, int level) {
		float t = 0;
		while(t < waitSome) {
			yield return new WaitForSeconds(Time.deltaTime);
			if(Input.GetMouseButtonDown(0)) {
				Application.LoadLevel(level);
				yield break;
			}
		}
		Application.LoadLevel(level);
	}

	private Rect messageRect = new Rect(0, 0, Screen.width, Screen.height);
	public GUIStyle messageStyle;
	void OnGUI() {
		if(gameEnded) {
			GUI.Label(messageRect, gameWon ? "Message successfully transferred!" : "Your data has been compromised!", messageStyle);
		}
	}
}
