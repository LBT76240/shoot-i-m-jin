using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_mover : MonoBehaviour {

    public int scrollSpeed = 1;

    public float tileSizeX;

    private Vector3 startPosition;
    private float oldPosition = 0f;

    // Use this for initialization
    void Start() {
        startPosition = transform.position;
    }

    

    // Update is called once per frame
    void Update () {


        float newPosition = Mathf.Repeat(Time.deltaTime * scrollSpeed + oldPosition, tileSizeX);
        oldPosition = newPosition;
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
