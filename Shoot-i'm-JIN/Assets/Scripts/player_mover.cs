using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary {
    public float xmin, xmax, ymin, ymax;
}

public class player_mover : MonoBehaviour {
    public float speed;
    public Boundary boundary;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        rb.velocity = movement * speed;
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xmin, boundary.xmax),
            Mathf.Clamp(rb.position.y, boundary.ymin, boundary.ymax),
            0f
        );
    }
}
