using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batty : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Mage");
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<SpriteRenderer>().isVisible)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position + Vector3.up * 0.3f, 2 * Time.deltaTime);
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Attack")
        {
            Destroy(gameObject);
        }
    }
}
