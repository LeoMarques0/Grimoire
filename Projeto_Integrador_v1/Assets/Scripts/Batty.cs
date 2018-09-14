using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batty : MonoBehaviour {

    GameObject player;
    SpriteRenderer sr;
    int life = 2;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Mage");
	}
	
	// Update is called once per frame
	void Update () {
        if (life <= 0)
            Destroy(gameObject, 0.3f);
        if (sr.isVisible)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position + Vector3.up * 0.3f, 2 * Time.deltaTime);
        }
	}

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Attack")
        {
            life -= col.GetComponent<AttackAtribute>().GetDamage();
            for (int i = 0; i < 3; i++)
            {
                sr.color = Color.red;
                yield return new WaitForSeconds(0.05f);
                sr.color = Color.white;
                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return null;
    }
}
