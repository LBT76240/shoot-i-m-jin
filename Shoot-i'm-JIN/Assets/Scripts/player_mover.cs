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
    public float timeDashed;
    Vector2 dirDash;
    float timeBetweenDash = 0f;
    public float timeBetweenDashLimit;
    public float dashValue;
    bool waitDash = false;
    bool isInvulnerableDash = false;
    float invulnerabilityDash;
    bool playerDead = false;
    Image life1;
    Image life2;
    Image life3;
    public SpriteRenderer playerShield;
    Image dashImage;
    float widthdashImage;
    


    void Start() {
        invulnerability = 0;
        isInvulnerable = false;
        isInvulnerableDash = false;
        invulnerabilityDash = 0;
        health = 3;
        rb = GetComponent<Rigidbody2D>();
        explosionAnimator = explosion.GetComponent<Animator>();
        explosionAnimator.enabled = false;
        explosion.GetComponent<SpriteRenderer>().enabled = false;
        life1 = GameObject.FindWithTag("Life1").GetComponent<Image>();
        life2 = GameObject.FindWithTag("Life2").GetComponent<Image>();
        life3 = GameObject.FindWithTag("Life3").GetComponent<Image>();
        dashImage = GameObject.FindWithTag("dashImage").GetComponent<Image>();
        RectTransform rectTransform = dashImage.transform as RectTransform;
        
        widthdashImage = rectTransform.sizeDelta.x;
        life1.enabled = true;
        life2.enabled = true;
        life3.enabled = true;

    }

    public void dealDamage (float damage, Vector3 pos) {
        if (!playerDead && !isInvulnerable && !isInvulnerableDash) {
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
    
    public void dash(Vector2 dir) {
        if (!waitDash) {
            if (gameObject.GetComponent<player_shoot>().energyAvailable()) {
                gameObject.GetComponent<player_shoot>().decreaseEnergy(20);
                dirDash = dir;
                waitDash = true;
                timeBetweenDash = 0f;
                isInvulnerableDash = true;
                invulnerabilityDash = 0;
                RectTransform rectTransform = dashImage.transform as RectTransform;
                rectTransform.sizeDelta = new Vector2(0, rectTransform.sizeDelta.y);
   
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

            invulnerabilityDash += Time.deltaTime;

            if (invulnerabilityDash > 1f) {
                isInvulnerableDash = false;

                Color newColor = playerShield.color;

                newColor.a = 0;
                playerShield.color = newColor;
            } else {
                if (isInvulnerableDash) {
                    if (invulnerabilityDash < 0.1f) {
                        Color newColor = playerShield.color;

                        newColor.a = 10* invulnerabilityDash;
                        playerShield.color = newColor;
                    } else if(invulnerabilityDash > 0.9f) {
                        Color newColor = playerShield.color;

                        newColor.a = (10- 10*invulnerabilityDash);
                        playerShield.color = newColor;
                    } else {
                        Color newColor = playerShield.color;

                        newColor.a = 1;
                        playerShield.color = newColor;
                    }
                } 

            }


            if (waitDash) {
                timeBetweenDash += Time.deltaTime;

                

                if (timeBetweenDash < timeDashed) {
                    gameObject.transform.position = rb.position + dirDash * dashValue * Time.deltaTime; 
                }


                if (invulnerabilityDash < 0.1f) {
                    Color newColor = playerShield.color;

                    newColor.a = 10 * invulnerabilityDash;
                    playerShield.color = newColor;
                } else if (invulnerabilityDash > 0.9f) {
                    Color newColor = playerShield.color;

                    newColor.a = (10 - 10 * invulnerabilityDash);
                    playerShield.color = newColor;
                } else {
                    Color newColor = playerShield.color;

                    newColor.a = 1;
                    playerShield.color = newColor;
                }
                RectTransform rectTransform = dashImage.transform as RectTransform;
                rectTransform.sizeDelta = new Vector2((widthdashImage * timeBetweenDash) / timeBetweenDashLimit, rectTransform.sizeDelta.y);

                if (timeBetweenDash > timeBetweenDashLimit) {
                    RectTransform rectTransform2 = dashImage.transform as RectTransform;
                    rectTransform2.sizeDelta = new Vector2(widthdashImage, rectTransform2.sizeDelta.y);
                    Color newColor = playerShield.color;

                    newColor.a = 0;
                    playerShield.color = newColor;
                    waitDash = false;
                } else if (timeBetweenDash > timeBetweenDashLimit - 0.1f) {
                    Color newColor = playerShield.color;

                    newColor.a = 1-(10 * (timeBetweenDashLimit - timeBetweenDash));
                    playerShield.color = newColor;


                } else if (timeBetweenDash > timeBetweenDashLimit - 0.2f) {
                    Color newColor = playerShield.color;

                    newColor.a = 10*(timeBetweenDashLimit-timeBetweenDash -0.1f);
                    playerShield.color = newColor;
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
