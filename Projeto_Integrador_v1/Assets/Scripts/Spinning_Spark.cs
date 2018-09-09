using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning_Spark : MonoBehaviour {

    float spd = 0;
    Animator anima;

	// Use this for initialization
	void Start () {
        anima = GetComponent<Animator>();
        StartCoroutine("Attack");
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(transform.forward * spd * Time.deltaTime);
    }

    IEnumerator Attack()
    {
        anima.Play("Charging");
        yield return new WaitForSeconds(1f);
        anima.Play("Spinning_Spark");
        spd = 80;
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
        yield return null;
    }
}
