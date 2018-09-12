using UnityEngine;
using System.Collections;

public class BGColorTransition : MonoBehaviour
{
	
	private float duration = 50.0f;
	public MeshRenderer render;
	private int starNum = 30;
//	GameObject[] starrysky;

	private Transform player;		// Reference to the player's transform.
	// starting value for the Lerp    
	static float t = 0.0f;

	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}

	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{
//		int screenRatio =30;
//		transform.localScale = new Vector3(Screen.width / screenRatio, Screen.height / screenRatio, 1);
		/*
        //generate random star(quad) shining on background
        starrysky = new GameObject[starNum];
        for (int i = 0; i < starNum; i++)
        {
			Vector3 randPos = new Vector3(Random.Range(0, Screen.width / screenRatio) - Screen.width / screenRatio / 2, Random.Range(0, Screen.height / screenRatio) - Screen.height / screenRatio / 2, 10);
			//Object prefab = Resources.Load("bgStar", typeof(GameObject));
			//GameObject clone = Instantiate(prefab, randPos, Quaternion.Euler(0, 0, 45)) as GameObject;
			starrysky[i] = GameObject.CreatePrimitive(PrimitiveType.Quad);
			starrysky[i].transform.parent = GameObject.FindGameObjectWithTag("starrysky").transform;
			starrysky[i].transform.position = randPos;
            starrysky[i].transform.rotation = Quaternion.Euler(0, 0, 45);
			//star material
			MeshRenderer starRender = starrysky[i].GetComponent<MeshRenderer>();
			Material starMat = Resources.Load("ConnectLineMaterial") as Material;
			starRender.material = starMat;
			starRender.receiveShadows = false;
			starRender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
        */
	}
	
	
	void Update()
	{
		//gradient background color change
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		render.material.SetColor("_Color", Color.Lerp(HexToColor("151F2B"), HexToColor("fa8856"), lerp)); //bottom
		render.material.SetColor("_Color2", Color.Lerp(HexToColor("527fc1"), HexToColor("7ae0ec"), lerp)); //top
		/*
        //starry sky shining effect
        for (int i = 0; i < starNum; i++)
        {
            float positionNum = starrysky[i].transform.position.x + starrysky[i].transform.position.y;
            float radius = (Mathf.Cos((Time.time - positionNum)) / 4);
            Vector3 newS = new Vector3(radius, radius, 0);
            starrysky[i].transform.localScale = newS;
        }
        */
	}
	void FixedUpdate ()
	{
		TrackPlayer();
	}


	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
//		float targetY = transform.position.y;

		targetX = Mathf.Lerp(transform.position.x, player.position.x, t);
//		targetY = Mathf.Lerp(transform.position.y, player.position.y, t);

		t += 0.5f * Time.deltaTime;

		// now check if the interpolator has reached 1.0
		// and swap maximum and minimum so game object moves
		// in the opposite direction.
		if (t > 1.0f){
			t = 0.0f;
		}
		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		//targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		//targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
//		transform.position = new Vector3(targetX, targetY, transform.position.z);

	}
}
