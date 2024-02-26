using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressBtn : MonoBehaviour{
    public bool isPressed = false;
    public void SetPressed(bool state){
        isPressed = state;
    }
}
