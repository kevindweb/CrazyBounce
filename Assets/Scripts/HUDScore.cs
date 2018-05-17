using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScore : MonoBehaviour {

	public GameObject player;
	public float score = 0f;

	void Update () {
		float yPos = player.transform.position.y;
		if(yPos > score){
			// only update height when we increase it
			score += (yPos - score);
		}
	}

	void OnDestroy(){
		if(!PlayerPrefs.HasKey("highScore")){
			PlayerPrefs.SetInt("highScore", (int) score);
		} else if(score > PlayerPrefs.GetInt("highScore")){
			PlayerPrefs.SetInt("highScore", (int) score);
		}
		PlayerPrefs.SetInt("playerHeight", (int) score);
	}

	void OnGUI(){
		int h = 30;
		int w = 100;
		GUI.Label(new Rect(10, 10, w, h), "Height: " + (int) score);
		// put score in top left of screen
	}
}
