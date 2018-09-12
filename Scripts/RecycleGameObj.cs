using UnityEngine;
using System.Collections;

public class RecycleGameObj : MonoBehaviour {

	// Use this for initialization
	public void Restart () {
		gameObject.SetActive (true);
	}

	// Update is called once per frame
	public void Shutdown () {
		gameObject.SetActive (false);
	}
}
