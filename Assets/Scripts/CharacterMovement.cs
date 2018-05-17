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
	public float boost = 1.5f;
	private bool boosted = false;
	private bool bouncing = false;
	private float savedTime;
	private Rigidbody2D rg2d;


	void Awake(){
		rg2d = gameObject.GetComponent<Rigidbody2D>();
		savedTime = Time.time;
		// DataLoader access = ScriptableObject.CreateInstance("DataLoader") as DataLoader;
		// access.Save("Here is stuff to save!");
		// Debug.Log("data loaded: " + access.Load(string.Empty, "saveFile.dat"));
	}

	void Update(){
		if(transform.position.y < -10){
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

	void OnTriggerEnter2D(Collider2D collidedEdge){
		GameObject obj = collidedEdge.gameObject;
		string tag = obj.tag;
		Transform colliderTransform = obj.transform;
		float topOfObject = colliderTransform.position.y + (colliderTransform.localScale.y / 2);
		float bottomOfPlayer = transform.position.y - (transform.localScale.y / 2);
		// get the measurements for collision detection
		if(tag == "Boost" && (bottomOfPlayer + .3f) >= topOfObject){
			// check for boost
			boosted = true;
			Destroy(collidedEdge);
		} else if(tag == "Ledge" && ((bottomOfPlayer + .3f) >= topOfObject)){
			// trigger tells us when ball has landed on top of ledge
			bouncing = true;
			savedTime = Time.time;
		}
	}

}
