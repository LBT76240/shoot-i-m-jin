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

            bool tapHorizontalUp = Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0;
            bool tapHorizontalDown= Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0;
            bool tapVerticalUp = Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0;
            bool tapVerticalDown = Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0;



            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");


            bool fire = Input.GetButton("Fire1");

            bool swap = Input.GetButtonDown("Fire2");


            if (player != null) {
                player.GetComponent<player_mover>().dash(tapHorizontalUp, tapHorizontalDown, tapVerticalUp, tapVerticalDown);
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
