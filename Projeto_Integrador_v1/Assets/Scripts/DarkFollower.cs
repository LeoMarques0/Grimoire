using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkFollower : MonoBehaviour {

    public GameObject fireball;

    GameObject player;
    float playerPos;
    float spd;
    int walk = 0, side = 0, life = 3;
    bool range = false, isAttack = false;
    public LayerMask mask;
    Animator anima;
    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Mage");
        sr = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (life <= 0)
            Destroy(gameObject);
        //Area na qual ele ira detectar o personagem.
        if (transform.localScale.x == 1)
        {
            side = 180;
            range = Physics2D.OverlapBox(new Vector3(transform.position.x - 3f, transform.position.y + 0.2f), new Vector3(6, 0.1f), side, mask);
        }
        else
        {
            side = 0;
            range = Physics2D.OverlapBox(new Vector3(transform.position.x + 3f, transform.position.y + 0.2f), new Vector3(6, 0.1f), side, mask);
        }
        playerPos = player.transform.position.x - transform.position.x;
        if (isAttack)
        {
            if (playerPos < 0.1)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }

        //Caso o personagem não esteja no alcance faça o padrão de andar.
        if (!range && !isAttack && walk == 0)
        {
            walk = 1;
            StopCoroutine("Attack");
            StartCoroutine("WalkPattern");
        }
        //caso o personagem esteja no alcance e não ja esteja no padrão de ataque, entre no padrão de ataque.
        else if (range && !isAttack)
        {
            walk = 0;
            isAttack = true;
            StopCoroutine("WalkPatter");
            StartCoroutine("Attack");
        }
        transform.Translate(transform.right * spd * Time.deltaTime);
	}

    //padrão de andar
    IEnumerator WalkPattern()
    {
        while (walk == 1)
        {
            //muda a velocidade para 1, vira a sprite para a direção em que está andando e o lado que o ataque irá
            spd = 1;
            transform.localScale = new Vector3(-1, 1, 1);
            yield return new WaitForSeconds(3);
            //muda a velocidade para -1, vira a sprite para a direção em que está andando e o lado que o ataque irá
            spd = -1;
            transform.localScale = new Vector3(1, 1, 1);
            yield return new WaitForSeconds(3);
        }
        yield return null;
    }

    //padrão de ataque
    IEnumerator Attack()
    {
        //Para de andar e começa o padrão de ataque
        while(sr.isVisible)
        {
            spd = 0;
            anima.Play("Dark Follower_Attack");
            Instantiate(fireball, new Vector2(transform.position.x + 0.1f, transform.position.y + 0.5f), Quaternion.Euler(Vector3.up * side));
            yield return new WaitForSeconds(1.5f);
        }
        anima.Play("DarkFollower");
        isAttack = false;
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (transform.localScale.x == 1)
            Gizmos.DrawWireCube(new Vector3(transform.position.x - 3f, transform.position.y + 0.2f), new Vector3(6, 0.1f));
        else
            Gizmos.DrawWireCube(new Vector3(transform.position.x + 3f, transform.position.y + 0.2f), new Vector3(6, 0.1f));
    }

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Attack")
        {
            life -= col.GetComponent<AttackAtribute>().GetDamage();
            if (life > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    sr.color = Color.red;
                    yield return new WaitForSeconds(0.05f);
                    sr.color = Color.white;
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        yield return null;
    }
}
