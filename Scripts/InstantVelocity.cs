using UnityEngine;
using System.Collections;

public class InstantVelocity : MonoBehaviour {

	public Vector2 velocity;
	private Rigidbody2D rb;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	
	}
	
	void FixedUpdate () {
		rb.velocity = velocity;
	}
}
