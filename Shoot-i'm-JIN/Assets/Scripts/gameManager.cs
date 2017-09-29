using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {
    public GameObject player;
    public GameObject ennemy;
    public GameObject boss1;
    public GameObject ui;
    public GameObject gameOverui;
    public Text gameOverText;
    public Text victoryText;
    public Text scoreEndText;
    public bool GameIsOn = false;
    float timeOver;
    public float timeOverLimit;
    public Vector3 spawnValues;
    int score;
    public Text scoreText;
    bool firstPhasedEnded;
    public float timePreBoss;
    float time;
    public float timeSpawnEnnemy;
    float timeSpawn;
    public ennemyFactory ennemyFactory;


    public Vector3 getSpawnEnnemyPosition() {
        Vector3 newPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);

        return newPosition;
    }
    public bool isGameOn() {
        return GameIsOn;
    }

    public void endGame(bool isVictory) {
        ui.SetActive(false);

        GameObject[] listOfEnnemy = GameObject.FindGameObjectsWithTag("Ennemy");

        for (int i = 0; i < listOfEnnemy.Length; i++) {
            
            Destroy(listOfEnnemy[i]);
            
        }

        GameObject[] listOfBoss1 = GameObject.FindGameObjectsWithTag("boss1");

        for (int i = 0; i < listOfBoss1.Length; i++) {

            Destroy(listOfBoss1[i]);

        }

        GameObject[] listOfPowerUp = GameObject.FindGameObjectsWithTag("powerUp");

        for (int i = 0; i < listOfPowerUp.Length; i++) {

            Destroy(listOfPowerUp[i]);

        }

        GameObject[] listOfPlayerBullet = GameObject.FindGameObjectsWithTag("player_shoot");

        for (int i = 0; i < listOfPlayerBullet.Length; i++) {

            Destroy(listOfPlayerBullet[i]);

        }
        GameObject[] listOfEnnemyBullet = GameObject.FindGameObjectsWithTag("ennemy_shoot");

        for (int i = 0; i < listOfEnnemyBullet.Length; i++) {

            Destroy(listOfEnnemyBullet[i]);

        }

        GameObject[] listOfPlayer = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < listOfPlayer.Length; i++) {

            Destroy(listOfPlayer[i]);

        }
        if (isVictory) {
            gameOverText.text = "";
        } else {
            victoryText.text = "";
        }
        string newscorestring = "Score : " + score;
        scoreEndText.text = newscorestring;
        gameOverui.SetActive(true);

        GameIsOn = false;
    }

    public void addScore(int score) {
        
        this.score += score;
        
        updateScoreText();
    }

    void updateScoreText() {
        string newscorestring = "Score : " + score;
        scoreText.text = newscorestring;
    }

    void Start() {
        GameIsOn = true;
        ui.SetActive(true);
        gameOverui.SetActive(false);
        Instantiate(player, Vector3.zero, Quaternion.identity);
        
        
        score = 0;
        updateScoreText();
        firstPhasedEnded = false;
    }

    void Update() {
        if(!firstPhasedEnded && GameIsOn) {
            time += Time.deltaTime;
            timeSpawn += Time.deltaTime;
            if (time> timePreBoss) {
                firstPhasedEnded = true;
                Vector3 pos = Vector3.zero;
                pos.x = 30;
                Instantiate(boss1, pos, Quaternion.identity);
            } else {
                if(timeSpawn>timeSpawnEnnemy) {

                    ennemyFactory.createEnnemy();
                    /*
                    Vector3 pos = getSpawnEnnemyPosition();
                    Instantiate(ennemy, pos, Quaternion.identity);*/

                    timeSpawn = 0f;
                }
            }

        }
        if(!GameIsOn) {
            timeOver += Time.deltaTime;

            if(timeOver>timeOverLimit) {
                SceneManager.LoadScene(0);
            }

            
        }
        
    }
	
}
