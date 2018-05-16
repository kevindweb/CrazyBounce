using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		GameObject collider = other.gameObject;
		if(collider.tag == "Player"){
			Debug.Log("Player out of bounds!!");
			Debug.Break();
			return;
		}
		Destroy(collider);
	}
}
