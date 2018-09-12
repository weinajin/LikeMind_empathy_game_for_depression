using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NarritiveManager : MonoBehaviour {

	public Text displayText;
	public GameObject panel;
	public float displayTime;
	public float fadeTime;
	string displayStr;
	float typewriterspeed;   //normal 0.08, depression 0.12
	Scene scene;

	private IEnumerator fadeAlpha;

	private static NarritiveManager narritiveManager;

	public static NarritiveManager Instance () {
		if (!narritiveManager) {
			narritiveManager = FindObjectOfType(typeof (NarritiveManager)) as NarritiveManager;
			if (!narritiveManager)
				Debug.LogError ("There needs to be one active DisplayManager script on a GameObject in your scene.");
		}

		return narritiveManager;
	}


	void Start () {
		//		thoughtText.text = thoughtTextContent;
		//		thoughtText.text = thoughtText.text.Replace ("//n", "\n");   //show newline
		//		thoughtText.enabled = false;
		scene = SceneManager.GetActiveScene ();
		//set typewriter speed(i.e.: thought speed)
		if (scene.name == "level1") {
			typewriterspeed = 0.06f;
		} else {
			typewriterspeed = 0.12f;
		}
		displayText.text = "";
	}


	public void DisplayMessage (string message) {
		displayStr = message;
		displayText.text = "";
		Color p = panel.GetComponent<Image> ().color;
		p.a = 0;
		panel.GetComponent<Image> ().color = p;
		SetAlpha ();
	}

	public void SetDisplayTime(float t){
		displayTime = t;
	}

	public void SetFadeTime(float t){
		fadeTime = t;
	}


	void SetAlpha () {
		if (fadeAlpha != null) {
			StopCoroutine (fadeAlpha);
		}
		fadeAlpha = FadeAlpha ();
		StartCoroutine (fadeAlpha);
	}

	IEnumerator FadeAlpha () {
		yield return new WaitForSeconds (1f);
		Color resetpanelColor = panel.GetComponent<Image> ().color;
		Color resetColor = displayText.color;
		resetColor.a = 1;
		displayText.color = resetColor;
		resetpanelColor.a =1;
		panel.GetComponent<Image> ().color = resetpanelColor;

		foreach (char c in displayStr) {
			displayText.text += c;
			displayText.text = displayText.text.Replace ("|", "\n");   //show newline
			yield return new WaitForSeconds (typewriterspeed);
		}
		yield return new WaitForSeconds (displayTime);

		while (displayText.color.a > 0) {
			Color displayColor = displayText.color;
			displayColor.a -= Time.deltaTime / fadeTime;
			displayText.color = displayColor;
			Color displayPanelColor = panel.GetComponent<Image> ().color;
			displayPanelColor.a -= Time.deltaTime / fadeTime;
			panel.GetComponent<Image> ().color = displayPanelColor;
			yield return null;
		}
		yield return null;
	}
}
