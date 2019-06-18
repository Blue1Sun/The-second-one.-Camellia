using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing : MonoBehaviour {

	public float force = 2;
	public float maxYVelocity = 15;
	
	void OnCollisionEnter2D(Collision2D col)
	{	
		if (col.relativeVelocity.y <= 0) {
			col.rigidbody.AddForce (new Vector2 (0, -col.relativeVelocity.y) * force, ForceMode2D.Impulse);
			Vector2 velocity = new Vector2 (col.rigidbody.velocity.x, Mathf.Clamp (col.rigidbody.velocity.y, 0, maxYVelocity));
			col.rigidbody.velocity = velocity;

			col.gameObject.GetComponent<Animator>().SetTrigger ("fall");
			col.gameObject.GetComponent<CharController> ().OnGround (false);
		}
	}
}
