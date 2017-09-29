using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up_life : MonoBehaviour {
    public int score;
    public int life;



    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (!other.gameObject.GetComponent<player_mover>().isDead()) {
                other.gameObject.GetComponent<player_mover>().increaseLife(life);
                GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>().addScore(score);
                Destroy(gameObject);
            }
        }
    }

}
