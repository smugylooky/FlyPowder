using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls
{

    public static bool isMovingLeft() {
        return Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
    }

    public static bool isMovingRight() {
        return Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    }

    public static bool isJumping() {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }

    public static bool isShooting() {
        return Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E);
    }

    public static bool isReloading() {
        return Input.GetKeyDown(KeyCode.R);
    }
}
