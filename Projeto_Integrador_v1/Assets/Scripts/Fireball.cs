using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer sr;
    public static int fireballAmount = 0;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 15;
        fireballAmount++;
    }
	
	// Update is called once per frame
	void Update () {
        if (!sr.isVisible)
        {
            fireballAmount--;
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player" && col.tag != "EvilAttack" && col.tag != "Attack")
        {
            Destroy(gameObject);
            fireballAmount--;
        }
    }
}
