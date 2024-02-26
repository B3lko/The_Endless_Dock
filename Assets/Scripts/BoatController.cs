using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoatController : MonoBehaviour{

    void Start(){
        RestartTween2();
    }

    void RestartTween(){
        transform.position = new Vector3(-80, 0, -30);
        transform.DOMoveX(60,30f).OnComplete(() => {
            RestartTween();
        });
    }

    void RestartTween2(){
        transform.position = new Vector3(-80, 0, -30);
        transform.DOMoveX(20,10f).OnComplete(() => {
            transform.DOLocalRotate(new Vector3(0,0,0),5 , RotateMode.Fast).OnComplete(() => {
                transform.DOMoveZ(90,10f).OnComplete(() => {
                    transform.DOLocalRotate(new Vector3(0,90,0), 5, RotateMode.Fast).OnComplete(() => {
                        transform.DOMoveX(100,5).OnComplete(() => {
                            RestartTween();
                        });
                    });
                });
            });
        });
    }
}
