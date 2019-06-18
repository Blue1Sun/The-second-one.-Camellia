using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogZoneInitialization : MonoBehaviour {

	void Start () {
		if (TextBoxManager.currentDialog == 0) {
			BoxCollider2D beginning = gameObject.AddComponent<BoxCollider2D> ();
			beginning.isTrigger = true;
			beginning.size = new Vector2 (6.3f, 5.1f);
			beginning.offset = new Vector2 (103.6f, -23.5f);
		}

		if (TextBoxManager.currentDialog < 2 || (TextBoxManager.currentDialog == 4 && UIController.notesFound == 3)) {
			BoxCollider2D elderDialog = gameObject.AddComponent<BoxCollider2D> ();
			elderDialog.isTrigger = true;
			elderDialog.size = new Vector2 (8.9f, 4.4f);
			elderDialog.offset = new Vector2 (140.5f, -22);

		}

		if (TextBoxManager.currentDialog < 3 || (TextBoxManager.currentDialog == 3 && CharController.haveBook)) {
			BoxCollider2D trollDialog = gameObject.AddComponent<BoxCollider2D> ();
			trollDialog.isTrigger = true;
			trollDialog.size = new Vector2 (5, 7);
			trollDialog.offset = new Vector2 (302.5f, -11.4f);
		}

		if (TextBoxManager.currentDialog < 6) {
			BoxCollider2D queenDialog = gameObject.AddComponent<BoxCollider2D> ();
			queenDialog.isTrigger = true;
			queenDialog.size = new Vector2 (12.1f, 8.7f);
			queenDialog.offset = new Vector2 (413.9f, 56.4f);
		}
	}
	

}
