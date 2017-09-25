using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_mover : MonoBehaviour {

    public int speed = 1;
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = gameObject.transform.position + Vector3.right * Time.deltaTime * speed;
        if(gameObject.transform.position.x<-10) {
            //print("reset");
            Vector3 pos = gameObject.transform.position;
            pos.x += 20.47f;
            gameObject.transform.position = pos;
        }
            
    }
}
