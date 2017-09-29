using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_by_oob : MonoBehaviour {

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.activeSelf) {
            if (other.CompareTag("ennemy_shoot")) {
                other.gameObject.SetActive(false);
                
                other.gameObject.GetComponent<contact_by_shoot>().recycleBullet();
            } else if (other.CompareTag("player_shoot")) {
               
                other.gameObject.SetActive(false);
                
                other.gameObject.GetComponent<contact_by_shoot>().recycleBullet();
            } else if (other.CompareTag("Ennemy")) {

                other.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("EnnemyFactory").GetComponent<ennemyFactory>().addNonUsedEnnemy(other.gameObject);
                
            } else {
                Destroy(other.gameObject);
            }
        }
    }
}
