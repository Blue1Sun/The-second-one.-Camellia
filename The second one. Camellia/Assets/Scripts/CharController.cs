using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour {

	// Variables from savedata
	static public float startPosX = 102.78f;
	static public float startPosY = -25.78f;
	static public float startHealth = 100;
	static public bool haveKey = false; 
	static public bool haveBook = false; 

	// TODO: convert to private variables, create functions for editing + in other scripts too
	public float maxHealth = 100;
	public float runningSpeed = 4.5f;
	public float elevatorControlSpeed = 2;
	public float jumpForce;
	public float attackSpeed = 4;
	public float fallingStartpoint = -6;
	public float health;
	public UIController ui;
	public GameObject elevator;
	public bool inDialog = false;
	public int damage = 20;

	private int[] elevatorBorders = new int[2] {26, 51};
	private bool onGround = true;
	private bool facingRight = true;
	private bool onElevator = false;
	private GameObject nearSave = null;
	private GameObject nearItem = null;

	private Animator animator;
	private Rigidbody2D charRigidbody;

	void Start () {
		animator = GetComponent<Animator> ();
		charRigidbody = GetComponent<Rigidbody2D> ();
		transform.position = new Vector3 (startPosX, startPosY, 0);
		health = startHealth;
	}


	void Update () {
		if (Time.timeScale == 1 && !IsCharState("death")) {
			if (!inDialog) {
				//Attacking
				if (Input.GetButtonDown ("Attack") && (IsCharState ("idle") || IsCharState ("run"))) {
					animator.SetTrigger ("attack");
				}
				if (IsCharState ("attack")) {
					transform.Translate (Vector3.right * transform.localScale.x * attackSpeed * Time.deltaTime);
				}

				//Falling
				if (charRigidbody.velocity.y < fallingStartpoint && !IsCharState ("jump") && !IsCharState ("jump start")) {
					animator.SetTrigger ("fall");
					onGround = false;
				}

				//Jumping
				if (Input.GetButtonDown ("Jump") && onGround) {
					animator.SetTrigger ("jump");
					charRigidbody.AddForce (new Vector2 (0, jumpForce));
					onGround = false;
				}

				//Moving
				if (Input.GetAxis ("Horizontal") < 0) {
					CharMoving (facingRight);
				} else if (Input.GetAxis ("Horizontal") > 0) {
					CharMoving (!facingRight);
				} else {
					animator.SetBool ("running", false);
				}

				//Elevator movement
				if (haveKey) {
					if (Input.GetAxis ("Vertical") < 0 && onElevator && elevator.transform.position.y > elevatorBorders [0])
						elevator.transform.Translate (Vector2.down * elevatorControlSpeed * Time.deltaTime);
					else if (Input.GetAxis ("Vertical") > 0 && onElevator && elevator.transform.position.y < elevatorBorders [1])
						elevator.transform.Translate (Vector2.up * elevatorControlSpeed * Time.deltaTime);
					else if (!onElevator && elevator.transform.position.y > elevatorBorders [0])
						elevator.transform.Translate (Vector2.down * (elevatorControlSpeed * 3) * Time.deltaTime);
				}

				//Saving, Picking up notes and heals
				if (Input.GetButtonDown ("Use")) {
					if (nearSave != null)
						FindObjectOfType<SaveController> ().Saving (nearSave);
					if (nearItem != null) {
						if (nearItem.tag == "Heal") {
							GetHeal (30);
						}
						if (nearItem.tag == "Note") {
							ui.ShowNote ();
							//Cutscene
							if (nearItem.name == "3n") {
								GameObject.Find ("Pillar").GetComponent<Animator> ().SetTrigger ("EarthQuake"); 
								GameObject.Find ("Pillar").GetComponent<Animator> ().SetBool ("falled", true); 
							}
						}
						if (nearItem.tag == "Book") {
							haveBook = true;
							//New Dialog with picked book
							GameObject dialogZone = GameObject.Find ("Dialog Zone");
							BoxCollider2D newZone = dialogZone.AddComponent<BoxCollider2D> ();
							newZone.isTrigger = true;
							newZone.size = new Vector2 (5, 7);
							newZone.offset = new Vector2 (302.5f, -11.4f);
						}
						nearItem.SetActive (false);
						nearItem = null;
					}
				}
			} else {
				//Talking
				if (Input.GetButtonDown ("Use")) {
					FindObjectOfType<TextBoxManager> ().nextMessage ();
				}
			}

		}
	}

	public void OnGround(bool value){
		onGround = value;
	}

	void CharMoving(bool changeDirection) {
		animator.SetBool ("running", true);
		Vector2 currentDirection = new Vector2 (1, 0) * transform.localScale.x;
		transform.Translate (currentDirection * runningSpeed * Time.deltaTime);

		if (changeDirection) {
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale; 
			facingRight = !facingRight;
		}
	}

	bool IsCharState (string state) {
		return animator.GetCurrentAnimatorStateInfo (0).IsName (state);
	}

	void GetHeal (int heal){		
		health = Mathf.Min(maxHealth, health + heal);
		ui.SetHealthBar (health);
	}

	void GetDamage (int damage){
		health -= damage;
		ui.SetHealthBar (health);
		if (health <= 0 && !IsCharState("death")) {
			StopAllCoroutines ();
			animator.SetTrigger ("death");
		} else if (damage > 0){
			animator.SetTrigger ("damaged");
		}
	}
	// Periodical damage
	IEnumerator GetDamageCoroutine (int damage){
		while (health > 0) {
			GetDamage (damage);
			if (health <= 0) {
				StopAllCoroutines ();
			}
			yield return new WaitForSeconds (1.5f);
		}
	}

	void Dying(){
		FindObjectOfType<UIController> ().Death ();
	}

	// Collision detection
	void OnCollisionEnter2D(Collision2D col){
		//Moving with Boat
		if (col.gameObject.tag == "Boat") {
			transform.parent = col.transform;
		}
		//Landing
		if (col.gameObject.name != "Walls" && (IsCharState ("jump") || IsCharState ("damage") || IsCharState("attack")) && !onGround) {		
			animator.SetTrigger ("groundReached");	
			onGround = true;
		}
	}
	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Boat") {
			transform.parent =  null;
		}
	}
			
	// Trigger Enter detection
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Dialog Zone") {
			animator.SetBool ("running", false);
			Destroy (col);
			inDialog = true;
			FindObjectOfType<TextBoxManager> ().firstMessage ();
		}
		if (col.gameObject.name == "DangerZone") {
			StartCoroutine(GetDamageCoroutine(10));
		}
		if (col.gameObject.name == "DeathZone") {
			GetDamage (100);
		}
		if (col.gameObject.name == "Bridge") {
			Destroy (col.gameObject);
		}
		if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.name == "SaveZone") {
			nearSave = col.gameObject;
		}
		if (col.gameObject.name == "Elevator") {
			onElevator = true;
		}
		if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.name == "Items") {
			nearItem = col.gameObject;
		}

		if (col.gameObject.tag == "EyeMob") {
			// BoxCollider == hidden, CircleCollider == damage
			if (col.GetType () == typeof(BoxCollider2D)) {
				col.gameObject.GetComponent<Animator> ().SetTrigger ("stepped");
				Destroy (col);
			} else {
				GetDamage (col.gameObject.GetComponent<EyeMobController>().damageOnExplosion);
				Destroy (col);
			}
		}

		if (col.gameObject.tag == "Ghost") {
			StartCoroutine(GetDamageCoroutine(col.gameObject.GetComponent<GhostMobController>().damage));
			if (IsCharState ("attack")) {
				col.gameObject.GetComponent<GhostMobController> ().GetDamage (damage);
			}
		}
		if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.tag  == "Ghost") {
			col.gameObject.transform.parent.GetComponent<GhostMobController> ().seenPlayer = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.name == "DangerZone") {
			StopAllCoroutines ();
		}
		if (col.gameObject.tag == "Ghost") {
			StopAllCoroutines ();
		}
		if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.name == "SaveZone") {
			nearSave = null;
		}
		if (col.gameObject.name == "Elevator") {
			onElevator = false;
		}
		if (col.gameObject.transform.parent != null && col.gameObject.transform.parent.name == "Items") {
			nearItem = null;
		}
	}
}