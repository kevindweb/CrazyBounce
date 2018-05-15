using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public float sideSpeed = 2.0f;
	public int bouncy = 11;
	private bool bouncing = false;
	private Rigidbody2D rg2d;

	void Awake(){
		rg2d = gameObject.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		// player will only be able to move sideways!!
		float x = Input.GetAxis("Horizontal") * sideSpeed * Time.deltaTime;
		Vector3 coordinates = new Vector3(x, 0, 0);
		transform.Translate(coordinates);
		// move sideways

		if(bouncing){
			// let's bounce up!
			bouncing = false;
			rg2d.velocity = Vector2.zero;
			// remove gravity effects
			rg2d.AddForce(new Vector2(0, bouncy), ForceMode2D.Impulse);
			// shoot up with bouncy amount
		}
	}

	void OnTriggerEnter2D(Collider2D collidedLedge){
		GameObject ledge = collidedLedge.gameObject;
		string tag = ledge.tag;
		Transform colliderTransform = ledge.transform;
		float topOfLedge = colliderTransform.position.y + (colliderTransform.localScale.y / 2);
		float bottomOfPlayer = transform.position.y - (transform.localScale.y / 2);
		// get the measurements for collision detection
		if(tag == "Ledge" && ((bottomOfPlayer + .3f) >= topOfLedge)){
			// trigger tells us when ball has landed on top of ledge
			bouncing = true;
		}
	}

}
