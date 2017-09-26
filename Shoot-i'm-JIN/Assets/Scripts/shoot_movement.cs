using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_movement : MonoBehaviour {
	// Update is called once per frame
    public float speedH = 1;
    public float speedV = 0;
    public float rotateSpeed = 0;
    public float radius = 0;
    public bool rotate = false;

    Vector3 center;
    float angle;

    void Start() {
        center = gameObject.transform.position;
    }

    void Update () {
        if (rotate) {
            center= gameObject.transform.position + Vector3.right * Time.deltaTime * speedH + Vector3.up * Time.deltaTime * speedV;

            angle += rotateSpeed * Time.deltaTime;

            var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle),0) * radius;

            gameObject.transform.position = center + offset;
        } else {
            gameObject.transform.position = gameObject.transform.position + Vector3.right * Time.deltaTime * speedH + Vector3.up * Time.deltaTime * speedV;
        }

    }
}
