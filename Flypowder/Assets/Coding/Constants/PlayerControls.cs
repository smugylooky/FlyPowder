using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls
{

    public static bool isMovingLeft() {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    }

    public static bool isMovingRight() {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }

    public static bool isJumping() {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }

    public static bool isShooting(int i) {
        if (i == (int)formaDisparo.AUTOMATICO) { 
            return Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E); 
        } else {
            return Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E);  
        }
    }

    public static bool isReloading() {
        return Input.GetKeyDown(KeyCode.R);
    }

    public static bool isCrouching()
    {
        return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }
}
