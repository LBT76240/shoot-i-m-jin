using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinBoss : MonoBehaviour {
    public float speedRotation;
    

	
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(speedRotation* Vector3.forward * Time.deltaTime);
    }
}
