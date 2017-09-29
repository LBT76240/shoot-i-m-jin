using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ennemyFactory : MonoBehaviour {
    public gameManager gameManager;
    public GameObject ennemy;
    List<GameObject> listOfEnnemy;
    // Use this for initialization
    void Start() {
        listOfEnnemy = new List<GameObject>();
        for (int i = 0; i < 20; i++) {
            GameObject newBulletEnnemy = Instantiate(ennemy, Vector3.zero, Quaternion.identity);
            newBulletEnnemy.SetActive(false);
            listOfEnnemy.Add(newBulletEnnemy);



        }
    }
    public void createEnnemy() {
        if (listOfEnnemy.Count == 0) {
            Vector3 pos = gameManager.getSpawnEnnemyPosition();
            Instantiate(ennemy, pos, Quaternion.identity);
        } else {
            GameObject ennemy = listOfEnnemy[listOfEnnemy.Count - 1];
            ennemy.transform.position = gameManager.getSpawnEnnemyPosition();
            
            ennemy.SetActive(true);
            ennemy.GetComponent<ennemy_mover>().reset();
            ennemy.GetComponent<ennemy_shoot>().reset();

            listOfEnnemy.RemoveAt(listOfEnnemy.Count - 1);
        }
    }
    public void addNonUsedEnnemy(GameObject ennemy) {
        listOfEnnemy.Add(ennemy);

    }

}
