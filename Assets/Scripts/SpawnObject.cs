using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {
	public GameObject[] objects;
	public GameObject[] items;
	public GameObject leftSide;
	public GameObject rightSide;
	public int rarity = 25;
	// rarity of 100 means item will spawn 1/100 times
	public int step = 50;
	// step is how often we decrease spawn rates by taking more time between spawns
	public float spawnMin = 1f;
	public float spawnMax = 3f;
	private float prevHeight;
	private float height;
	// Use this for initialization
	void Start () {
		prevHeight = transform.position.y;
		height = transform.position.y;
		Spawn();
	}

	void Spawn () {
		height = transform.position.y;
		if((height - prevHeight) > step){
			// decrease spawn rate by height increments
			prevHeight = height;
			spawnMin += 0.2f;
		}
		Vector3 pos = transform.position;
		float width = leftSide.transform.localScale.x / 2.0f;
		// width of destroyer walls
		float left = leftSide.transform.position.x + width;
		float right = rightSide.transform.position.x - width;
		// get the two walls on the sides
		GameObject obj = objects[Random.Range(0, objects.GetLength(0))];
		float xWidth = obj.transform.localScale.x / 2.0f;
		float yHeight = obj.transform.localScale.y / 2.0f;
		float x = pos.x + Random.Range(left + xWidth, right - xWidth);
		// place object randomly in between two destroyers
		float y = pos.y;
		Vector3 spawnLocation = new Vector3(x, y, pos.z);
		// shift ledge randomly based on camera position
		Instantiate(obj, spawnLocation, Quaternion.identity);
		int spawnItem = Random.Range(0, rarity);
		// only spawn by a rarity count
		if(spawnItem == 0){
			GameObject item = items[Random.Range(0, items.GetLength(0))];
			Vector3 itemLocation = new Vector3(x, y + yHeight + (item.transform.localScale.y / 2.0f), pos.z);
			// center object directly above this ledge
			Instantiate(item, itemLocation, Quaternion.identity);
		}
		if(spawnMax > spawnMin){
			// this will catch decrease in max spawn rate
			// so we don't have Random.Range(bigger number, smaller number) which would error
			Invoke("Spawn", Random.Range(spawnMin, spawnMax));
		} else {
			Invoke("Spawn", spawnMin);
		}
	}
}
