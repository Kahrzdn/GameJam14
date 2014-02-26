using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ScaleSecond();
		//StartCoroutine(FadeInNOut());
//		FadeTransparency();
//		Color textureColor = guiTexture.color;
//		textureColor.a = alphaValue;
//		guiTexture.color = textureColor;
	}
//		void ScaleFirst()
//		                {
//		iTween.PunchScale(gameObject, iTween.Hash ("x", 2.0f, "y", 2.0f, "z", 2.0f, "time", 0.7f, "delay", 1.0f, "oncomplete", "ScaleSecond"));
//		}
	public void ScaleSecond()
	{
		iTween.PunchScale(gameObject, iTween.Hash ("x", 0.5f, "y", 0.5f, "time", 1f, "delay", 0f));
	}

	IEnumerator FadeInNOut() {
		float t = 0;
		while(true) {
			Color myColor = renderer.material.color;
			myColor.a = 0.8f + Mathf.Sin(t * 4f) * 0.2f;
			renderer.material.color = myColor;
			yield return new WaitForSeconds(Time.deltaTime);
			t += Time.deltaTime;
		}
	}

	public void FadeTransparency()
	{
		iTween.FadeTo(gameObject, iTween.Hash("alpha", 0.2f, "time", 1f, "oncomplete", "FadeBackTransparency"));
	}

	public void FadeBackTransparency()
	{
		iTween.FadeTo(gameObject, iTween.Hash("alpha", 1f, "time", 1f, "oncomplete", "FadeTransparency"));
	}
}
