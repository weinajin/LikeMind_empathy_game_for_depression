using UnityEngine;
using System.Collections;

public class LeverAfterJumped : MonoBehaviour {
	private Rigidbody2D rb;
	private HingeJoint2D hj;
	float fixedAngle = -20;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		hj = GetComponent<HingeJoint2D> ();
	
	}
	
	void OnCollisionStay2D (Collision2D other) {
		//Debug.Log (hj.jointAngle);
		if ((other.gameObject.CompareTag ("Player") || other.gameObject.CompareTag ("Stone")) && hj.jointAngle <= fixedAngle) {
			//hj.referenceAngle = Mathf.SmoothDamp(prevAngle, targetAngle, ref velocity, 0.3f);
			rb.isKinematic = true;
		}
	}
}
