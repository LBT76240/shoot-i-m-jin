using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject explosion;
    private Animator explosionAnimator;
    int health;
    bool isInvulnerable;
    float invulnerability;
    float timeBetweenInput;
    float timeBetweenDash = 0f;
    public float timeBetweenDashLimit;
    public float dashValue;
    bool waitDash = false;
    bool playerDead = false;
    Image life1;
    Image life2;
    Image life3;

    LastTap lastTap = LastTap.None;
    void Start() {
        invulnerability = 0;
        isInvulnerable = false;
        health = 3;
        rb = GetComponent<Rigidbody2D>();
        explosionAnimator = explosion.GetComponent<Animator>();
        explosionAnimator.enabled = false;
        explosion.GetComponent<SpriteRenderer>().enabled = false;
        life1 = GameObject.FindWithTag("Life1").GetComponent<Image>();
        life2 = GameObject.FindWithTag("Life2").GetComponent<Image>();
        life3 = GameObject.FindWithTag("Life3").GetComponent<Image>();

        life1.enabled = true;
        life2.enabled = true;
        life3.enabled = true;

    }

    public void dealDamage (float damage, Vector3 pos) {
        if (!playerDead && !isInvulnerable) {
            health -= 1;
            invulnerability = 0;
            isInvulnerable = true;
            if (health < 0) {
                health = 0;
                explosion.GetComponent<SpriteRenderer>().enabled = true;
                explosionAnimator.enabled = true;
                playerDead = true;
            }
            switch(health) {
                case 0:
                    life1.enabled = false;
                    life2.enabled = false;
                    life3.enabled = false;
                    break;
                case 1:
                    life1.enabled = true;
                    life2.enabled = false;
                    life3.enabled = false;
                    break;
                case 2:
                    life1.enabled = true;
                    life2.enabled = true;
                    life3.enabled = false;
                    break;
                case 3:
                    life1.enabled = true;
                    life2.enabled = true;
                    life3.enabled = true;
                    break;
            }
        }

    }

    public bool isDead() {
        return playerDead;
    }

    public void dash(bool tapHorizontalUp, bool tapHorizontalDown, bool tapVerticalUp, bool tapVerticalDown) {
        if (!playerDead) {
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
        if (!playerDead) {
            timeBetweenInput += Time.deltaTime;
            invulnerability += Time.deltaTime;

            if(invulnerability>2f) {
                isInvulnerable = false;
                Color newColor = gameObject.GetComponent<SpriteRenderer>().color;

                newColor.a = 1;
                gameObject.GetComponent<SpriteRenderer>().color = newColor;
            } else {
                if (isInvulnerable) {
                    Color newColor = gameObject.GetComponent<SpriteRenderer>().color;

                    newColor.a = Mathf.Repeat(invulnerability * 5, 1);
                    gameObject.GetComponent<SpriteRenderer>().color = newColor;
                }

            }


            if (waitDash) {
                timeBetweenDash += Time.deltaTime;
                if (timeBetweenDash > timeBetweenDashLimit) {
                    waitDash = false;
                }
            }

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            gameObject.transform.position = rb.position + movement * speed * Time.deltaTime;
            gameObject.transform.position = new Vector2(
                Mathf.Clamp(rb.position.x, boundary.xmin, boundary.xmax),
                Mathf.Clamp(rb.position.y, boundary.ymin, boundary.ymax)
            );
        } else {
            if (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) {
                GameObject.Destroy(gameObject);
            } else if (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f) {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
