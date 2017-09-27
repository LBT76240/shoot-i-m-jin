using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {
    public GameObject player;
    public GameObject ennemy;
    public GameObject boss1;
    public GameObject ui;
    public bool GameIsOn = false;
    public Vector3 spawnValues;

    public Vector3 getSpawnEnnemyPosition() {
        Vector3 newPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);

        return newPosition;
    }
    public bool isGameOn() {
        return GameIsOn;
    }

    void Start() {
        ui.SetActive(false);
    }

    public void startGame(bool start) {
        if(start) {
            GameIsOn = true;
            ui.SetActive(true);
            Instantiate(player, Vector3.zero, Quaternion.identity);
            Vector3 pos = Vector3.zero;
            pos.x = 30;
            Instantiate(boss1, pos, Quaternion.identity);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
