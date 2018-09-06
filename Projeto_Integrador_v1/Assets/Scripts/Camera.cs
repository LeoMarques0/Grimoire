using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject target;
    public float offsetX;
    public float offsetY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3 (target.transform.position.x + offsetX, target.transform.position.y + offsetY, -5);
	}
}
