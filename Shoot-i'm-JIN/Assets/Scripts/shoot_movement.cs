using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_movement : MonoBehaviour {
	// Update is called once per frame
    public int speed = 1;
	void Update () {
        gameObject.transform.position = gameObject.transform.position + Vector3.right*Time.deltaTime*speed;

    }
}
