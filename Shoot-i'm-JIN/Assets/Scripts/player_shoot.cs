using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_shoot : MonoBehaviour {
    public GameObject shot;
    public GameObject shotspawn;
    public float delayShoot;

    float timeSinceLastShoot = 0f;

    // Update is called once per frame
    void Update() {
        timeSinceLastShoot += Time.deltaTime;




        if (Input.GetButton("Fire1")) {
            if (timeSinceLastShoot > delayShoot) {
                Instantiate(shot, shotspawn.transform.position, shotspawn.transform.rotation);
                timeSinceLastShoot = 0;
            }
        }
    }
}
