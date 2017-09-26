using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public gameManager gameManager;

    GameObject player = null;


	void Start() {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (gameManager.isGameOn()) {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");


            bool fire = Input.GetButton("Fire1");

            bool swap = Input.GetButtonDown("Fire2");


            if (player != null) {
                player.GetComponent<player_mover>().setMovement(moveHorizontal, moveVertical);
                player.GetComponent<player_shoot>().setFire(fire);
                player.GetComponent<player_shoot>().swapShot(swap);
            }
        } else {
            gameManager.startGame(Input.GetButton("Fire1"));
            player = GameObject.FindWithTag("Player");
        }

    }
}
