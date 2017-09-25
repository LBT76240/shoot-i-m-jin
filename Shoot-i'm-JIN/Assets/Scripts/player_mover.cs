using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_mover : MonoBehaviour {
    public float speed;

    
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        gameObject.transform.position = gameObject.transform.position + movement * Time.deltaTime* speed;
    }
}
