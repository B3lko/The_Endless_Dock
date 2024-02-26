using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    [SerializeField] private Transform Player;
    void Update(){
        transform.position = new Vector3(Player.position.x, 5f, Player.position.z - 6f);   
    }
}
