using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawns : MonoBehaviour {

    public GameObject enemy;
    GameObject spawned;
    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (spawned == null && !sr.isVisible)
        {
            spawned = Instantiate(enemy, transform);
        }
    }
}
