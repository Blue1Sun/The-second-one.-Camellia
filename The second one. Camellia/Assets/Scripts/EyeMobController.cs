using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMobController : MonoBehaviour {

	public int damageOnExplosion;
	public float speed = 2;
	public float damageRadius = 1.32f;

	private bool attacking = false;
	private Transform playerPos;

	void Start(){
		playerPos = GameObject.Find ("Character").GetComponent<Transform> ();
	}

	void Update(){
		//TODO: make hearing imitation + memorize last player position + when on target position walk circles
		if (attacking) {
			if (playerPos.position.x < transform.position.x) {
				Moving (transform.localScale.x == -1);
			}
			if (playerPos.position.x > transform.position.x) {
				Moving (transform.localScale.x == 1);
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

	//TODO: make better (changing variable in animator)
	public void StartAttack(){
		attacking = true;
	}

	public void MobExplode(){
		gameObject.AddComponent<CircleCollider2D>();
		gameObject.GetComponent<CircleCollider2D> ().isTrigger = true;
		gameObject.GetComponent<CircleCollider2D> ().radius = damageRadius;
	}
	public void MobDestroy(){
		Destroy(gameObject);
	}
}
