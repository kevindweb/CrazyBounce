using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// to load game over scene

public class DestroyObject : MonoBehaviour {
	public GameObject spawner;

	void OnTriggerEnter2D(Collider2D other){
		GameObject collider = other.gameObject;
		if(collider.tag == "Player"){
			Destroy(spawner);
			Debug.Log("Player out of bounds!!");
			SceneManager.LoadScene("GameOver");
			// load game over scene
			return;
		}
		Destroy(collider);
	}
}
