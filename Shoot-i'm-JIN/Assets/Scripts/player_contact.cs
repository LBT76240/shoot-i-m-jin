using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_contact : MonoBehaviour {

    public float damageCollision;

    void OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag("Ennemy")) {
            if (!other.gameObject.GetComponent<ennemy_mover>().isDead() && !gameObject.GetComponent<player_mover>().isDead()) {
                gameObject.GetComponent<player_mover>().dealDamage(gameObject.transform.position);
                other.gameObject.GetComponent<ennemy_mover>().dealDamage(damageCollision, gameObject.transform.position);
            }
        }

        
    }
}
