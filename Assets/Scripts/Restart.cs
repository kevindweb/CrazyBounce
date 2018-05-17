using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour {

	void OnGUI(){
		int h = 30;
		int w = 100;
		int score = PlayerPrefs.GetInt("playerHeight");
		int highScore = PlayerPrefs.GetInt("highScore");
		// Debug.Log("highScore: " + highScore);
		float height = (Screen.height-h)/2;
		float width = (Screen.width-w)/2;
		GUI.Label(new Rect(width, height, w, h), "GAME OVER!!");
		GUI.Label(new Rect(width, height + h, w, h), "End Height: " + (int) score);
		if(score >= highScore){
			GUI.Label(new Rect(width, height + 2*h, w, h), "High Score!!!");
		}
	}

	public void RestartGame(){
		SceneManager.LoadScene("JumpScene1");
	}
}
