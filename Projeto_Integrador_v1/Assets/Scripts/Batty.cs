using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batty : MonoBehaviour {

    GameObject player;
    SpriteRenderer sr;
    AudioSource aS;
    int life = 2;
    bool hit = false;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        aS = GetComponent<AudioSource>();
        player = GameObject.Find("Mage");
	}
	
	// Update is called once per frame
	void Update () {
        if (sr.isVisible)
            aS.volume = 1;
        else
            aS.volume = 0;
        if (life <= 0)
            Destroy(gameObject);
        if (sr.isVisible && !hit)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position + Vector3.up * 0.3f, 2 * Time.deltaTime);
        }
        else if (hit)
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 10, transform.position.y + 10), 4 * Time.deltaTime);
    }

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Attack")
        {
            life -= col.GetComponent<AttackAtribute>().GetDamage();
            if(life > 0)
            {
                    sr.color = Color.red;
                    yield return new WaitForSeconds(0.05f);
                    sr.color = Color.white;
                    yield return new WaitForSeconds(0.05f);
                }
        }
        if(col.tag == "Player")
        {
            hit = true;
            yield return new WaitForSeconds(0.2f);
            hit = false;
        }
        yield return null;
    }
}
