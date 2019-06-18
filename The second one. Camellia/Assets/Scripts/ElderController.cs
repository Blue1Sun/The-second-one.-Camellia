using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderController : MonoBehaviour {

	private bool seePlayer = false;
	private int attacksPerformed = 0;
	private int maxAmountAttacks = 4;

	void Update(){
		//TODO: remove Input check, make check on object state
		if(Input.GetButtonDown("Attack") && seePlayer && !FindObjectOfType<CharController>().inDialog){
			attacksPerformed ++;
			if (attacksPerformed > maxAmountAttacks) {
				Comment ();
			}
		}
	}

	void Comment(){
		attacksPerformed = 0;
		transform.GetChild (0).gameObject.SetActive (true);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Character") {
			seePlayer = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.name == "Character") {
			seePlayer = false;
			transform.GetChild (0).gameObject.SetActive (false);
		}
	}
}
