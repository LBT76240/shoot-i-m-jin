using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_shoot : MonoBehaviour {
    public GameObject shot1;
    public GameObject shot2a;
    public GameObject shot2b;
    public GameObject shot3;
    public GameObject shotspawn;
    public float delayShoot;
    Slider slider;
    CanvasRenderer sliderBackground;
    CanvasRenderer sliderFill;

    bool fire = false;

    bool canFire = true;

    float energy = 100;
    public float energyPerSecond;

    int selectedShot = 1;

    float timeSinceLastShoot = 0f;

    public void swapShot(bool swap) {
        if (swap) {
            selectedShot++;
            if (selectedShot > 3) {
                selectedShot = 1;
            }
        }
    }

    public void setFire(bool newFire) {
        fire = newFire;
    }

    void Start() {

        slider = GameObject.FindWithTag("energy_slider").GetComponent<Slider>();
        sliderBackground = GameObject.FindWithTag("energy_slider_background").GetComponent<CanvasRenderer>();
        sliderFill = GameObject.FindWithTag("energy_slider_fill").GetComponent<CanvasRenderer>();





    }


    void decreaseEnergy(float value) {
        energy -= value;
        if(energy<0) {
            energy = 0;
            canFire = false;
            sliderBackground.SetColor(Color.red);
            sliderFill.SetColor(Color.red);
        }
    }

    // Update is called once per frame
    void Update() {
        timeSinceLastShoot += Time.deltaTime;

        if(energy<100) {
            if(canFire) {
                energy += Time.deltaTime * energyPerSecond;
            } else {
                energy += Time.deltaTime * energyPerSecond*0.75f;
            }
            
            if(energy>100) {
                canFire = true;
                sliderBackground.SetColor(Color.white);
                sliderFill.SetColor(Color.white);
            }
        }


        if (fire) {
            if (timeSinceLastShoot > delayShoot) {
                if (canFire) {
                    switch (selectedShot) {
                        case 1:
                            Instantiate(shot1, shotspawn.transform.position, shotspawn.transform.rotation);
                            decreaseEnergy(10);
                            break;
                        case 2:
                            Instantiate(shot2a, shotspawn.transform.position, shotspawn.transform.rotation);
                            Instantiate(shot2b, shotspawn.transform.position, shotspawn.transform.rotation);
                            decreaseEnergy(20);
                            break;
                        default:
                            Instantiate(shot3, shotspawn.transform.position, shotspawn.transform.rotation);
                            decreaseEnergy(15);
                            break;
                    }
                }
                
                timeSinceLastShoot = 0;
            }
        }


        slider.value = energy;
    }
}
