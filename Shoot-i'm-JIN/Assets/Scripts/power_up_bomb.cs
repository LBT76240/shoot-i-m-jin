using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up_bomb : MonoBehaviour {
    public int score;
    public float damage;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (!other.gameObject.GetComponent<player_mover>().isDead()) {
                GameObject[] listOfEnnemy = GameObject.FindGameObjectsWithTag("Ennemy");
                
                for (int i =0;i< listOfEnnemy.Length;i++) {
                    if (!listOfEnnemy[i].GetComponent<ennemy_mover>().isDead()) {
                        listOfEnnemy[i].gameObject.GetComponent<ennemy_mover>().dealDamage(damage, gameObject.transform.position);
                    }
                }
                GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>().addScore(score);
                Destroy(gameObject);
            }
        }
    }
}
