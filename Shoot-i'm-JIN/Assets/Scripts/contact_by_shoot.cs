using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contact_by_shoot : MonoBehaviour {
    public BulletType bulletType;
    public float damage;
    bulletFactory bulletFactory;

    public void Start() {
        bulletFactory = GameObject.FindGameObjectWithTag("BulletFactory").GetComponent<bulletFactory>();
    }

    public void recycleBullet() {
        bulletFactory.addNonUsedBullet(bulletType, gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (gameObject.activeSelf) {
            if (gameObject.CompareTag("ennemy_shoot")) {
                if (other.gameObject.CompareTag("Player")) {
                    if (!other.gameObject.GetComponent<player_mover>().isDead()) {
                        other.gameObject.GetComponent<player_mover>().dealDamage(gameObject.transform.position);

                        gameObject.SetActive(false);
                        

                        recycleBullet();

                        //Destroy(gameObject);
                    }
                }
            }

            if (gameObject.CompareTag("player_shoot")) {
                if (other.gameObject.CompareTag("Ennemy")) {
                    if (!other.gameObject.GetComponent<ennemy_mover>().isDead()) {
                        other.gameObject.GetComponent<ennemy_mover>().dealDamage(damage, gameObject.transform.position);
                        
                        gameObject.SetActive(false);
                        
                        recycleBullet();
                        //Destroy(gameObject);
                    }
                }
                if (other.gameObject.CompareTag("boss1")) {
                    other.gameObject.GetComponent<boss1_ia>().dealDamage(damage, gameObject.transform.position);

                    gameObject.SetActive(false);
                    
                    recycleBullet();
                    //Destroy(gameObject);
                }
            }
        }

    }
}
