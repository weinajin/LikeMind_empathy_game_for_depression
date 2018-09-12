using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleLevelTransition : MonoBehaviour {
	GameObject panel;

	// Use this for initialization
	void Start () {
		panel = GameObject.Find ("titlePanel");
		StartCoroutine (DisplayTitle ());
	}

	IEnumerator DisplayTitle(){
		yield return new WaitForSeconds (5f);
		Color resetColor = panel.GetComponent<Image> ().color;
		resetColor.a = 1;
		panel.GetComponent<Image> ().color = resetColor;
		while (panel.GetComponent<Image> ().color.a > 0) {
			Color displayColor = panel.GetComponent<Image> ().color;
			displayColor.a -= Time.deltaTime / 3f;
			panel.GetComponent<Image> ().color = displayColor;
			yield return null;
		}
		SceneManager.LoadScene("level1");	

	}
}
