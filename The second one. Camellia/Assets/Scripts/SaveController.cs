using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour {

	void Start(){
		if (SceneManager.GetActiveScene().buildIndex == 0 && !File.Exists(System.IO.Directory.GetCurrentDirectory() + "/gamesave.save")){
			GameObject.Find ("Continue Button").SetActive(false);
		}
	}

	public void NewGame(){
		CharController.startPosX = 102.78f;
		CharController.startPosY = -25.78f;
		CharController.startHealth = 100;
		CharController.haveBook = false;
		CharController.haveKey = false;

		UIController.notesFound = 0;

		TextBoxManager.currentDialog = 0;
		TextBoxManager.currentLine = 0;

		ItemsInitialization.itemsState = new bool[10] {true, true, true, true, true, true, true, true, false, false};
		ItemsInitialization.pillarFalled = false;
		ItemsInitialization.noBridge = false;

		SceneManager.LoadScene ("Loading Screen");
	}

	public void Loading(){
		Time.timeScale = 1;
		if (File.Exists (System.IO.Directory.GetCurrentDirectory () + "/gamesave.save")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (System.IO.Directory.GetCurrentDirectory () + "/gamesave.save", FileMode.Open);
			Save save = (Save)bf.Deserialize (file);
			file.Close ();

			CharController.startPosX = save.charPositionX;
			CharController.startPosY = save.charPositionY;
			CharController.startHealth = save.health;
			CharController.haveBook = save.haveBook;
			CharController.haveKey = save.haveKey;

			UIController.notesFound = save.notesFound;

			TextBoxManager.currentDialog = save.currentDialog;
			TextBoxManager.currentLine = save.currentLine;

			ItemsInitialization.itemsState = save.itemsState;
			ItemsInitialization.pillarFalled = save.pillarFalled;
			ItemsInitialization.noBridge = save.noBridge;

			SceneManager.LoadScene ("Loading Screen");
		} else {
			NewGame ();
		}
	}

	public void Saving(GameObject nearSave){
		CharController character = FindObjectOfType<CharController> ();
		Transform items = GameObject.Find ("Items").GetComponent<Transform>();
		bool[] itemsState = new bool[items.childCount];
		int notesFound = UIController.notesFound; 
		int i = 0;
		foreach (Transform item in items) {
			itemsState [i] = item.gameObject.activeSelf;
			i++;
		}
		int currentDialog = TextBoxManager.currentDialog;
		int currentLine = TextBoxManager.currentLine;
		bool haveKey = CharController.haveKey;
		bool haveBook = CharController.haveBook;
		bool pillarFalled = GameObject.Find ("Pillar").GetComponent<Animator>().GetBool("falled");
		bool noBridge = (GameObject.Find ("Bridge") == null);

		Save save = new Save (character.transform.position.x, character.transform.position.y, character.health, itemsState, 
			notesFound, currentDialog, currentLine, haveKey, haveBook, pillarFalled, noBridge);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(System.IO.Directory.GetCurrentDirectory()+ "/gamesave.save");
		bf.Serialize(file, save);
		file.Close();
		nearSave.GetComponent<Animator> ().SetTrigger ("saved");
	}
}
