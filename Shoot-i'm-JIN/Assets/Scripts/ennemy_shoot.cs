using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemy_shoot : MonoBehaviour {
    public GameObject shoot;
    public GameObject shotspawn;
    public float waitFirst;

    public float waitBetween;

	// Use this for initialization
	void Start () {
        StartCoroutine(spawnShoot());
    }

    IEnumerator spawnShoot() {
        yield return new WaitForSeconds(waitFirst);
        while (true) {

            Instantiate(shoot, shotspawn.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waitBetween);
        }

    }


}
