using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour {

	public float speed = 2;

	private int[] borders = new int[2] {114, 128};
	private Vector2 movingVector = Vector2.left;

	void Update () {
		transform.Translate (movingVector * speed * Time.deltaTime);

		// Left/Right border check
		if (transform.localPosition.x < borders[0]) {
			movingVector = Vector2.right;
			GetComponent<SpriteRenderer> ().flipX = true;
		}
		else if (transform.localPosition.x > borders[1]) {
			movingVector = Vector2.left;
			GetComponent<SpriteRenderer> ().flipX = false;
		}
	}

}
