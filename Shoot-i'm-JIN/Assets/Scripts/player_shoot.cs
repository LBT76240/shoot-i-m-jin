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

    public float costShot1;
    public float costShot2;
    public float costShot3;

    public float delayShoot;
    Slider slider;
    CanvasRenderer sliderBackground;
    CanvasRenderer sliderFill;
    Image tire_droit_selected;
    Image tire_diagonal_selected;
    Image tire_spirale_selected;
    player_mover player_Mover;

    bool fire = false;

    bool canFire = true;

    float energy = 100;
    public float energyPerSecond;

    int selectedShot = 1;

    float timeSinceLastShoot = 0f;

    bulletFactory bulletFactory;

    public void swapShot(bool swap) {
        if (player_Mover != null) {
            if (!player_Mover.isDead()) {
                if (swap) {
                    selectedShot++;
                    if (selectedShot > 3) {
                        selectedShot = 1;
                    }

                    switch (selectedShot) {
                        case 1:
                            tire_droit_selected.enabled = true;
                            tire_diagonal_selected.enabled = false;
                            tire_spirale_selected.enabled = false;
                            break;
                        case 2:
                            tire_droit_selected.enabled = false;
                            tire_diagonal_selected.enabled = true;
                            tire_spirale_selected.enabled = false;
                            break;
                        case 3:
                            tire_droit_selected.enabled = false;
                            tire_diagonal_selected.enabled = false;
                            tire_spirale_selected.enabled = true;
                            break;
                    }
                }
            }
        }
    }

    public void setFire(bool newFire) {
        fire = newFire;
    }

    void Start() {
        player_Mover = gameObject.GetComponent<player_mover>();
        slider = GameObject.FindWithTag("energy_slider").GetComponent<Slider>();
        sliderBackground = GameObject.FindWithTag("energy_slider_background").GetComponent<CanvasRenderer>();
        sliderFill = GameObject.FindWithTag("energy_slider_fill").GetComponent<CanvasRenderer>();

        tire_droit_selected = GameObject.FindWithTag("tire_droit_selected").GetComponent<Image>();
        tire_diagonal_selected = GameObject.FindWithTag("tire_diagonal_selected").GetComponent<Image>();
        tire_spirale_selected = GameObject.FindWithTag("tire_spirale_selected").GetComponent<Image>();

        tire_droit_selected.enabled = true;
        tire_diagonal_selected.enabled = false;
        tire_spirale_selected.enabled = false;
        bulletFactory = GameObject.FindGameObjectWithTag("BulletFactory").GetComponent<bulletFactory>();

    }


    public void decreaseEnergy(float value) {
        energy -= value;
        if(energy<0) {
            energy = 0;
            canFire = false;
            sliderBackground.SetColor(Color.red);
            sliderFill.SetColor(Color.red);
        }
    }

    public bool energyAvailable () {
        return canFire;
    }


    public void increaseEnergy(float value) {
        if (!player_Mover.isDead()) {
            energy += value;
            if (energy >= 100) {
                energy = 100;
                canFire = true;
                sliderBackground.SetColor(Color.white);
                sliderFill.SetColor(Color.white);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (!player_Mover.isDead()) {
            timeSinceLastShoot += Time.deltaTime;

            if (energy < 100 && timeSinceLastShoot > delayShoot) {
                if (canFire) {
                    increaseEnergy(Time.deltaTime * energyPerSecond);
                } else {
                    increaseEnergy(Time.deltaTime * energyPerSecond * 0.75f);
                }

                
            }


            if (fire) {
                if (timeSinceLastShoot > delayShoot && canFire) {

                    switch (selectedShot) {
                        case 1:
                            bulletFactory.getBullet(BulletType.player1, shotspawn.transform.position);
                            
                            decreaseEnergy(costShot1);
                            break;
                        case 2:
                            bulletFactory.getBullet(BulletType.player2a, shotspawn.transform.position);
                            bulletFactory.getBullet(BulletType.player2b, shotspawn.transform.position);
                            
                            decreaseEnergy(costShot2);
                            break;
                        default:
                            bulletFactory.getBullet(BulletType.player3, shotspawn.transform.position);
                            
                            decreaseEnergy(costShot3);
                            break;
                    }


                    timeSinceLastShoot = 0;
                }
            }


            slider.value = energy;
        }
    }
}
