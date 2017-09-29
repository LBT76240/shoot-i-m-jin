using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss1_ia : MonoBehaviour {
    
    public bool cheat;
    public float speed;
    public float zoneWait1X;
    public float zoneWait2Y;
    public float zoneWait3Y;
    public float waitShootPhase1;
    public float waitShootPhase2;
    public float waitShootPhase3;
    public int scorePhase1;
    public int scorePhase2;
    public int scorePhase3;
    int waitShootPhase3Count;
    public GameObject explosion;
    public Slider slider;
    public Image sliderFillImage;
    
    private Animator explosionAnimator;

    public GameObject shotspawn1;
    public GameObject shotspawn2;
    public GameObject shotspawn3;
    public GameObject shotspawn4;
    public GameObject shotspawn5;
    public GameObject shoot;
    public GameObject ennemy;
    //private gameManager gameManager;

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

    public GameObject powerUp1;
    public GameObject powerUp2;
    public GameObject powerUp3;
    public GameObject powerUp4;
    bool powerUpPoped;

    bulletFactory bulletFactory;
    ennemyFactory ennemyFactory;

    //public TextAsset textAsset;


    public void InitWithConf(configClass newconfigClass) {
        speed = newconfigClass.speed;
        zoneWait1X = newconfigClass.zoneWait1X;
        zoneWait2Y = newconfigClass.zoneWait2Y;
        zoneWait3Y = newconfigClass.zoneWait3Y;
        waitShootPhase1 = newconfigClass.waitShootPhase1;
        waitShootPhase2 = newconfigClass.waitShootPhase2;
        waitShootPhase3 = newconfigClass.waitShootPhase3;
        scorePhase1 = newconfigClass.scorePhase1;
        scorePhase2 = newconfigClass.scorePhase2;
        scorePhase3 = newconfigClass.scorePhase3;
        healthPhase1 = newconfigClass.healthPhase1;
        healthPhase2 = newconfigClass.healthPhase2;
        healthPhase3 = newconfigClass.healthPhase3;
        Debug.Log("Fichier de conf chargé");
    }

    // Use this for initialization
    void Start () {
        

        if (cheat) {
            speed = 100;
            healthPhase1 = 1;
            healthPhase2 = 1;
            healthPhase3 = 1;

        }
        waitShootPhase3Count = 0;
        shieldColor =shield.GetComponent<SpriteRenderer>().color;
        explosionAnimator=explosion.GetComponent<Animator>();
        explosionAnimator.enabled = false;
        explosion.GetComponent<SpriteRenderer>().enabled=false;
        //gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>();
        slider.maxValue = healthPhase1 + healthPhase2 + healthPhase3;
        powerUpPoped = false;
        bulletFactory = GameObject.FindGameObjectWithTag("BulletFactory").GetComponent<bulletFactory>();
        ennemyFactory = GameObject.FindGameObjectWithTag("EnnemyFactory").GetComponent<ennemyFactory>();
    }
	
    void updateSlider () {
        slider.value = healthPhase1 + healthPhase2 + healthPhase3;
        Color color = sliderFillImage.color;
        color.r = (healthPhase1 + healthPhase2 + healthPhase3) / slider.maxValue;
        color.g = (1 - (healthPhase1 + healthPhase2 + healthPhase3) / slider.maxValue);
        sliderFillImage.color = color;
        
    }

    public void dealDamage(float damage,Vector3 pos) {
        if (!invulnerability) {
            if (!phase1ended) {
                if (healthPhase1 > 0) {
                    healthPhase1 -= damage;
                    if(gameObject.GetComponent< miniExplosionController > ()!=null) {
                        StartCoroutine(gameObject.GetComponent<miniExplosionController>().launchAnimationDamage(pos));
                    }
                    
                    
                    if (healthPhase1 <= 0) {
                        healthPhase1 = 0;
                        switchPhase();
                    }

                    updateSlider();

                }
            } else if (!phase2ended) {
                if (healthPhase2 > 0) {
                    healthPhase2 -= damage;
                    if (gameObject.GetComponent<miniExplosionController>() != null) {
                        StartCoroutine(gameObject.GetComponent<miniExplosionController>().launchAnimationDamage(pos));
                    }
                    if (healthPhase2 <= 0) {
                        healthPhase2 = 0;
                        switchPhase();
                    }
                    updateSlider();
                }
            } else if (!phase3ended) {
                if (healthPhase3 > 0) {
                    healthPhase3 -= damage;
                    if (gameObject.GetComponent<miniExplosionController>() != null) {
                        StartCoroutine(gameObject.GetComponent<miniExplosionController>().launchAnimationDamage(pos));
                    }
                    if (healthPhase3 <= 0) {
                        healthPhase3 = 0;
                        switchPhase();
                        
                    }
                    updateSlider();
                }
            }
        }
    }

    

    public void switchPhase() {
        if(!phase1ended) {
            phase1ended = true;
            phase2ended = false;
            phase3ended = false;
            invulnerability = true;
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>().addScore(scorePhase1);
        } else if (!phase2ended) {
            phase1ended = true;
            phase2ended = true;
            phase3ended = false;
            invulnerability = true;
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>().addScore(scorePhase2);
        } else if (!phase3ended) {
            phase1ended = true;
            phase2ended = true;
            phase3ended = true;
            invulnerability = true;
            explosion.GetComponent<SpriteRenderer>().enabled = true;
            explosionAnimator.enabled = true;
            sliderFillImage.enabled = false;
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>().addScore(scorePhase3);


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
                            bulletFactory.createBullet(BulletType.boss, shotspawn2.transform.position);
                            bulletFactory.createBullet(BulletType.boss, shotspawn3.transform.position);
                            
                           
                        }
                        if (angle >= 72 && angle < 140) {
                            bulletFactory.createBullet(BulletType.boss, shotspawn3.transform.position);
                            bulletFactory.createBullet(BulletType.boss, shotspawn4.transform.position);

                            
                        }
                        if (angle >= 140) {

                            bulletFactory.createBullet(BulletType.boss, shotspawn4.transform.position);
                            bulletFactory.createBullet(BulletType.boss, shotspawn5.transform.position);

                            
                        }
                    } else if (axe.z < 0) {
                        if (angle >= 143) {

                            bulletFactory.createBullet(BulletType.boss, shotspawn4.transform.position);
                            bulletFactory.createBullet(BulletType.boss, shotspawn5.transform.position);

                            
                        }
                        if (angle < 143 && angle >= 72) {
                            bulletFactory.createBullet(BulletType.boss, shotspawn5.transform.position);
                            bulletFactory.createBullet(BulletType.boss, shotspawn1.transform.position);


                            
                        }
                        if (angle < 72 && angle >= 0) {

                            bulletFactory.createBullet(BulletType.boss, shotspawn1.transform.position);
                            bulletFactory.createBullet(BulletType.boss, shotspawn2.transform.position);

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
                    ennemyFactory.createEnnemy();
                    /*
                    Vector3 pos = gameManager.getSpawnEnnemyPosition();
                    Instantiate(ennemy, pos, Quaternion.identity);*/

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
                    waitShootPhase3Count++;
                    bulletFactory.createBullet(BulletType.boss, shotspawn1.transform.position);
                    bulletFactory.createBullet(BulletType.boss, shotspawn2.transform.position);
                    bulletFactory.createBullet(BulletType.boss, shotspawn3.transform.position);
                    bulletFactory.createBullet(BulletType.boss, shotspawn4.transform.position);
                    bulletFactory.createBullet(BulletType.boss, shotspawn5.transform.position);
                   
                    timerShoot = 0f;
                    if(waitShootPhase3Count>=5) {
                        ennemyFactory.createEnnemy();
                        /*
                        Vector3 pos = gameManager.getSpawnEnnemyPosition();
                        Instantiate(ennemy, pos, Quaternion.identity);*/
                        waitShootPhase3Count = 0;
                    }
                }
                


            }
            


        } else {


            if (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) {
                GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>().endGame(true);
                gameObject.GetComponent<destroy>().destroyObject();
            } else if(explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f) {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                if (!powerUpPoped) {
                    Instantiate(powerUp1, gameObject.transform.position + Vector3.up, Quaternion.identity);
                    Instantiate(powerUp2, gameObject.transform.position + Vector3.down, Quaternion.identity);
                    Instantiate(powerUp3, gameObject.transform.position + Vector3.left, Quaternion.identity);
                    Instantiate(powerUp4, gameObject.transform.position + Vector3.right, Quaternion.identity);
                    powerUpPoped = true;
                }
            }
        }
    }
}
