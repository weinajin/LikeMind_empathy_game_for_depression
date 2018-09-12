using UnityEngine;
using System.Collections;

public class WalkSound : MonoBehaviour {
	public AudioClip leftFeetClip;
	public AudioClip rightFeetClip;

	void PlayLeft () {
		AudioSource.PlayClipAtPoint(leftFeetClip, transform.position);
	}
	
	void PlayRight () {
		AudioSource.PlayClipAtPoint(rightFeetClip, transform.position);
	}
}
