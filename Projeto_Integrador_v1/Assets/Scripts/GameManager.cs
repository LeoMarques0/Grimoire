using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager singleton = null;
    public int vida = 20;
	// Use this for initialization
	void Start () {
        if (singleton == null)
            singleton = this;
        else if(singleton != this)
            Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
