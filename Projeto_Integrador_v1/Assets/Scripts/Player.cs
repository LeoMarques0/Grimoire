using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Rigidbody2D rbPlayer;
    public Animator anima;
    public GameObject thunderSword;
    Animator lightningSword;
    public float spd, minHeight, maxHeight, initialHeight;
    public GameObject dust;
    public GameObject fireball;
    public LayerMask mask;
    public AudioClip[] sounds;
    public int vida = 20;
    bool isGrounded = true, isDamaged = false;
    AudioSource audiosource;
    public Image life;


	// Use this for initialization
	void Start () {
        anima = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        lightningSword = thunderSword.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        rbPlayer.gravityScale = 1;
        isGrounded = Physics2D.OverlapBox(transform.position, new Vector2(0.2f, 0.02f), 0, mask);
        anima.SetBool("isGrounded", isGrounded);

        //Verifica se o personagem está recebendo dano
        if (isDamaged)
        {
            anima.SetFloat("hor", 0);
            anima.SetFloat("ver", 0);
        }
        else
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                IsRunning();
            }
            else
                anima.SetFloat("hor", 0);

            if (Input.GetKeyDown(KeyCode.Mouse0) && Fireball.fireballAmount < 3)
                IsFireball();
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                audiosource.clip = sounds[0];
                audiosource.Play();
                if (anima.GetFloat("hor") != 0 && isGrounded == true)
                {
                    thunderSword.SetActive(true);
                    lightningSword.SetBool("isLightning", true);
                }
                else
                    thunderSword.SetActive(false);
                anima.SetBool("isLightning", true);
                StopCoroutine("IsLightning");
                StartCoroutine("IsLightning");
                if (anima.GetBool("isLightning"))
                    audiosource.Play();
            }
            else if ((Input.GetKeyDown(KeyCode.Space) && isGrounded == true) || isGrounded == false)
                IsJumping();
                
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EvilAttack" || col.tag == "Boss" || col.tag == "Enemy")  && isDamaged == false)
        {
            rbPlayer.velocity = new Vector2(0, 0);
            if (transform.localScale.x == 2)
                rbPlayer.velocity = -transform.right * 2;
            else if (transform.localScale.x == -2)
                rbPlayer.velocity = transform.right * 2;
            anima.Play("Damage");
            isDamaged = true;
            yield return new WaitForSeconds(0.3f);
            isDamaged = false;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(0.2f, 0.02f));
    }

    private void IsRunning()
    {
        if (anima.GetBool("isLightning") && isGrounded == true && !anima.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            transform.Translate(transform.right * 0 * Time.deltaTime);
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(transform.right * spd * Time.deltaTime);
                transform.localScale = new Vector3(2, 2, 2);
                anima.SetFloat("hor", 1);
            }

            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(transform.right * spd * -1 * Time.deltaTime);
                transform.localScale = new Vector3(-2, 2, 2);
                anima.SetFloat("hor", 1);
            }
        }
    }

    private void IsJumping()
    {
        float ver = rbPlayer.velocity.y;
        if (isGrounded == true)
        {
            initialHeight = transform.position.y;
            rbPlayer.velocity = (transform.up * 10);
        }
        if (transform.position.y >= initialHeight + maxHeight)
        {
            rbPlayer.velocity = (rbPlayer.velocity / 3);
            initialHeight = transform.position.y;
        }
        else if (rbPlayer.velocity.y < 0)
        {
            rbPlayer.gravityScale = 5;
        }
        if (isGrounded == false && Input.GetKeyDown(KeyCode.DownArrow))
            rbPlayer.velocity = (transform.up * -10);
        anima.SetFloat("ver", ver);
    }

    private void IsFireball()
    {
        if(isGrounded == false)
        {
            if (anima.GetFloat("ver") > 0)
                anima.Play("FireballAir");
            else
                anima.Play("FireballFall");
        }
        else if (anima.GetFloat("hor") != 0)
        {
            anima.Play("FireballRun", 0, anima.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
        else
            anima.Play("Fireball");
        if (transform.localScale.x == -2)
            Instantiate(fireball, new Vector3(-0.3f, 0.6f) + transform.position, Quaternion.Euler(Vector3.up * 180));
        else
            Instantiate(fireball, new Vector3(0.3f, 0.6f) + transform.position, Quaternion.identity);
    }

    IEnumerator IsLightning()
    {
        yield return new WaitForSeconds(0.3f);
        anima.SetBool("isLightning", false);
        thunderSword.SetActive(false);
        yield return null;
    }

    public void StopLightning()
    {
        anima.SetBool("isLightning", false);
    }
}
