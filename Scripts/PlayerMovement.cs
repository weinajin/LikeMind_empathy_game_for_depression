using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
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
	public AudioClip stoneIncrease;
	public AudioClip stoneDecrease;

	//GUI
	int starScore = 0;
	int heartScore = 0;
	Text moodText;
	Text interestText;
	Text depressionBurden;
	Slider heartSlider;
	Slider starSlider; 
	Slider stoneSlider;

	//pickups
	public GameObject interest100;
//	public GameObject interest50;
//	public GameObject mood100;
//	public GameObject mood50;
//	public GameObject doctor;
	bool leverHited = false;
//	bool doctorNoShow = true;

	//stone
	GameObject stone;
	float stoneMass;

	//thought
	bool cannotwalkTextShown = false;
	bool jumpTextShown = false;
	bool Option2Shown = false;
	bool NarritiveOption2Shown = false;
	NarritiveManager narritiveManager;
	NarritiveOptionManager narritiveOptionManager;
	string[] msgs = new string[] {"I know I need to walk, |move one leg then another, |but it seems too much work for me.",
		"Food is insipid. |Eating feels to me like the Stations of the Cross.",
		"Why can't I do anything to the star?|Those interesting things no longer interest me anymore. ", //I don't feel like doing any of the things I had previously wanted to do. |I don’t know why...",
		"People jump. |It’s not a big deal. |But yet I'm nonetheless in its grip and unable to figure out any way around it.",
		"It’s the stone attached changed me. |But I don’t know what it is and where it comes from. |Not to mention how to get rid of it...",
		"“What’s going on with you lately?”|I always encounter friends’ ask. |How should I respond?",//|(Reach the bubble in front of option to make a choice)",
		"The bigger the stone, |the more vacant and slower I feel.",
		"The stone seems to be intangible to others. |Only I can sense it.",
		"Should I tell others about my stone problem?",
		"I hear my mind repeating, “You are nothing. You are nobody. You don't even deserve to live.”",
		"My family is getting to realize my problem now. “It’s your weakness problem. You really need to snap out of it.”",
		"But how could I will myself to get over a broken arm? |Or a nagging stone? |The problem isn’t arise because of my personality trait or subjective thought. It’s an objective existence!",
		"Some co-worker heard of my problem. They think I’m dangerous and try to avoid me privately.",
		"I lose quite a few friends because they were scared of me or didn’t know how to treat me."
	};

	void Awake()
	{
		rb = GetComponent<Rigidbody2D> ();
		stone = GameObject.Find("Stone");
		stoneMass = stone.GetComponent<Rigidbody2D> ().mass;
		narritiveManager = NarritiveManager.Instance ();
		narritiveOptionManager = NarritiveOptionManager.Instance ();
		Narritive ("",0,0); //disable the text frame in the beginning
	}
	// Use this for initialization
	void Start () {
		speed = 40f;
		jumpSpeed =500f;  //init 300   level 1 < 1500
		oriScale = transform.localScale.x;
		groundCheck = transform.FindChild ("GroundCheck");
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
//			if (doctorNoShow && leverHited ) {
//			Doctor ();
//			}
			transform.localScale = new Vector2 (-oriScale, oriScale);
		}
		else if (rb.velocity.x > 0) 
		{
			transform.localScale = new Vector2 (oriScale, oriScale);
		}
		if (!cannotwalkTextShown && transform.position.x > -2.5f) {
			Narritive (msgs [0], 5, 3);
			cannotwalkTextShown = true;
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
			if (!jumpTextShown && transform.position.x >11f) {
				Narritive(msgs[3],5,3);
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
		if (other.gameObject.name == "StarNarritive1") {
			Narritive (msgs [1]);
		} else if (other.gameObject.name == "StarNarritive2") {
			Narritive (msgs [2]);
		}
		else if (other.gameObject.name == "StarNarritive4") {
			Narritive (msgs [4]);
		}
		else if (other.gameObject.name == "StarNarritive5") {
			Narritive (msgs [5], 8,1);
			float newY = gameObject.transform.position.y + 2f;
			Vector3 newPos = new Vector3 (gameObject.transform.position.x, newY, gameObject.transform.position.z);
			if (!Option2Shown) {
				StartCoroutine (OptionDisplay (newPos, 8f));
				Option2Shown = true;
			}
		}
		else if (other.gameObject.name == "StarNarritive6") {
			Narritive (msgs [6]);
		}
		else if (other.gameObject.name == "StarNarritive7") {
			Narritive (msgs [7]);
		}
		if (other.gameObject.tag == "NarritiveOption2" && !NarritiveOption2Shown ) {
			StartCoroutine (OptionDisappear (other));
			NarritiveOption2Shown = true;
		}
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
		if (other.gameObject.name == "narritiveOptions2_0") {
			StoneChange (-1f);
		}
		else if (other.gameObject.name == "narritiveOptions2_1") {
			StoneChange (1f);
		}

	}

	IEnumerator OptionDisplay(Vector3 pos, float delayTime){
		yield return new WaitForSeconds (delayTime);
		narritiveOptionManager.DisplayNarritiveOptions (pos);
	}

	void StoneChange(float delta){
		stoneMass += delta*10;
		stone.transform.localScale += new Vector3(0.1f* delta, 0.1f*delta, 1f);
		if (delta > 0) {
			AudioSource.PlayClipAtPoint (stoneIncrease, transform.position);
			Vector3 newPosStone = stone.transform.position;
			Instantiate (interest100, newPosStone, Quaternion.identity);

		} else if (delta < 0) {
			AudioSource.PlayClipAtPoint (stoneDecrease, transform.position);
		}

			
	}

//	void SetScoreGUI(){
//		//GameObject stoneUI = GameObject.Find("stoneUI").GetComponent<GameObject>();
//		//stoneUI.enabled;
//		heartSlider = GameObject.Find ("heartSlider").GetComponent<Slider> ();
//		starSlider = GameObject.Find ("starSlider").GetComponent<Slider> ();
//		stoneSlider = GameObject.Find ("stoneSlider").GetComponent<Slider> ();
//		moodText = GameObject.Find ("moodText").GetComponent<Text> ();
//		interestText = GameObject.Find ("interestText").GetComponent<Text> ();
//		depressionBurden = GameObject.Find ("depressionBurden").GetComponent<Text> ();
//		moodText.text = "mood  " + heartScore;
//		interestText.text = "interest  " + starScore;
//		depressionBurden.text = stoneMass+"%";
//		heartSlider.value = heartScore;
//		starSlider.value = starScore;fi
//		stoneSlider.value = stoneMass;
//
//	}
//

	//make doctor appear
//	void Doctor(){
//		Vector2 docPos = new Vector2(5.5f,1f);
//		Instantiate (doctor, docPos, transform.rotation);
//		doctorNoShow = false;
//	}
//
//	//doctor effect
//	void DoctorHelp(){
//		AudioSource.PlayClipAtPoint(pickupClip, transform.position);
//		heartScore += 100  ;
//		starScore += 100 ;
//		speed += 5f;
//		jumpSpeed += 250f;
//		//change stone mass
//		stoneMass = 80;
//		stone.transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
//
//	}
	/*
	void Movement()
	{
		//var offsetPos = transform.position;
		if (moveHori > 0)
		{
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			//offsetPos.x += 1f;
			transform.eulerAngles = new Vector2(0,0);
		}
		if (moveHori < 0)
		{
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			//offsetPos.x -= 1f;
			transform.eulerAngles = new Vector2(0,180);
		}
		//transform.position = offsetPos;
	}*/


}
