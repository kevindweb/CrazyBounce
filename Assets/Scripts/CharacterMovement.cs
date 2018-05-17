using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// to load game over scene

public class CharacterMovement : MonoBehaviour {
	public GameObject spawner;
	public float sideSpeed = 2.0f;
	public float modifier = 1.1f;
	private int increaseCount = 0;
	public int bouncy = 11;
	public int boost = 2;
	private bool boosted = false;
	private bool bouncing = false;
	private float savedTime;
	private Rigidbody2D rg2d;


	void Awake(){
		rg2d = gameObject.GetComponent<Rigidbody2D>();
		savedTime = Time.time;
	}

	void Update(){
		if(transform.position.y < -10){
			Debug.Log("We lost!");
			Destroy(spawner);
			SceneManager.LoadScene("GameOver");
			// load game over scene
		}
	}

	void FixedUpdate(){
		// player will only be able to move sideways!!
		float x = Input.GetAxis("Horizontal") * sideSpeed * Time.deltaTime;
		Vector3 coordinates = new Vector3(x, 0, 0);
		transform.Translate(coordinates);
		// move sideways
		if(bouncing){
			bouncing = false;
			// set gravity scale back to normal
			rg2d.gravityScale = 1;
			// let's bounce up!
			rg2d.velocity = Vector2.zero;
			// remove gravity effects
			if(boosted){
				Debug.Log("Boost up!");
				boosted = false;
				rg2d.AddForce(new Vector2(0, bouncy * boost), ForceMode2D.Impulse);
				// boost up more than usual
				return;
			}
			rg2d.AddForce(new Vector2(0, bouncy), ForceMode2D.Impulse);
			// shoot up with bouncy amount
		} else if(Time.time - savedTime > 1){
			if(increaseCount%10 == 0){
				// increase every tenth frame
				rg2d.gravityScale *= modifier;
				// increase gravity to fall faster
			}
			increaseCount++;
		}
	}

	void OnTriggerEnter2D(Collider2D collidedLedge){
		GameObject ledge = collidedLedge.gameObject;
		string tag = ledge.tag;
		if(tag == "Boost"){
			// check for boost
			boosted = true;
			Destroy(collidedLedge);
			return;
		}
		Transform colliderTransform = ledge.transform;
		float topOfLedge = colliderTransform.position.y + (colliderTransform.localScale.y / 2);
		float bottomOfPlayer = transform.position.y - (transform.localScale.y / 2);
		// get the measurements for collision detection
		if(tag == "Ledge" && ((bottomOfPlayer + .3f) >= topOfLedge)){
			// trigger tells us when ball has landed on top of ledge
			bouncing = true;
			savedTime = Time.time;
		}
	}

}
