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
    float moveHorizontal = 0f;
    float moveVertical = 0f;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    public void setMovement(float mHorizontal, float mVertical) {
        print("lol");
        print(mHorizontal);
        print(mVertical);
        moveHorizontal = mHorizontal;
        moveVertical = mVertical;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        gameObject.transform.position = rb.position + movement * speed * Time.deltaTime;
        gameObject.transform.position = new Vector2(
            Mathf.Clamp(rb.position.x, boundary.xmin, boundary.xmax),
            Mathf.Clamp(rb.position.y, boundary.ymin, boundary.ymax)
        );
    }
}
