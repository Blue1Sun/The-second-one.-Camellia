using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	static public int currentDialog = 0;
	static public int currentLine = 0;

	public GameObject book;
	public GameObject note;
	public GameObject textBox;
	public Text dialogText;
	public TextAsset textFile;
	public int[] endLine;
	public Color[] boxColor;

	private bool isTyping = false;
	private string[] textLines;
	private string currentTyping = "";
	private CharController character;

	void Start () {
		character = FindObjectOfType<CharController> ();

		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
		}
	}

	public void firstMessage(){
		// Move dialog window
		if (currentDialog > 0 && currentDialog < 5)
			textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0.0344f, 0.02f);
		else if (currentDialog == 5 || currentDialog == 6)
			textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2 (-0.0356f, 0.02f);
		
		if (currentDialog == 2)
			book.SetActive (true);
		else if (currentDialog == 3)
			note.SetActive (true);
		
		textBox.SetActive (true);
		textBox.GetComponent<Image> ().color = boxColor [currentDialog];

		nextMessage ();
	}

	public void nextMessage(){
		if (textLines[currentLine] == "\r") {
			character.inDialog = false;
			textBox.SetActive (false);
			currentDialog++;
			currentLine++;

			//Show Key
			if (currentDialog == 5) {
				CharController.haveKey = true;
				FindObjectOfType<UIController> ().ShowKey ();
			}
		} else {
			//Load all line, if button pressed again
			if (isTyping) {
				StopAllCoroutines ();
				dialogText.text = textLines [currentLine];
				currentLine++;
				isTyping = false;
			} else {
				StartCoroutine (TypingText (textLines [currentLine]));
			}
		}
	}

	//Typing effect on text
	IEnumerator TypingText(string line){
		isTyping = true;
		for (int i = 0; i < line.Length; i++) {
			currentTyping = line.Substring (0, i);
			dialogText.text = currentTyping;
			yield return new WaitForSeconds (0.05f);
		}
		currentLine++;
		isTyping = false;
	}
}
