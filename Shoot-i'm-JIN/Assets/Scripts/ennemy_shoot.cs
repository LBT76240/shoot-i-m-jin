using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemy_shoot : MonoBehaviour {
    public GameObject shoot;
    public GameObject shotspawn;
    public float waitFirst;

    bulletFactory bulletFactory;

    ennemy_mover ennemy_mover;

    public float waitBetweenMin;
    public float waitBetweenMax;

    float waitBetween;

	// Use this for initialization
	void Start () {
        reset();

    }

    public void reset() {
        waitBetween = Random.Range(waitBetweenMin, waitBetweenMax);
        StartCoroutine(spawnShoot());
        ennemy_mover = gameObject.GetComponent<ennemy_mover>();
        bulletFactory = GameObject.FindGameObjectWithTag("BulletFactory").GetComponent<bulletFactory>();
    }

    IEnumerator spawnShoot() {
        yield return new WaitForSeconds(waitFirst);
        while (true) {
            if (!ennemy_mover.isDead()) {

                
                bulletFactory.createBullet(BulletType.ennemy, shotspawn.transform.position);
            }
            yield return new WaitForSeconds(waitBetween);
        }

    }


}
