using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject[] prefabs;
	public float delay = 0f;
	public bool active = true;
	public Vector2 delayRange = new Vector2(3,10); 

	private int stoneNum = 0;


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Player") )
		{

			StartCoroutine (EnemyGenerator ());
			ResetDelay ();
			gameObject.GetComponent<Collider2D>().enabled = false; 
		}
	}
	
	// Update is called once per frame
	IEnumerator EnemyGenerator () {
		yield return new WaitForSeconds(delay);
		if (active) {
			var newTransform = transform;
			var perfab = prefabs[Random.Range(0, prefabs.Length)];
			if (perfab.gameObject.CompareTag("ObstacleStone")){
				stoneNum ++;
			}
			GameObjUtil.Instantiate(perfab, newTransform.position);
			ResetDelay();
		}
		StartCoroutine (EnemyGenerator());
	}
	
	void ResetDelay(){
		delay = Random.Range (delayRange.x, delayRange.y);
		GameObject player = GameObject.FindWithTag ("Player");
		if (player.transform.position.x > 23 || stoneNum > 10) {
			active = false;
		}
	}
}
