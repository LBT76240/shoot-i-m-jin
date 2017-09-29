using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up_energy : MonoBehaviour {
    public int score;
    public float energy;



    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (!other.gameObject.GetComponent<player_mover>().isDead()) {
                other.gameObject.GetComponent<player_shoot>().increaseEnergy(energy);
                GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>().addScore(score);
                Destroy(gameObject);
            }
        }
    }

}
