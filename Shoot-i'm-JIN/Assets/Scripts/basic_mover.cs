using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_mover : MonoBehaviour {

    public float speedX;
    public float speedY;

	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 movement = new Vector3 (speedX, speedY) * Time.deltaTime;
        gameObject.transform.position = gameObject.transform.position + movement;
    }
}
