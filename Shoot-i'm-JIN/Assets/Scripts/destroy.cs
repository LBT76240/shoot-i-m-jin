using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour {

    public GameObject gameOb;

	public void destroyObject() {
        Destroy(gameOb);
    }
}
