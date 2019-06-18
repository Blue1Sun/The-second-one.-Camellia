using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	static public int notesFound = 0;

	public GameObject pauseMenu;
	public GameObject deathMenu;
	public GameObject noteCount;
	public GameObject note;
	public Sprite[] notes = new Sprite[3];
	public Sprite key;

	private Slider slider;
	private Image sliderimage;

	void Start() {
		slider = GameObject.Find("Slider").GetComponent<Slider>();
		sliderimage = GameObject.Find("Handle").GetComponent<Image>();
		slider.value = CharController.startHealth;

		if (!CharController.haveKey)
			noteCount.GetComponent<Text> ().text = notesFound + "x";
		else
			noteCount.GetComponentInChildren<Image> ().sprite = key;
		
		if (notesFound == 0)
			noteCount.SetActive (false);
	}

	void Update() {
		if (Input.GetButtonDown("Pause") && deathMenu.activeSelf == false) {
			if (note.activeSelf)
				EnableElement(1, note);
			else if (!pauseMenu.activeSelf) {
				EnableElement (0, pauseMenu);
			} else {
				UnPause ();
			}
		} 
	}

	public void SetHealthBar(float health){
		slider.value = health;
	}

	public void HealthBarChange() { 
		if (slider.value > 0)
			sliderimage.color = Color.Lerp (Color.red, Color.white, slider.value / 100);
		else
			sliderimage.color = Color.black;
	}

	public void Death(){
		EnableElement (0, deathMenu);
	}
	public void UnPause(){
		EnableElement (1, pauseMenu);
	}

	public void ToMainMenu(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("Main Menu");
	}

	public void ShowNote(){
		notesFound++;
		note.transform.GetChild(1).GetComponent<Image> ().sprite = notes [notesFound - 1];

		noteCount.SetActive (true);
		noteCount.GetComponent<Text> ().text = notesFound + "x";
		EnableElement (0, note);

		// New Dialog with picked all notes
		if (notesFound == 3) {
			GameObject dialogZone = GameObject.Find ("Dialog Zone");
			BoxCollider2D newZone = dialogZone.AddComponent<BoxCollider2D> ();
			newZone.isTrigger = true;
			newZone.size = new Vector2 (8.9f, 4.4f);
			newZone.offset = new Vector2 (140.5f, -22);
		}
	}

	public void ShowKey(){
		ShowNote ();
		noteCount.GetComponentInChildren<Image> ().sprite = key;
		noteCount.GetComponent<Text> ().text = "";
	}

	void EnableElement(int time, GameObject element){
		Time.timeScale = time;
		element.SetActive (!element.activeSelf);
	}
}
