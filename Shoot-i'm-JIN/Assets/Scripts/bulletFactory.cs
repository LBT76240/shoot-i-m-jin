using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { ennemy, boss, player1, player2a, player2b, player3 }

public class bulletFactory : MonoBehaviour {
    
    public GameObject bulletEnnemy;
    List<GameObject> listOfbulletEnnemy;
    public GameObject bulletBoss;
    List<GameObject> listOfbulletBoss;
    public GameObject bulletPlayer1;
    List<GameObject> listOfbulletPlayer1;
    public GameObject bulletPlayer2a;
    List<GameObject> listOfbulletPlayer2a;
    public GameObject bulletPlayer2b;
    List<GameObject> listOfbulletPlayer2b;
    public GameObject bulletPlayer3;
    List<GameObject> listOfbulletPlayer3;


    public void Start() {
        listOfbulletEnnemy = new List<GameObject>();
        listOfbulletBoss = new List<GameObject>();
        listOfbulletPlayer1 = new List<GameObject>();
        listOfbulletPlayer2a = new List<GameObject>();
        listOfbulletPlayer2b = new List<GameObject>();
        listOfbulletPlayer3 = new List<GameObject>();

        for(int i =0;i<100;i++) {
            GameObject newBulletEnnemy = Instantiate(bulletEnnemy, Vector3.zero, Quaternion.identity);
            newBulletEnnemy.SetActive(false);
            listOfbulletEnnemy.Add(newBulletEnnemy);

            GameObject newBulletBoss = Instantiate(bulletBoss, Vector3.zero, Quaternion.identity);
            newBulletBoss.SetActive(false);
            listOfbulletBoss.Add(newBulletBoss);

            GameObject newBulletPlayer1 = Instantiate(bulletPlayer1, Vector3.zero, Quaternion.identity);
            newBulletPlayer1.SetActive(false);
            listOfbulletPlayer1.Add(newBulletPlayer1);

            GameObject newBulletPlayer2a = Instantiate(bulletPlayer2a, Vector3.zero, Quaternion.identity);
            newBulletPlayer2a.SetActive(false);
            newBulletPlayer2a.transform.Rotate(Vector3.forward * 45);
            listOfbulletPlayer2a.Add(newBulletPlayer2a);

            GameObject newBulletPlayer2b = Instantiate(bulletPlayer2b, Vector3.zero, Quaternion.identity);
            newBulletPlayer2b.SetActive(false);
            newBulletPlayer2b.transform.Rotate(Vector3.forward * -45);
            listOfbulletPlayer2b.Add(newBulletPlayer2b);

            GameObject newBulletPlayer3 = Instantiate(bulletPlayer3, Vector3.zero, Quaternion.identity);
            newBulletPlayer3.SetActive(false);
            listOfbulletPlayer3.Add(newBulletPlayer3);

        }

    }

    public void getBullet(BulletType bulletType, Vector3 pos) {
        switch(bulletType) {
            case BulletType.ennemy:
                if (listOfbulletEnnemy.Count == 0) {
                    Instantiate(bulletEnnemy, pos, Quaternion.identity);
                } else {
                    GameObject bullet = listOfbulletEnnemy[0];
                    bullet.transform.position = pos;
                    bullet.SetActive(true);
                    
                    listOfbulletEnnemy.RemoveAt(0);
                }
                break;
            case BulletType.boss:
                if (listOfbulletBoss.Count == 0) {
                    Instantiate(bulletBoss, pos, Quaternion.identity);
                } else {
                    GameObject bullet = listOfbulletBoss[0];
                    bullet.transform.position = pos;
                    bullet.SetActive(true);
                    
                    listOfbulletBoss.RemoveAt(0);

                }
                break;
            case BulletType.player1:
                if (listOfbulletPlayer1.Count == 0) {
                    Instantiate(bulletPlayer1, pos, Quaternion.identity);
                } else {
                    GameObject bullet = listOfbulletPlayer1[0];
                    bullet.transform.position = pos;
                    bullet.SetActive(true);
                    
                    listOfbulletPlayer1.RemoveAt(0);
                    
                }
                break;
            case BulletType.player2a:
                if (listOfbulletPlayer2a.Count == 0) {
                    Instantiate(bulletPlayer2a, pos, Quaternion.identity).transform.Rotate(Vector3.forward * 45);
                } else {
                    GameObject bullet = listOfbulletPlayer2a[0];
                    bullet.transform.position = pos;
                    bullet.SetActive(true);
                    
                    listOfbulletPlayer2a.RemoveAt(0);
                }
                break;
            case BulletType.player2b:
                if (listOfbulletPlayer2b.Count == 0) {
                    Instantiate(bulletPlayer2b, pos, Quaternion.identity).transform.Rotate(Vector3.forward * -45);
                } else {
                    GameObject bullet = listOfbulletPlayer2b[0];
                    bullet.transform.position = pos;
                    bullet.SetActive(true);
                    
                    listOfbulletPlayer2b.RemoveAt(0);
                }
                break;
            case BulletType.player3:
                if (listOfbulletPlayer3.Count == 0) {
                    Instantiate(bulletPlayer3, pos, Quaternion.identity);
                } else {
                    GameObject bullet = listOfbulletPlayer3[0];
                    bullet.transform.position = pos;
                    bullet.SetActive(true);
                    
                    listOfbulletPlayer3.RemoveAt(0);
                }
                break;

        }
        
    }

    public void addNonUsedBullet(BulletType bulletType,GameObject bullet) {
        
        switch (bulletType) {
            case BulletType.ennemy:
                
                listOfbulletEnnemy.Add(bullet);
                break;
            case BulletType.boss:
                
                listOfbulletBoss.Add(bullet);
                
                break;
            case BulletType.player1:
                
                listOfbulletPlayer1.Add(bullet);
                
                
                
                break;
            case BulletType.player2a:
                
                listOfbulletPlayer2a.Add(bullet);
               
                break;
            case BulletType.player2b:
                
                listOfbulletPlayer2b.Add(bullet);
                
                break;
            case BulletType.player3:
                
                listOfbulletPlayer3.Add(bullet);
                
                break;

        }

    }


    

}
