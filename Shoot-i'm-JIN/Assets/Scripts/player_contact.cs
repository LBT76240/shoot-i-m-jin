using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_contact : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        print("Test");
        if(other.CompareTag("Ennemy")) {
            gameObject.GetComponent<player_mover>().dealDamage(gameObject.transform.position);
            other.gameObject.GetComponent<ennemy_mover>().dealDamage(gameObject.transform.position);
        }

        
    }
}
