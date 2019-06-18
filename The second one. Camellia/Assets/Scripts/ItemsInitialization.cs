using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInitialization : MonoBehaviour {

	static public bool[] itemsState = new bool[10] {true, true, true, true, true, true, true, true, false, false};
	static public bool pillarFalled = false;
	static public bool noBridge = false;

	void Start () {
		int i = 0;
		foreach (Transform child in transform) {
			child.gameObject.SetActive (itemsState [i]);
			i++;
		}

		GameObject.Find ("Pillar").GetComponent<Animator> ().SetBool ("falled", pillarFalled); 

		if (noBridge)
			Destroy(GameObject.Find("Bridge"));
	}

}
