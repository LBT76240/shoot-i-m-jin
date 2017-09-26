using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {
    public GameObject player;
    public GameObject ennemy;

    public bool GameIsOn = false;
	
    public bool isGameOn() {
        return GameIsOn;
    }

    public void startGame(bool start) {
        if(start) {
            GameIsOn = true;
            Instantiate(player, Vector3.zero, Quaternion.identity);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
