using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls
{

    public static bool isMovingLeft() {
        return Input.GetKey(KeyCode.A);
    }

    public static bool isMovingRight() {
        return Input.GetKey(KeyCode.D);
    }

    public static bool isJumping() {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }

    public static bool isShooting() {
        return Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E);
    }
}
