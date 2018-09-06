using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilThunder : MonoBehaviour {

    GameObject player;
    Animator animaThunder;
    bool move = true;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Mage");
        animaThunder = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (move == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, 3f), 6 * Time.deltaTime);
        }
        StartCoroutine(Thunder());

	}

    IEnumerator Thunder()
    {
        yield return new WaitForSeconds(2.5f);
        move = false;
        animaThunder.Play("EvilThunder");
        Destroy(gameObject, 0.55f);
        yield return null;
    }
}
