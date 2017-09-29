using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemy_mover : MonoBehaviour {
    public int score;
    public float maxspeed;
    public float minspeed;
    float speed;
    bool zigzag = false;
    bool zigzagUp = true;
    float timeZigzag = 0f;
    float timeZigzagMax = 0.5f;
    public float health;
    bool dead = false;
    float startY;
    public GameObject explosion;
    private Animator explosionAnimator;
    public Animator miniexplosionAnimator;

    public GameObject powerUp1;
    public GameObject powerUp2;
    public GameObject powerUp3;
    public GameObject powerUp4;



    void Start() {
        reset();

    }
    public void dealDamage(float damage, Vector3 pos) {
        if (!dead) {
            if (gameObject.transform.position.x < 24.5) {
                health -= damage;
                if (gameObject.GetComponent<miniExplosionController>() != null) {
                    StartCoroutine(gameObject.GetComponent<miniExplosionController>().launchAnimationDamage(pos));
                }
                if (health <= 0) {
                    health = 0;

                    die();
                }
            }

            
        }

    }

    public void reset() {
        speed = Random.Range(minspeed, maxspeed);

        startY = gameObject.transform.position.y;
        if (Random.Range(0, 100) > 50) {
            zigzag = true;
        }
        explosionAnimator = explosion.GetComponent<Animator>();
        explosionAnimator.enabled = false;
        explosion.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void die() {
        explosion.GetComponent<SpriteRenderer>().enabled = true;
        explosionAnimator.enabled = true;
        dead = true;
        GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>().addScore(score);
        int rng = Random.Range(0, 100);
        if(rng<5) {
            Instantiate(powerUp1, gameObject.transform.position, Quaternion.identity);
        } else if (rng<10) {
            Instantiate(powerUp2, gameObject.transform.position, Quaternion.identity);
        } else if (rng < 15) {
            Instantiate(powerUp3, gameObject.transform.position, Quaternion.identity);
        } else if (rng < 20) {
            Instantiate(powerUp4, gameObject.transform.position, Quaternion.identity);
        }
    }

    public bool isDead() {
        return dead;
    }

    public void setZigzag(bool newBool) {
        zigzag = newBool;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!dead) {
            Vector3 movement = Vector3.left * speed * Time.deltaTime;

            if (zigzag) {
                if (zigzagUp) {
                    timeZigzag += Time.deltaTime;
                    if (timeZigzag > timeZigzagMax) {
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
        } else {
            if (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) {
                gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("EnnemyFactory").GetComponent<ennemyFactory>().addNonUsedEnnemy(gameObject);
                //GameObject.Destroy(gameObject);
                
            } else if (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f) {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    } 
}
