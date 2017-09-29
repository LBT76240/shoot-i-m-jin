using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class config : MonoBehaviour {

   
    static public bool dashButton = false;    // this is reachable from everywhere
    
    public void setDashButton (bool value) {
        dashButton = value;
    }

    public bool getDashButton() {
        return dashButton;
    }
}
