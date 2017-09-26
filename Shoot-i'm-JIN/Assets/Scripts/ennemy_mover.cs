using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemy_mover : MonoBehaviour {

    public float speed;
    bool zigzag = false;
    bool zigzagUp = true;
    float timeZigzag = 0f;
    float timeZigzagMax = 0.5f;

    float startY;

    

    void Start() {
        startY = gameObject.transform.position.y;
    }

    public 

    // Update is called once per frame
    void FixedUpdate () {
        
        Vector3 movement = Vector3.left * speed * Time.deltaTime;

        if (zigzag) {
            if (zigzagUp) {
                timeZigzag += Time.deltaTime;
                if(timeZigzag> timeZigzagMax) {
                    zigzagUp = false;
                }
                
            } else {
                timeZigzag -= Time.deltaTime;
                if (timeZigzag < -timeZigzagMax) {
                    zigzagUp = true;
                }
            }

            Vector3 pos = gameObject.transform.position;
            pos.y = startY + timeZigzag * 2;
            gameObject.transform.position = pos;

        }
        gameObject.transform.position = gameObject.transform.position + movement;
    }
}
