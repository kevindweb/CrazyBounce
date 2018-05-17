using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJumpFollow : MonoBehaviour {
	public int offset = -3;
	public Transform player;

	void Update () {
		if(player){
			// check because walls could have destroyed player
			transform.position = new Vector3(0, player.position.y + offset, -10);
		}
	}
}
