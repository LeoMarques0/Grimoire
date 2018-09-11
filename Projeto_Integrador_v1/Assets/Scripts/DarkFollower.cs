using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkFollower : MonoBehaviour {

    public GameObject fireball;

    GameObject player;
    float spd;
    int walk = 1, side = 0;
    bool range = false, isWalking = false, isAttack = false;
    public LayerMask mask;
    Animator anima;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Mage");
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        range = Physics2D.OverlapBox(new Vector3(transform.position.x, transform.position.y + 0.2f), new Vector3(transform.position.x - 7f, 0.1f), 0, mask);
        if (!range && !isWalking)
        {
            walk = 1;
            StopCoroutine("Attack");
            StartCoroutine("WalkPattern");
        }
        else if (range && !isAttack)
        {
            walk = 0;
            StopCoroutine("WalkPatter");
            StartCoroutine("Attack");
        }
        print(range);
        transform.Translate(transform.right * spd * Time.deltaTime);
	}

    IEnumerator WalkPattern()
    {
        isWalking = true;
        while (walk == 1)
        {
            spd = 1;
            transform.localScale = new Vector3(-1, 1, 1);
            side = 0;
            yield return new WaitForSeconds(3);
            spd = -1;
            transform.localScale = new Vector3(1, 1, 1);
            side = 180;
            yield return new WaitForSeconds(3);
        }
        yield return null;
    }

    IEnumerator Attack()
    {
        spd = 0;
        yield return new WaitForSeconds(1f);
        isAttack = true;
        for(int i = 0; i < 3; i++)
        {
            anima.Play("Dark Follower_Attack");
            Instantiate(fireball, new Vector2(transform.position.x + 0.1f, transform.position.y + 1f), Quaternion.Euler(Vector3.up * side));
            yield return new WaitForSeconds(0.3f);
        }
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + 0.2f), new Vector3(transform.position.x - 7f, 0.1f));
    }
}
