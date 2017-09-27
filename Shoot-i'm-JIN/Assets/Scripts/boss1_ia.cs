using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1_ia : MonoBehaviour {
    public float speed;
    public float zoneWait1X;
    public float zoneWait2Y;
    public float zoneWait3Y;
    public float waitShootPhase1;
    public float waitShootPhase2;
    public float waitShootPhase3;
    public GameObject explosion;
    public Animator miniexplosionAnimator;
    private Animator explosionAnimator;

    public GameObject shotspawn1;
    public GameObject shotspawn2;
    public GameObject shotspawn3;
    public GameObject shotspawn4;
    public GameObject shotspawn5;
    public GameObject shoot;
    public GameObject ennemy;
    private gameManager gameManager;

    bool phase1ended = false;
    bool prephase2ended = false;
    bool phase2ended = false;
    bool prephase3ended = false;
    bool phase3ended = false;
    bool invulnerability = true;
    public float healthPhase1;
    public float healthPhase2;
    public float healthPhase3;
    public GameObject shield;
    Color shieldColor;

    float timerShoot = 0f;

    // Use this for initialization
    void Start () {
        shieldColor=shield.GetComponent<SpriteRenderer>().color;
        explosionAnimator=explosion.GetComponent<Animator>();
        explosionAnimator.enabled = false;
        explosion.GetComponent<SpriteRenderer>().enabled=false;
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>();

    }
	
    public void dealDamage(float damage,Vector3 pos) {
        if (!invulnerability) {
            if (!phase1ended) {
                if (healthPhase1 > 0) {
                    healthPhase1 -= damage;
                    StartCoroutine(launchAnimationDamage(pos));
                    if (healthPhase1 <= 0) {
                        healthPhase1 = 0;
                        switchPhase();
                    }
                }
            } else if (!phase2ended) {
                if (healthPhase2 > 0) {
                    healthPhase2 -= damage;
                    StartCoroutine(launchAnimationDamage(pos));
                    if (healthPhase2 <= 0) {
                        healthPhase2 = 0;
                        switchPhase();
                    }
                }
            } else if (!phase3ended) {
                if (healthPhase3 > 0) {
                    healthPhase3 -= damage;
                    StartCoroutine(launchAnimationDamage(pos));
                    if (healthPhase3 <= 0) {
                        healthPhase3 = 0;
                        switchPhase();
                    }
                }
            }
        }
    }

    IEnumerator launchAnimationDamage(Vector3 pos) {

        Animator animator = Instantiate(miniexplosionAnimator, pos, Quaternion.identity).GetComponent<Animator>();
        animator.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        
        Vector3 newScale = animator.gameObject.transform.localScale;

        newScale.x = 0.2f;
        newScale.y = 0.2f;

        animator.gameObject.transform.localScale = newScale;
        animator.enabled = true;
        
        while(animator.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.8f) {
            yield return new WaitForSeconds(0.1f);
            
        }
        
        GameObject.Destroy(animator.gameObject);
    }

    public void switchPhase() {
        if(!phase1ended) {
            phase1ended = true;
            phase2ended = false;
            phase3ended = false;
            invulnerability = true;
        } else if (!phase2ended) {
            phase1ended = true;
            phase2ended = true;
            phase3ended = false;
            invulnerability = true;
        } else if (!phase3ended) {
            phase1ended = true;
            phase2ended = true;
            phase3ended = true;
            invulnerability = true;
            explosion.GetComponent<SpriteRenderer>().enabled = true;
            explosionAnimator.enabled = true;
            
            
        }
    }

	// Update is called once per frame
	void FixedUpdate() {
        if (!phase1ended) {
            if (gameObject.transform.position.x > zoneWait1X) {
                Vector3 movement = Vector3.left * speed * Time.deltaTime;
                gameObject.transform.position = gameObject.transform.position + movement;
                if (gameObject.transform.position.x < zoneWait1X) {
                    Vector3 pos = gameObject.transform.position;
                    pos.x = zoneWait1X;
                    gameObject.transform.position = pos;
                    
                }
            } else if (shieldColor.a > 0) {
                shieldColor.a -= Time.deltaTime;
                if (shieldColor.a <= 0) {
                    shieldColor.a = 0;
                    invulnerability = false;
                    timerShoot = 0;
                }
                shield.GetComponent<SpriteRenderer>().color = shieldColor;
            } else {
                timerShoot += Time.deltaTime;
                if (timerShoot> waitShootPhase1) {
                    float angle;
                    Vector3 axe;
                    gameObject.transform.rotation.ToAngleAxis(out angle, out axe);
                    
                    if (axe.z > 0) {
                        if (angle >= 0 && angle < 72) {
                            
                            Instantiate(shoot, shotspawn2.transform.position, Quaternion.identity);
                            Instantiate(shoot, shotspawn3.transform.position, Quaternion.identity);
                        }
                        if (angle >= 72 && angle < 140) {
                            
                            Instantiate(shoot, shotspawn3.transform.position, Quaternion.identity);
                            Instantiate(shoot, shotspawn4.transform.position, Quaternion.identity);
                        }
                        if (angle >= 140) {
                            
                            Instantiate(shoot, shotspawn4.transform.position, Quaternion.identity);
                            Instantiate(shoot, shotspawn5.transform.position, Quaternion.identity);
                        }
                    } else if (axe.z < 0) {
                        if (angle >= 143) {
                            
                            Instantiate(shoot, shotspawn4.transform.position, Quaternion.identity);
                            Instantiate(shoot, shotspawn5.transform.position, Quaternion.identity);
                        }
                        if (angle < 143 && angle >= 72) {
                            
                            Instantiate(shoot, shotspawn5.transform.position, Quaternion.identity);
                            Instantiate(shoot, shotspawn1.transform.position, Quaternion.identity);
                        }
                        if (angle < 72 && angle >= 0) {
                            
                            Instantiate(shoot, shotspawn1.transform.position, Quaternion.identity);
                            Instantiate(shoot, shotspawn2.transform.position, Quaternion.identity);
                        }
                    } 

                    timerShoot = 0f;
                }

                
            }

        } else if(!phase2ended) {
            if (!prephase2ended) {
                if (shieldColor.a < 1) {
                    shieldColor.a += Time.deltaTime;
                    if (shieldColor.a > 1) {
                        shieldColor.a = 1;
                        prephase2ended = true;
                    }
                    shield.GetComponent<SpriteRenderer>().color = shieldColor;
                } else {
                    prephase2ended = true;
                }
            } else if (gameObject.transform.position.y < zoneWait2Y) {
                Vector3 movement = Vector3.up * speed * Time.deltaTime;
                gameObject.transform.position = gameObject.transform.position + movement;
                if (gameObject.transform.position.y > zoneWait2Y) {
                    Vector3 pos = gameObject.transform.position;
                    pos.y = zoneWait2Y;
                    gameObject.transform.position = pos;
                }
            } else if (shieldColor.a > 0) {
                shieldColor.a -= Time.deltaTime;
                if (shieldColor.a <= 0) {
                    shieldColor.a = 0;
                    invulnerability = false;
                    timerShoot = 0;
                }
                shield.GetComponent<SpriteRenderer>().color = shieldColor;
            } else {
                timerShoot += Time.deltaTime;
                if (timerShoot > waitShootPhase2) {

                    Vector3 pos = gameManager.getSpawnEnnemyPosition();
                    Instantiate(ennemy, pos, Quaternion.identity);

                    timerShoot = 0f;
                }
                
            }

        } else if (!phase3ended) {
            if (!prephase3ended) {
                if (shieldColor.a < 1) {
                    shieldColor.a += Time.deltaTime;
                    if (shieldColor.a > 1) {
                        shieldColor.a = 1;
                        prephase3ended = true;
                    }
                    shield.GetComponent<SpriteRenderer>().color = shieldColor;
                } else {
                    prephase3ended = true;
                }
            } else if (gameObject.transform.position.y > zoneWait3Y) {
                Vector3 movement = Vector3.down * speed * Time.deltaTime;
                gameObject.transform.position = gameObject.transform.position + movement;
                if (gameObject.transform.position.y < zoneWait3Y) {
                    Vector3 pos = gameObject.transform.position;
                    pos.y = zoneWait3Y;
                    gameObject.transform.position = pos;
                }
            } else if (shieldColor.a > 0) {
                shieldColor.a -= Time.deltaTime;
                if (shieldColor.a <= 0) {
                    shieldColor.a = 0;
                    invulnerability = false;
                    timerShoot = 0;
                }
                shield.GetComponent<SpriteRenderer>().color = shieldColor;
            } else {
                timerShoot += Time.deltaTime;
                if (timerShoot > waitShootPhase3) {

                    Instantiate(shoot, shotspawn1.transform.position, Quaternion.identity);
                    Instantiate(shoot, shotspawn2.transform.position, Quaternion.identity);
                    Instantiate(shoot, shotspawn3.transform.position, Quaternion.identity);
                    Instantiate(shoot, shotspawn4.transform.position, Quaternion.identity);
                    Instantiate(shoot, shotspawn5.transform.position, Quaternion.identity);

                    timerShoot = 0f;
                }
                
                
            }
            


        } else {


            if (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) {
                gameObject.GetComponent<destroy>().destroyObject();
            } else if(explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f) {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
