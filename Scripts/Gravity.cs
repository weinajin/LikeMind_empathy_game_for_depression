using UnityEngine;
using System.Collections.Generic;

//Provides a relatively accurate simulation of body-to-body gravity (i.e. plantary gravity).
//http://wiki.unity3d.com/index.php/Gravity

public class Gravity : MonoBehaviour 
{	
	public static float range = 5f;
	Rigidbody2D thisrb;
	void Start(){
		thisrb = GetComponent<Rigidbody2D> ();
	}
	
	void FixedUpdate () 
	{
		Collider2D[] cols  = Physics2D.OverlapCircleAll(transform.position, range); 
		List<Rigidbody2D> rbs = new List<Rigidbody2D>();
		
		foreach(Collider2D c in cols)
		{
			Rigidbody2D rb = c.attachedRigidbody;
			if(rb != null && rb != thisrb && !rbs.Contains(rb))
			{
				rbs.Add(rb);
				Vector3 offset = transform.position - c.transform.position;
				rb.AddForce( offset / offset.sqrMagnitude * thisrb.mass/10f);
				//Debug.Log(rb);
			}
		}
	}
}