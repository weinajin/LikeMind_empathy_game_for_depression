using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjUtil  {
	public static Dictionary<RecycleGameObj, ObjectPool> pools = new Dictionary<RecycleGameObj, ObjectPool>();
	
	public static GameObject Instantiate(GameObject prefab, Vector3 pos){
		GameObject instance = null;
		
		var recycleScript = prefab.GetComponent<RecycleGameObj>();
		if (recycleScript != null) {
			var pool = GetObjectPool (recycleScript);
			instance = pool.NextObj (pos).gameObject;
		} else {
			instance = GameObject.Instantiate (prefab);
			instance.transform.position = pos;
		}
		return instance;
	}
	
	
	public static void Destory(GameObject gameObject){
		var recycleGameObject = gameObject.GetComponent<RecycleGameObj> ();
		if (recycleGameObject != null) {
			recycleGameObject.Shutdown ();
		}
		else GameObject.Destroy (gameObject);
	}
	
	private static ObjectPool GetObjectPool(RecycleGameObj reference){
		ObjectPool pool = null;
		if (pools.ContainsKey (reference)) {
			pool = pools [reference];
		} else {
			var poolContainer = new GameObject(reference.gameObject.name + "ObjectPool");
			pool = poolContainer.AddComponent<ObjectPool>();
			pool.prefab = reference;
			pools.Add(reference,pool);
		}
		return pool;
	}
}
