using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovementNoStone : MonoBehaviour {
	float speed ;
	float jumpSpeed ;
	public static float moveHori;
	private float oriScale;
	private Rigidbody2D rb;

	//jump and ground check
	public static bool isGrounded = true;
	public Transform groundCheck;
	public LayerMask groundLayers;
	float groundRadius = 0.2f;
	//bool doubleJump = false;

	//audio
	public AudioClip pickupClip;
	public AudioClip walkClip;
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.

	//thought
	bool jumpTextShown = false;
	bool beginningTextShown = false;
	NarritiveManager narritiveManager;
	NarritiveOptionManager narritiveOptionManager;
	string[] msgs = new string[] {"If I could have fortune, |I wish to revisit the days, |those before everything happened...",
		"On those ordinary days, |even jumping and walking are lovely!",
		"The stars present my favourite things. |Life can't live without them.",
		"Engaging in things I'm interested in |makes me full of vitality!",
		"A regular meal still taste delicious... ",
		"The past is now faraway dreams... |Will I regain them, those precious ordinary life of mine?"
	};


	//pickups
//	public GameObject interest100;
	public GameObject interest100;
//	public GameObject mood100;
//	public GameObject mood50;
	bool leverHited = false;



	void Awake()
	{
		rb = GetComponent<Rigidbody2D> ();
		narritiveManager = NarritiveManager.Instance ();
		narritiveOptionManager = NarritiveOptionManager.Instance ();
	}

	void Start () {
		speed = 75f;
		jumpSpeed = 500f;  //init 300   level 1 < 1500
		oriScale = transform.localScale.x;
		groundCheck = transform.FindChild ("GroundCheck");
//		JumpThought = GameObject.Find ("ThoughtBubble_Jump");
//		BeginningThought = GameObject.Find ("ThoughtBubble_Beginning");
		if ( !beginningTextShown) {
			Narritive(msgs[0],10,4);
//			BeginningThought.GetComponent<ThoughtBubble> ().PlayerBehaviorTrigger ();
			beginningTextShown = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate()
	{
		moveHori = Input.GetAxis ("Horizontal");
		Vector2 move = new Vector2 (moveHori, 0.0f);
		rb.velocity = move * speed;

		if (Mathf.Abs(moveHori) > 0 )
			AudioSource.PlayClipAtPoint(walkClip, transform.position);
		//flip face
		if (rb.velocity.x < 0) 
		{
			transform.localScale = new Vector2 (-oriScale, oriScale);
		}
		else if (rb.velocity.x > 0) 
		{
			transform.localScale = new Vector2 (oriScale, oriScale);
		}

		//jump
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, groundLayers);
		//if ( (isGrounded || ! doubleJump) && Input.GetButtonDown ("Jump") ) {
		if ( isGrounded  && Input.GetButtonDown ("Jump") ) {
			//jump sound
			int i = Random.Range(0, jumpClips.Length);
			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
			//jump force
			rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			isGrounded = false;
			if (!jumpTextShown && transform.position.x >5f) {
//				JumpThought.GetComponent<ThoughtBubble> ().PlayerBehaviorTrigger ();
				Narritive(msgs[1],5,3);
				jumpTextShown = true;
			}

			//double jump
			//if (!doubleJump && !isGrounded)
			//{	doubleJump = true; Debug.Log( doubleJump);}
		}
		//double jump
		//if (isGrounded)
		//	doubleJump = false;

	}

	void OnCollisionEnter2D(Collision2D other) {

//		star and heart, star-interest-jump, heart-mood-walk
		if (other.gameObject.CompareTag ("Star") || other.gameObject.CompareTag ("StarSmall")) {
			Destroy(other.gameObject);
//			if (other.gameObject.CompareTag ("Star") ) {
//				jumpSpeed += 10f;
//				Vector2 newPosS = RelocatePickupScore(other.gameObject, 4.4f, -3.65f);
////				Instantiate (interest100, newPosS, transform.rotation);
//			}
			if (other.gameObject.CompareTag ("StarSmall")) {
				jumpSpeed += 5f;
				Vector2 newPosS = RelocatePickupScore (other.gameObject, 2.2f, -1.5f);
				Instantiate (interest100, newPosS, transform.rotation);
				AudioSource.PlayClipAtPoint(pickupClip, transform.position);
			}
			if (other.gameObject.name == "StarNarritive1") {
				Narritive (msgs [2]);
			}
			if (other.gameObject.name == "StarNarritive2") {
				Narritive (msgs [3]);
			}
			if (other.gameObject.name == "StarNarritive3") {
				Narritive (msgs [4]);
			}
			if (other.gameObject.name == "StarNarritive5") {
				Narritive (msgs [5]);
				float newY = gameObject.transform.position.y + 2f;
				Vector3 newPos = new Vector3 (gameObject.transform.position.x, newY, gameObject.transform.position.z);
				StartCoroutine (OptionDisplay(newPos, 5f));
			}
		}
//		if (other.gameObject.CompareTag ("Heart") || other.gameObject.CompareTag ("HeartSmall")) {
//			Destroy(other.gameObject);
//			if (other.gameObject.CompareTag ("Heart") ) {
//				heartScore += 100  ;
//				speed += 1f;
//				Vector2 newPosH = RelocatePickupScore(other.gameObject, 5.4f, -3.5f);
//				Instantiate (mood100, newPosH, transform.rotation);
//			}
//			if (other.gameObject.CompareTag ("HeartSmall") ) {
//				heartScore += 50  ;
//				speed += 0.5f;
//				Vector2 newPosHS = RelocatePickupScore(other.gameObject, 2.6f, -1.3f);
//				Instantiate (mood50, newPosHS, transform.rotation);
//			}
//			AudioSource.PlayClipAtPoint(pickupClip, transform.position);
//		}

		//icon 
		if (other.gameObject.CompareTag ("Lever"))
			leverHited = true;
//		if (other.gameObject.CompareTag ("Doctor")) {
//			DoctorHelp();
//			//increase mood & interest
//			Vector2 newPosH = RelocatePickupScore(other.gameObject, 5.4f, -2.3f);
//			Instantiate (mood100, newPosH, transform.rotation);
//
//			Vector2 newPosS = RelocatePickupScore(other.gameObject, 4.4f, -3.65f);
//			Instantiate (interest100, newPosS, transform.rotation);
//			Destroy (other.gameObject);
//
//		}
	}
		

	Vector2 RelocatePickupScore(GameObject obj, float x, float y){
		Vector2 result = obj.transform.position;
		result.x += x;
		result.y += y;
		return result;
	}

	void Narritive(string msg, float t1=5, float t2=3){
		narritiveManager.DisplayMessage (msg);
		narritiveManager.SetDisplayTime (t1);
		narritiveManager.SetFadeTime (t2);
	}

	void OnTriggerEnter2D (Collider2D other) {
		StartCoroutine (OptionDisappear(other));
	}

	IEnumerator OptionDisappear(Collider2D other){
		int i = 3;
		while (i > 0) {
			other.gameObject.SetActive (false);
			yield return new WaitForSeconds (0.4f);
			other.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.4f);
			i--;
		}
		yield return new WaitForSeconds (1.5f);
		other.gameObject.transform.parent.gameObject.SetActive(false);
		if (other.gameObject.name == "narritiveOptions1_0") {
			SceneManager.LoadScene("level1");
		}
		else if (other.gameObject.name == "narritiveOptions1_1") {
			SceneManager.LoadScene("level2");
		}
		
	}

	IEnumerator OptionDisplay(Vector3 pos, float delayTime){
		yield return new WaitForSeconds (delayTime);
		narritiveOptionManager.DisplayNarritiveOptions (pos);
	}

}
