using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public enum MenuType: byte
	{
		MainMenu = 0,
		OptionMenu = 1,
		PauseMenu = 2,
		GameOverMenu = 3,
	}
	public MenuType ActiveMenu { get; set; }
	public bool isMenuActive { get; set; }
	private readonly GUI.WindowFunction[] MenuFunctions = null;
	private readonly string[] MenuNames = new string[]
	{
		"Main Menu",
		"Options Menu",
		"Pause Menu",
		"Gamee Over Menu",
	};

	public AudioClip clickSound;
	private AudioSource m_SoundSource;
	private Settings m_Settings = new Settings();
	public GameManager()
	{
		MenuFunctions = new GUI.WindowFunction[]
		{
			MainMenu,     //0
			OptionMenu,   //1
			PauseMenu,    //2
			GameOverMenu, //3
		};
	}


	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake(){
		DontDestroyOnLoad (gameObject);
		//ActiveMenu = MenuType.MainMenu;
		//isMenuActive = true;
		m_SoundSource = Camera.main.transform.FindChild ("Sound").GetComponent<AudioSource> ();
		m_Settings.Load (Camera.main.transform.FindChild ("Music").GetComponent<AudioSource>() , m_SoundSource);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		int width = Screen.width / 3 * 2;
		int height = Screen.height / 3 * 2;
		//GUI.Label (new Rect(0, 0, 100, 100), "Score: ");
		if (isMenuActive) {
			Rect windowRect = new Rect((Screen.width - width )/2, (Screen.height- height)/2, width, height);
			GUILayout.Window(0, windowRect, MenuFunctions[(byte)ActiveMenu], MenuNames[(byte)ActiveMenu] );

		}
	}

	void MainMenu(int id){
		GUILayout.Label ("The goal of the game is to collect the two items as many as possible:");
		GUILayout.Label ("'Interest' (Star) and 'Mood' (Heart)  ");
		GUILayout.Label (" ");
		GUILayout.Label ("  ");
		GUILayout.Label ("And to get to know yourself well through this experience.");
		if (GUILayout.Button("Let's get started!")) 
		{
			m_SoundSource.PlayOneShot(clickSound);
			isMenuActive = false;
		}

		if (GUILayout.Button ("Options")) 
		{
			m_SoundSource.PlayOneShot(clickSound);
			ActiveMenu = MenuType.OptionMenu;
		}
	}

	void OptionMenu(int id)
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Music Volume: ", GUILayout.Width(90) );
		m_Settings.MusicVolume = GUILayout.HorizontalSlider(m_Settings.MusicVolume, 0.0f, 1.0f);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Sound Volume: ", GUILayout.Width(90));
		m_Settings.SoundVolume = GUILayout.HorizontalSlider(m_Settings.SoundVolume, 0.0f, 1.0f);
		GUILayout.EndHorizontal();
		
		if (GUILayout.Button("Reset High Score"))
		{
			m_SoundSource.PlayOneShot(clickSound);
			m_Settings.HighScore = 0;
		}
		
		if (GUILayout.Button("Back"))
		{
			m_SoundSource.PlayOneShot(clickSound);
			m_Settings.Save();
			ActiveMenu =  MenuType.MainMenu;  //m_SourceMenu; 
		}
	}

	void PauseMenu(int id)
	{
	}

	void GameOverMenu(int id)
	{
	}
}
