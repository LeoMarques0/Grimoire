using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFireball : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 12;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sr.isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Boss" && col.tag != "Enemy" && col.tag != "EvilAttack" && col.tag != "Attack")
        {
            Destroy(gameObject);
        }
        if (col.tag == "Player")
            GameObject.Find("Mage").GetComponent<Player>().vida -= 2;
    }
}
