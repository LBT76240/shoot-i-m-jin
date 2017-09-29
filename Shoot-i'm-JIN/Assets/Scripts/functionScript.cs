using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class functionScript : MonoBehaviour {

    public GameObject mainMenuCanvas;
    public GameObject optionCanvas;
    public GameObject levelsCanvas;
    public Text dashButtonText;
    

    public void ChangeToScene(int sceneToChangeTo) {
        

        SceneManager.LoadScene(sceneToChangeTo);
        
    }

    

    public void Start() {
        mainMenu();

        if(gameObject.GetComponent<config>().getDashButton()) {
            dashButtonText.text = "Dash : Shift";
        }
        
    }

    public void menuLevels() {
        mainMenuCanvas.SetActive(false);
        optionCanvas.SetActive(false);
        levelsCanvas.SetActive(true);
    }

    public void menuOption() {
        mainMenuCanvas.SetActive(false);
        optionCanvas.SetActive(true);
        levelsCanvas.SetActive(false);
    }

    public void mainMenu() {
        mainMenuCanvas.SetActive(true);
        optionCanvas.SetActive(false);
        levelsCanvas.SetActive(false);
    }

    public void switchDashMod() {
        if(gameObject.GetComponent<config>().getDashButton()) {
            gameObject.GetComponent<config>().setDashButton(false);
            dashButtonText.text = "Dash : Double Tap";
        } else {
            gameObject.GetComponent<config>().setDashButton(true);
            dashButtonText.text = "Dash : Shift";
        }
    }

}
