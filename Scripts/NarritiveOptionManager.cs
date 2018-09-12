using UnityEngine;
using System.Collections;

public class NarritiveOptionManager : MonoBehaviour {
	
	private static NarritiveOptionManager narritiveOptionManager;


	
	public static NarritiveOptionManager Instance () {
		if (!narritiveOptionManager) {
			narritiveOptionManager = FindObjectOfType(typeof (NarritiveOptionManager)) as NarritiveOptionManager;
			if (!narritiveOptionManager)
				Debug.LogError ("There needs to be one active narritiveOptionManager script on a GameObject in your scene.");
		}

		return narritiveOptionManager;
	}

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	
	}


	public void DisplayNarritiveOptions(Vector3 playerPos){
		gameObject.transform.position = playerPos;
		gameObject.SetActive (true);
	}



}
