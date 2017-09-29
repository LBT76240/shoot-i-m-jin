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
    int currentLvl;
    
    configClass newconfigClass;
    bool configLoaded;
    bool isVictory;
    public TextAsset textAsset;
    List<string> listOfXMLLevel;
    public Vector3 getSpawnEnnemyPosition() {
        Vector3 newPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);

        return newPosition;
    }
    public bool isGameOn() {
        return GameIsOn;
    }

    private void reset() {
        isVictory = false;
        GameIsOn = true;
        ui.SetActive(true);
        gameOverui.SetActive(false);
        

        
        
        
        firstPhasedEnded = false;
        time = 0;
        timeSpawn = 0;
        timeOver = 0;

    }

    public void endGame(bool isVictory) {
        ui.SetActive(false);
        this.isVictory = isVictory;
        GameObject[] listOfEnnemy = GameObject.FindGameObjectsWithTag("Ennemy");

        for (int i = 0; i < listOfEnnemy.Length; i++) {
            if (isVictory && (currentLvl!= listOfXMLLevel.Count-1)) {
                if (listOfEnnemy[i].activeSelf) {
                    listOfEnnemy[i].SetActive(false);
                    GameObject.FindGameObjectWithTag("EnnemyFactory").GetComponent<ennemyFactory>().addNonUsedEnnemy(listOfEnnemy[i]);
                }
            } else {
                listOfEnnemy[i].SetActive(false);
            }

            
            
            
        }

        GameObject[] listOfBoss1 = GameObject.FindGameObjectsWithTag("boss1");

        for (int i = 0; i < listOfBoss1.Length; i++) {

            listOfBoss1[i].SetActive(false);

        }

        GameObject[] listOfPowerUp = GameObject.FindGameObjectsWithTag("powerUp");

        for (int i = 0; i < listOfPowerUp.Length; i++) {

            listOfPowerUp[i].SetActive(false);

        }

        GameObject[] listOfPlayerBullet = GameObject.FindGameObjectsWithTag("player_shoot");

        for (int i = 0; i < listOfPlayerBullet.Length; i++) {
            if (isVictory && (currentLvl != listOfXMLLevel.Count - 1)) {
                if (listOfPlayerBullet[i].activeSelf) {
                    listOfPlayerBullet[i].SetActive(false);
                    listOfPlayerBullet[i].GetComponent<contact_by_shoot>().recycleBullet();
                }
            } else {
                listOfPlayerBullet[i].SetActive(false);
            }


        }
        GameObject[] listOfEnnemyBullet = GameObject.FindGameObjectsWithTag("ennemy_shoot");

        for (int i = 0; i < listOfEnnemyBullet.Length; i++) {
            if (isVictory && (currentLvl != listOfXMLLevel.Count - 1)) {
                if (listOfEnnemyBullet[i].activeSelf) {
                    listOfEnnemyBullet[i].SetActive(false);
                    listOfEnnemyBullet[i].GetComponent<contact_by_shoot>().recycleBullet();
                }
            } else {
                listOfEnnemyBullet[i].SetActive(false);
            }



        }


        GameObject[] listOfPlayer = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < listOfPlayer.Length; i++) {
            if (currentLvl == listOfXMLLevel.Count - 1) {
                listOfPlayer[i].SetActive(false);
            }

        }

        if (isVictory) {
            gameOverText.text = "";
            victoryText.text = "\nVictory";
        } else {
            gameOverText.text = "Game Over";
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
        isVictory = false;
        GameIsOn = true;
        ui.SetActive(true);
        gameOverui.SetActive(false);
        Instantiate(player, Vector3.zero, Quaternion.identity);

        currentLvl = 0;
        score = 0;
        updateScoreText();
        firstPhasedEnded = false;

        configLoaded = false;
        try {
            listOfXMLLevel = XmlHelpers.DeserializeDatabaseFromXML<string>(textAsset);
        } catch (System.Exception exception) {
            Debug.LogError("Pas de fichier de conf general" + exception);
        }

        if(listOfXMLLevel.Count==0) {
            throw new System.Exception("Pas de niveau");
        } 

        try {
            newconfigClass = XmlHelpers.DeserializeFromXML<configClass>(listOfXMLLevel[currentLvl]);
            timeOverLimit = newconfigClass.timeOverLimit;
            timePreBoss= newconfigClass.timePreBoss;
            timeSpawnEnnemy= newconfigClass.timeSpawnEnnemy;
            configLoaded = true;
            Debug.Log("Fichier de conf chargé");
        } catch (System.Exception exception) {
            Debug.LogError("Pas de fichier de conf" + exception);
            
        }
        

        
    }

    void Update() {
        if(!firstPhasedEnded && GameIsOn) {
            time += Time.deltaTime;
            timeSpawn += Time.deltaTime;
            if (time> timePreBoss) {
                firstPhasedEnded = true;
                Vector3 pos = Vector3.zero;
                pos.x = 30;
                GameObject newBoss = Instantiate(boss1, pos, Quaternion.identity);
                if (configLoaded) {
                    newBoss.GetComponentInChildren<boss1_ia>().InitWithConf(newconfigClass);
                }
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
                if(isVictory && (currentLvl != listOfXMLLevel.Count - 1)) {
                    currentLvl++;
                    try {
                        newconfigClass = XmlHelpers.DeserializeFromXML<configClass>(listOfXMLLevel[currentLvl]);
                        timeOverLimit = newconfigClass.timeOverLimit;
                        timePreBoss = newconfigClass.timePreBoss;
                        timeSpawnEnnemy = newconfigClass.timeSpawnEnnemy;
                        configLoaded = true;
                        Debug.Log("Fichier de conf chargé");
                    } catch (System.Exception exception) {
                        Debug.LogError("Pas de fichier de conf" + exception);

                    }
                    reset();
                } else {
                    SceneManager.LoadScene(0);
                }
                

            }

            
        }
        
    }
	
}
