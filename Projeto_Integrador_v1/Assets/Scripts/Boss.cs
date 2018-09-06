using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public GameObject evilFireball;
    public GameObject evilThunder;


    GameObject evilMage;
    SpriteRenderer evilImage;
    Animator animaBoss;
    Animator teleport;
    BoxCollider2D evilBox;
    Vector3 evilBoxSize, rightSide = new Vector3(6.5f, -2.75f), leftSide = new Vector3(-6.5f, -2.75f);
    int life = 3, rnd = 1, side = 0;

	// Use this for initialization
	void Start () {

        evilMage = GameObject.Find("EvilImage");
        teleport = GameObject.Find("Teleport").GetComponent<Animator>();
        evilBox = gameObject.GetComponent<BoxCollider2D>();
        evilImage = evilMage.GetComponent<SpriteRenderer>();
        animaBoss = evilMage.GetComponent<Animator>();
        evilBoxSize = evilBox.size;
	}
	
	// Update is called once per frame
	void Update () {
        if (evilImage.sortingOrder == -5)
            evilBox.size = new Vector3(0, 0, 0);
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(Attack1());
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(Attack2());
        }
    }

    IEnumerator Attack1()
    {
        rnd = Random.Range(1, 3);

        teleport.Play("Teleport");
        yield return new WaitForSeconds(0.3f);
        if(rnd == 1)
        {
            transform.position = rightSide;
            transform.localScale = new Vector3(2, 2, 2);
            side = 180;
        }
        else
        {
            transform.position = leftSide;
            transform.localScale = new Vector3(-2, 2, 2 );
            side = 0;
        }
        evilBox.size = new Vector3(0, 0, 0);
        teleport.Play("Backteleport");
        animaBoss.Play("Boss_idleside");
        evilImage.sortingOrder = 1;
        yield return new WaitForSeconds(0.3f);
        teleport.Play("noteleport");
        evilBox.size = evilBoxSize;


        animaBoss.Play("Boss_attack_1");
        yield return new WaitForSeconds(0.35f);
        Instantiate(evilFireball, new Vector2(transform.position.x + 0.6f, -3.1f), Quaternion.Euler(Vector3.up * side));
        yield return new WaitForSeconds(0.35f);
        Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -2.6f), Quaternion.Euler(Vector3.up * side));
        yield return new WaitForSeconds(0.35f);
        Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -3.6f), Quaternion.Euler(Vector3.up * side));
        yield return new WaitForSeconds(0.35f);
        Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -2.1f), Quaternion.Euler(Vector3.up * side));
        if(life <= 2)
        {
            yield return new WaitForSeconds(0.35f);
            Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -3.6f), Quaternion.Euler(Vector3.up * side));
            Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -3.1f), Quaternion.Euler(Vector3.up * side));
            yield return new WaitForSeconds(0.35f);
            Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -2.6f), Quaternion.Euler(Vector3.up * side));
            Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -2.1f), Quaternion.Euler(Vector3.up * side));
        }
        if (life == 1)
        {
            yield return new WaitForSeconds(0.35f);
            Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -2.6f), Quaternion.Euler(Vector3.up * side));
            Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -2.1f), Quaternion.Euler(Vector3.up * side));
            yield return new WaitForSeconds(0.35f);
            Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -3.1f), Quaternion.Euler(Vector3.up * side));
            Instantiate(evilFireball, new Vector2(transform.position.x + 0.5f, -3.6f), Quaternion.Euler(Vector3.up * side));
        }
        animaBoss.Play("Boss_idleside");

        yield return null;
    }

    IEnumerator Attack2()
    {

        rnd = Random.Range(1, 3);

        teleport.Play("Teleport");
        yield return new WaitForSeconds(0.3f);
        if (rnd == 1)
        {
            transform.position = rightSide;
            transform.localScale = new Vector3(2, 2, 2);
            side = 180;
        }
        else
        {
            transform.position = leftSide;
            transform.localScale = new Vector3(-2, 2, 2);
            side = 0;
        }
            evilBox.size = new Vector3(0, 0, 0);
            teleport.Play("Backteleport");
            animaBoss.Play("Boss_idleside");
            evilImage.sortingOrder = 1;
            yield return new WaitForSeconds(0.3f);
            teleport.Play("noteleport");
            evilBox.size = evilBoxSize;
       

        for (int i = 0; i<3; i++)
        {
            animaBoss.Play("Boss_attack_1");
            yield return new WaitForSeconds(0.2f);
            Instantiate(evilThunder, new Vector2(transform.position.x -1f, -3.1f), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            animaBoss.Play("Boss_idleside");
            yield return new WaitForSeconds(2f);


        }
        yield return null;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Attack")
        {
            life--;
            for (int i = 0; i < 3; i++)
            {
                evilImage.color = Color.red;
                yield return new WaitForSeconds(0.05f);
                evilImage.color = Color.white;
                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return null;
    }


}
