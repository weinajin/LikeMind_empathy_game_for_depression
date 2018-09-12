using UnityEngine;
using System.Collections;

public class FallingGround : MonoBehaviour {

	GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (player.transform.position.x - transform.position.x > 3) {
			if (!gameObject.GetComponent<Rigidbody2D> ()) {
				Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D> ();
			}
		}
	}
}
