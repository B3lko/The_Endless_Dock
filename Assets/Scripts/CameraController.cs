using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    [SerializeField] private Transform player;
    float posY = 4f;
    float posZ = -6f;
    void Update(){
        transform.position = new Vector3(player.position.x, posY, player.position.z + posZ);   
    }
}
