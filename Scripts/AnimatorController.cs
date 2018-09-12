using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AnimatorController : MonoBehaviour {
	private Animator m_Animatior;
	private Scene scene;

	// Use this for initialization
	void Start () {
		m_Animatior = GetComponent<Animator> ();
		scene = SceneManager.GetActiveScene();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (scene.name == "level1") {
			m_Animatior.SetFloat ("Speed", Mathf.Abs (PlayerMovementNoStone.moveHori));
			m_Animatior.SetBool ("isGrounded", PlayerMovementNoStone.isGrounded);
		} else {
			m_Animatior.SetFloat ("Speed", Mathf.Abs (PlayerMovement.moveHori));
			m_Animatior.SetBool ("isGrounded", PlayerMovement.isGrounded);
		}
	}
}
