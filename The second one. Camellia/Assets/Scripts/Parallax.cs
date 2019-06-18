using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	//TODO: change class name, nomore parallax, maybe skyController

	public float skySpeed = 0.5f;

	private Vector3 playerPos;

	private Color curColor;
	private Color forestColor = new Color (1, 0.7f, 0.7f);
	private Color desertColor = new Color (0.6f, 0.6f, 0.6f);
	private Color iceColor = new Color (0.9f, 1, 0.7f);
	private Color endColor = Color.white;
	
	void Update(){	
		transform.Translate ( Vector2.right * skySpeed * Time.deltaTime);
		playerPos = GameObject.Find ("Character").GetComponent<Transform> ().position;	

		// Location color change
		foreach (Transform child in transform) {
			curColor = child.GetComponent<SpriteRenderer> ().color;
			child.GetComponent<SpriteRenderer> ().color = Color.Lerp (curColor, endColor, 0.2f * Time.deltaTime);
		}

		if (endColor != Color.white && playerPos.x > 180 && playerPos.x < 200 && playerPos.y > -30 && playerPos.y < -20) {
			endColor = Color.white;
		}
		else if (endColor != forestColor && playerPos.x > 130 && playerPos.x < 150 && playerPos.y > 10 && playerPos.y < 20) {
			endColor = forestColor;
		}
		else if (endColor != desertColor && playerPos.x > 200 && playerPos.x < 335 && playerPos.y > 20 && playerPos.y < 30) {
			endColor = desertColor;
		}
		else if (endColor != iceColor && playerPos.x > 335 && playerPos.x < 445 && playerPos.y > 25 && playerPos.y < 55) {
			endColor = iceColor;
		}
		else if (endColor != Color.white && playerPos.x > 445 && playerPos.x < 455 && playerPos.y > 50 && playerPos.y < 60) {
			endColor = Color.white;
		}

		// Autocompleting sky
		if (playerPos.x > transform.GetChild (0).position.x) {
			transform.position = new Vector3 (transform.position.x + 45.6f, transform.position.y);
		}
		else if (playerPos.x < transform.GetChild (1).position.x) {
			transform.position = new Vector3 (transform.position.x - 45.6f, transform.position.y);
		}

		if (playerPos.y > transform.GetChild (0).position.y + 6) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 43.2f);
		}
		else if (playerPos.y < transform.GetChild (2).position.y - 6) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - 43.2f);
		}
	}
}
