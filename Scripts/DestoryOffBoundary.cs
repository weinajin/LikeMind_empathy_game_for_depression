using UnityEngine;
using System.Collections;

public class DestoryOffBoundary : MonoBehaviour {
	
	private Rigidbody2D rb;
	private bool outofBound;

	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		var posX = transform.position.x;
		var dirX = rb.velocity.x;
		if ((posX < 11f) && dirX < 0) {
				outofBound = true;
		}
		else
			outofBound = false;
		if (outofBound)
			OutofBounds ();
	}

	public void OutofBounds (){
		outofBound = false;
		GameObjUtil.Destory (gameObject);
	}
}
