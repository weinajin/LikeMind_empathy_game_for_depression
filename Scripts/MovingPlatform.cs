using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Transform DestinationSpot;
	public Transform OriginSpot;
	public float movingSpeed;
	public Vector2 playerPos;

	bool switchDirection = false;
	public bool active = false;
	GameObject player;

	void Awake(){
		player = GameObject.FindWithTag ("Player");
	}

	void FixedUpdate(){
//		if (player.transform.position.x > playerPos.x && player.transform.position.x <playerPos.y && !active ) {
		active = true;
//		}

		if (active) {
			// For these 2 if statements, it's checking the position of the platform.
			// If it's at the destination spot, it sets Switch to true.
			if (transform.position == DestinationSpot.position) {
//				StartCoroutine (Delay ());
				switchDirection = true;
			}
			if (transform.position == OriginSpot.position) {
//				StartCoroutine (Delay ());
				switchDirection = false;
			}
			
			// If Switch becomes true, it tells the platform to move to its Origin.
			if (switchDirection) {
				transform.position = Vector2.MoveTowards (transform.position, OriginSpot.position, movingSpeed);
			} else {
				// If Switch is false, it tells the platform to move to the destination.
				transform.position = Vector2.MoveTowards (transform.position, DestinationSpot.position, movingSpeed);
			}
		}
	}

	IEnumerator Delay(){
		yield return new WaitForSeconds (2f);
		yield return null;

	}

	void OnCollisionStay2D(Collision2D other){
		if (other.gameObject.name == "Player") {
//			if (!Input.GetButtonDown ("Jump") )
//			{
			other.transform.parent = gameObject.transform;
//			}
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.name == "Player") {
			other.transform.parent = null;
		}
	}

}
