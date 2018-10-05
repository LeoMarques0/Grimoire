using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {


    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    bool walk = false;
    int spd = 0;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (sr.isVisible && !walk)
        {
            walk = true;
            StartCoroutine("Walk");
        }
        else if (!sr.isVisible)
            walk = false;
        anim.SetBool("isWalking", walk);
        transform.Translate(transform.right * spd * Time.deltaTime);

    }

    IEnumerator Walk()
    {
        while(walk)
        {
            spd = 5;
            yield return new WaitForSeconds(0.5f);
            spd = -5;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
