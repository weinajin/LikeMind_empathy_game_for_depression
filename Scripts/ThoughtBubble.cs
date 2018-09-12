using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ThoughtBubble : MonoBehaviour {
	public Text thoughtText;
	public string thoughtTextContent;
	public List<Text> thoughtChoiceTexts; //= new List<thoughtText>(); 
	public float WaitBeforeFadeout = 5f;

	Scene scene;
	float typewriterspeed;   //normal 0.08, depression 0.12
	float fadeDuration = 3.0f;
	bool textNeverAppear = true;
	bool textDoneAllShown = false;

	//text position
	public float width = 600f;
	public float height = 300f;

	// Use this for initialization
	void Start () {
//		thoughtText.text = thoughtTextContent;
//		thoughtText.text = thoughtText.text.Replace ("//n", "\n");   //show newline
//		thoughtText.enabled = false;
		scene = SceneManager.GetActiveScene();
		//set typewriter speed(i.e.: thought speed)
		if (scene.name == "level1") {
			typewriterspeed = 0.06f;
		} else {
			typewriterspeed = 0.12f;
		}
		thoughtText.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D  other) {
		if (textNeverAppear && other.gameObject.CompareTag ("Player")) {
			showText ();
		}
	}


	void OnTriggerExit2D(Collider2D  other) {
		if (other.gameObject.CompareTag ("Player") ) {
			StartCoroutine(TextFadeOut(WaitBeforeFadeout));
		}
	}

	IEnumerator TypewriterText(float typewriterspeed){
		textNeverAppear = false;
		foreach (char c in thoughtTextContent) {
			thoughtText.text += c;
			thoughtText.text = thoughtText.text.Replace ("|", "\n");   //show newline
			yield return new WaitForSeconds (typewriterspeed);
		}
		textDoneAllShown = true;
	}

	IEnumerator TextFadeOut(float sec)
	{
		if (!textDoneAllShown) {
			yield return new WaitForSeconds(typewriterspeed * thoughtTextContent.Length+WaitBeforeFadeout);
		}
		thoughtText.CrossFadeAlpha (0.0f, 3.0f, false);
//		yield return new Wai̧tForSeconds (5f);
//		thoughtText.enabled = false;
	}

	void showText(){
		Debug.Log (this.gameObject.name);
//		thoughtText.enabled = true;
		thoughtText.text = "";
		StartCoroutine (TypewriterText (typewriterspeed));
	}

	public void PlayerBehaviorTrigger(){
		showText ();
		StartCoroutine(TextFadeOut(WaitBeforeFadeout));
	}

}
