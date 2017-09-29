using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public gameManager gameManager;
    //public bool dashButton;
    GameObject player = null;
    LastTap lastTap = LastTap.None;
    float timeBetweenInput;
    void Start() {
        player = GameObject.FindWithTag("Player");
    }
	
    

    void tryDash (bool tapHorizontalUp, bool tapHorizontalDown, bool tapVerticalUp, bool tapVerticalDown,bool isFire3Down,float moveHorizontal, float moveVertical) {

        if (!gameObject.GetComponent<config>().getDashButton()) {
            
            if (tapHorizontalUp) {
                
                if (lastTap == LastTap.HorizontalUp) {
                    if (timeBetweenInput < 1f) {
                        player.GetComponent<player_mover>().dash(Vector2.right);

                        lastTap = LastTap.None;
                    } else {
                        //print("DashHP Too Slow");
                        timeBetweenInput = 0f;
                    }
                } else {
                    //print("NewDashHP");
                    lastTap = LastTap.HorizontalUp;
                    timeBetweenInput = 0f;
                }

            }


            if (tapHorizontalDown) {
                
                if (lastTap == LastTap.HorizontalDown) {
                    if (timeBetweenInput < 1f) {
                        player.GetComponent<player_mover>().dash(-Vector2.right);

                        lastTap = LastTap.None;
                    } else {
                        //print("DashHN Too Slow");
                        timeBetweenInput = 0f;
                    }
                } else {
                    //print("NewDashHN");
                    lastTap = LastTap.HorizontalDown;
                    timeBetweenInput = 0f;
                }

            }

            if (tapVerticalUp) {
                
                if (lastTap == LastTap.VerticalUp) {
                    if (timeBetweenInput < 1f) {
                        player.GetComponent<player_mover>().dash(Vector2.up);

                        lastTap = LastTap.None;
                    } else {
                        //print("DashVP Too Slow");
                        timeBetweenInput = 0f;
                    }
                } else {
                    //print("NewDashVP");
                    lastTap = LastTap.VerticalUp;
                    timeBetweenInput = 0f;
                }

            }

            if (tapVerticalDown) {
                
                if (lastTap == LastTap.VerticalDown) {
                    if (timeBetweenInput < 1f) {
                        player.GetComponent<player_mover>().dash(-Vector2.up);

                        lastTap = LastTap.None;
                    } else {
                        //print("DashVN Too Slow");
                        timeBetweenInput = 0f;
                    }
                } else {
                    //print("NewDashVN");
                    lastTap = LastTap.VerticalDown;
                    timeBetweenInput = 0f;
                }

            }
        } else {
            if(isFire3Down) {
                if(moveHorizontal>0) {
                    moveHorizontal = 1;
                }
                if (moveHorizontal<0) {
                    moveHorizontal = -1;
                }
                if (moveVertical > 0) {
                    moveVertical = 1;
                }
                if (moveVertical < 0) {
                    moveVertical = -1;
                }
                if(moveHorizontal!=0 && moveVertical!=0) {
                    moveHorizontal /=1.41f;
                    moveVertical /= 1.41f;
                }
                Vector2 dir = new Vector2(
                    moveHorizontal,
                    moveVertical
                );
                player.GetComponent<player_mover>().dash(dir);
            }
        }
    }



	// Update is called once per frame
	void Update () {


        bool tapHorizontalUp = (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0) || (lastTap == LastTap.HorizontalDown && Input.GetAxisRaw("Horizontal") > 0);
        bool tapHorizontalDown = (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0) || (lastTap == LastTap.HorizontalUp && Input.GetAxisRaw("Horizontal") < 0);
        bool tapVerticalUp = (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0) || (lastTap == LastTap.VerticalDown && Input.GetAxisRaw("Vertical") > 0);
        bool tapVerticalDown = (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0) || (lastTap == LastTap.VerticalUp && Input.GetAxisRaw("Vertical") < 0);
        bool isFire3Down = Input.GetButtonDown("Fire3");
        


        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");


        bool fire = Input.GetButton("Fire1");

        bool swap = Input.GetButtonDown("Fire2");
        

        if (player != null) {
            tryDash(tapHorizontalUp, tapHorizontalDown, tapVerticalUp, tapVerticalDown, isFire3Down, moveHorizontal, moveVertical);
            //player.GetComponent<player_mover>().dash(tapHorizontalUp, tapHorizontalDown, tapVerticalUp, tapVerticalDown);
            player.GetComponent<player_mover>().setMovement(moveHorizontal, moveVertical);
            player.GetComponent<player_shoot>().setFire(fire);
            player.GetComponent<player_shoot>().swapShot(swap);
        }
        

    }
}
