using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contact_by_shoot : MonoBehaviour {

    public float damage;

    void OnTriggerEnter2D(Collider2D other) {
        if (gameObject.CompareTag("ennemy_shoot")) {
            if (other.gameObject.CompareTag("Player")) {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        if (gameObject.CompareTag("player_shoot")) {
            if (other.gameObject.CompareTag("Ennemy")) {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

    }
}
