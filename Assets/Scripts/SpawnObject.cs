using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

	public GameObject[] objects;
	public float spawnMin = 2f;
	public float spawnMax = 4f;

	// Use this for initialization
	void Start () {
		Spawn();
	}

	// Update is called once per frame
	void Spawn () {
		Instantiate(objects[Random.Range(0, objects.GetLength(0))], transform.position, Quaternion.identity);
		Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}
}
