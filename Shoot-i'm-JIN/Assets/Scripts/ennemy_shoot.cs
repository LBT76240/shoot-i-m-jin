using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemy_shoot : MonoBehaviour {
    public GameObject shoot;
    public GameObject shotspawn;
    public float waitFirst;

    ennemy_mover ennemy_mover;

    public float waitBetweenMin;
    public float waitBetweenMax;

    float waitBetween;

	// Use this for initialization
	void Start () {
        waitBetween = Random.Range(waitBetweenMin, waitBetweenMax);
        StartCoroutine(spawnShoot());
        ennemy_mover = gameObject.GetComponent<ennemy_mover>();
    }

    IEnumerator spawnShoot() {
        yield return new WaitForSeconds(waitFirst);
        while (true) {
            if (!ennemy_mover.isDead()) {
                Instantiate(shoot, shotspawn.transform.position, Quaternion.identity);
                
            }
            yield return new WaitForSeconds(waitBetween);
        }

    }


}
