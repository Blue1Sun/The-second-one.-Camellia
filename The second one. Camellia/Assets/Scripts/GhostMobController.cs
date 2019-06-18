using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMobController : MonoBehaviour {

	public float speed;
	public bool seenPlayer = false;
	public int health = 80;
	public int damage = 25;

	private int[] movementBorders = new int[2] {198, 352}; //ghost location borders
	private bool returning = false;
	private Vector2 spawnPos;
	private Transform playerPos;
	private Animator animator;

	void Start () {
		spawnPos = transform.position;
		animator = GetComponent<Animator> ();
		playerPos = GameObject.Find ("Character").GetComponent<Transform> ();
	}

	void Update () {
		if ((transform.position.x < movementBorders[0] || transform.position.x > movementBorders[1] || Vector2.Distance(transform.position, playerPos.position) > 15) && !returning) {
			seenPlayer = false;
			returning = true;
			StartCoroutine ("Returning");
		}
		else if (seenPlayer) {
			returning = false;
			StopCoroutine ("Returning");
			GoToTarget (playerPos.position); 
		}
	}

	IEnumerator Returning(){		
		while (!seenPlayer) {
			GoToTarget(spawnPos);
			yield return null;
		}
	}

	void GoToTarget(Vector2 target){
		if (!IsCharState ("angry")) {
			if (target.x < transform.position.x) {
				Moving ((transform.localScale.x == -1));
			}
			if (target.x > transform.position.x) {
				Moving ((transform.localScale.x == 1));
			}
		}
	}

	void Moving(bool changeDirection) {
		Vector2 currentDirection = new Vector2 (-1, 0) * transform.localScale.x;
		transform.Translate (currentDirection * speed * Time.deltaTime);

		if (changeDirection) {
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale; 
		}
	}

	public void GetDamage(int damage){
		if (IsCharState("angry"))
			health -= damage;
		if (health <= 0)
			Destroy (gameObject);
	}

	// Function called from animator
	public void EnableCollider (int value){
		if (value == 0 && damage == 25) {
			GetComponent<BoxCollider2D> ().enabled = false;
			damage = 0;
			GetComponent<BoxCollider2D> ().enabled = true;
		} else if (value == 1 && damage == 0) {
			GetComponent<BoxCollider2D> ().enabled = false;
			damage = 25;
			GetComponent<BoxCollider2D> ().enabled = true;
		}
	}

	bool IsCharState (string state) {
		return animator.GetCurrentAnimatorStateInfo (0).IsName (state);
	}
}
