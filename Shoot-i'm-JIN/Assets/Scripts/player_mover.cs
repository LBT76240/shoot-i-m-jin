using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary {
    public float xmin, xmax, ymin, ymax;
}

public enum LastTap { None, HorizontalUp, HorizontalDown, VerticalUp, VerticalDown }

public class player_mover : MonoBehaviour {
    public float speed;
    public Boundary boundary;
    float moveHorizontal = 0f;
    float moveVertical = 0f;
    Rigidbody2D rb;

    float timeBetweenInput;
    float timeBetweenDash = 0f;
    public float timeBetweenDashLimit;
    public float dashValue;
    bool waitDash = false;

    LastTap lastTap = LastTap.None;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    

    public void dash(bool tapHorizontalUp, bool tapHorizontalDown, bool tapVerticalUp, bool tapVerticalDown) {
        if (tapHorizontalUp) {
            if (!waitDash) {
                if (lastTap == LastTap.HorizontalUp) {
                    if (timeBetweenInput < 1f) {
                        gameObject.transform.position = rb.position + Vector2.right * dashValue;
                        waitDash = true;
                        timeBetweenDash = 0f;
                        lastTap = LastTap.None;
                    } else {
                        //print("DashHP Too Slow");
                        timeBetweenInput = 0f;
                    }
                } else {
                    //print("NewDashHP");
                    lastTap = LastTap.HorizontalUp;
                    timeBetweenInput = 0f;
                }
            }
        }

        if (tapHorizontalDown) {
            if (!waitDash) {
                if (lastTap == LastTap.HorizontalDown) {
                    if (timeBetweenInput < 1f) {
                        gameObject.transform.position = rb.position + Vector2.right * -dashValue;
                        waitDash = true;
                        timeBetweenDash = 0f;
                        lastTap = LastTap.None;
                    } else {
                        //print("DashHN Too Slow");
                        timeBetweenInput = 0f;
                    }
                } else {
                    //print("NewDashHN");
                    lastTap = LastTap.HorizontalDown;
                    timeBetweenInput = 0f;
                }
            }
        }

        if (tapVerticalUp) {
            if (!waitDash) {
                if (lastTap == LastTap.VerticalUp) {
                    if (timeBetweenInput < 1f) {
                        gameObject.transform.position = rb.position + Vector2.up * dashValue;
                        waitDash = true;
                        timeBetweenDash = 0f;
                        lastTap = LastTap.None;
                    } else {
                        //print("DashVP Too Slow");
                        timeBetweenInput = 0f;
                    }
                } else {
                    //print("NewDashVP");
                    lastTap = LastTap.VerticalUp;
                    timeBetweenInput = 0f;
                }
            }
        }

        if (tapVerticalDown) {
            if (!waitDash) {
                if (lastTap == LastTap.VerticalDown) {
                    if (timeBetweenInput < 1f) {
                        gameObject.transform.position = rb.position + Vector2.down * dashValue;
                        waitDash = true;
                        timeBetweenDash = 0f;
                        lastTap = LastTap.None;
                    } else {
                        //print("DashVN Too Slow");
                        timeBetweenInput = 0f;
                    }
                } else {
                    //print("NewDashVN");
                    lastTap = LastTap.VerticalDown;
                    timeBetweenInput = 0f;
                }
            }
        }

    }
    public void setMovement(float mHorizontal, float mVertical) {
        moveHorizontal = mHorizontal;
        moveVertical = mVertical;
        if(moveHorizontal==0) {
            if(moveVertical>0) {
                moveVertical = 1;
            }
            if(moveVertical<0) {
                moveVertical = -1;
            }
        } else if (moveVertical == 0) {
            if (moveHorizontal > 0) {
                moveHorizontal = 1;
            }
            if (moveHorizontal < 0) {
                moveHorizontal = -1;
            }
        } else {
            if (moveHorizontal > 0) {
                moveHorizontal = 1/1.41f;
            } else {
                moveHorizontal = -1 / 1.41f;
            }
            if (moveVertical > 0) {
                moveVertical = 1 / 1.41f;
            } else {
                moveVertical = -1 / 1.41f;
            }

        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timeBetweenInput += Time.deltaTime;

        

        if(waitDash) {
            timeBetweenDash += Time.deltaTime;
            if(timeBetweenDash> timeBetweenDashLimit) {
                waitDash = false;
            }
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        gameObject.transform.position = rb.position + movement * speed * Time.deltaTime;
        gameObject.transform.position = new Vector2(
            Mathf.Clamp(rb.position.x, boundary.xmin, boundary.xmax),
            Mathf.Clamp(rb.position.y, boundary.ymin, boundary.ymax)
        );
    }
}
