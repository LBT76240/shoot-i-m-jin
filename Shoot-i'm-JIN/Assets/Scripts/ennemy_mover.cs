using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemy_mover : MonoBehaviour {

    public float speed;
    bool zigzag = false;
    bool zigzagUp = true;
    float timeZigzag = 0f;
    float timeZigzagMax = 0.5f;
    bool dead = false;
    float startY;
    public GameObject explosion;
    private Animator explosionAnimator;


    void Start() {
        startY = gameObject.transform.position.y;
        if(Random.Range(0, 100) > 50) {
            zigzag = true;
        }
        explosionAnimator = explosion.GetComponent<Animator>();
        explosionAnimator.enabled = false;
        explosion.GetComponent<SpriteRenderer>().enabled = false;

    }
    public void dealDamage(float damage, Vector3 pos) {
        if (!dead) {
            explosion.GetComponent<SpriteRenderer>().enabled = true;
            explosionAnimator.enabled = true;
            dead = true;
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
                GameObject.Destroy(gameObject);
            } else if (explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f) {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    } 
}
