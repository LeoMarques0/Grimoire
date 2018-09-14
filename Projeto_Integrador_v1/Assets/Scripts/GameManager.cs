using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager singleton = null;
	// Use this for initialization
	void Start () {
        if (singleton == null)
            singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
