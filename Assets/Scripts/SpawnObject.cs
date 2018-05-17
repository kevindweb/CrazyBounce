using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

	public GameObject[] objects;
	public GameObject leftSide;
	public GameObject rightSide;
	public float spawnMin = 1f;
	public float spawnMax = 2f;
	public float yOffset = 1f;

	// Use this for initialization
	void Start () {
		Spawn();
	}

	// Update is called once per frame
	void Spawn () {
		Vector3 pos = transform.position;
		float width = leftSide.transform.localScale.x / 2.0f;
		// width of destroyer walls
		float left = leftSide.transform.position.x + width;
		float right = rightSide.transform.position.x - width;
		// get the two walls on the sides
		GameObject obj = objects[Random.Range(0, objects.GetLength(0))];
		float xWidth = obj.transform.localScale.x / 2.0f;
		Vector3 spawnLocation = new Vector3(pos.x + Random.Range(left + xWidth, right - xWidth), pos.y + Random.Range(-yOffset, yOffset), pos.z);
		// shift ledge randomly based on camera position
		Instantiate(obj, spawnLocation, Quaternion.identity);
		Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}
}
