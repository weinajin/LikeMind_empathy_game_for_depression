using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
	public RecycleGameObj prefab;

	private List<RecycleGameObj> poolInstance = new List<RecycleGameObj>();

	private RecycleGameObj CreaateInstance(Vector3 pos){
		var clone = Instantiate (prefab);
		clone.transform.position = pos;
		clone.transform.parent = transform;
		poolInstance.Add (clone);
		return clone;
	}
	
	public RecycleGameObj NextObj(Vector3 pos){
		RecycleGameObj instance = null;
		foreach (var go in poolInstance){
			if(go.gameObject.activeSelf != true){
				instance = go;
				instance.transform.position = pos;
			}
		}
		if (instance == null) instance =	CreaateInstance (pos);
		instance.Restart ();
		return instance;
	}
}
