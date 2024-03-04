using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinController : MonoBehaviour{

    void Start(){
        
        Spin();
    }
    
    void Spin(){
        transform.DORotate(new Vector3(90,0,180), 1, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() => {
            transform.DORotate(new Vector3(90,0,360), 1, RotateMode.Fast).SetEase(Ease.Linear).OnComplete(() => {
                Spin();
            });
        });
    }

    public void SetState(){
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Collider>().gameObject.tag == "ObstacleLarge"){
            gameObject.SetActive(false);
        }
        if(other.GetComponent<Collider>().gameObject.tag == "Obstacle"){
            transform.position = new Vector3(transform.position.x,3,transform.position.z);
        }
    }
    

    

    /*void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Collider>().gameObject.tag == "Player"){
            gameController.GetComponent<GameController>().SetTextCoin();
            gameObject.SetActive(false);
        }
    }*/
}
