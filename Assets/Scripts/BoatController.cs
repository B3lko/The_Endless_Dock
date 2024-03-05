using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoatController : MonoBehaviour{
private float posX = -80;
private float posZ = -30;
    void Start(){
        RestartTween2();
    }

    void RestartTween(){
        transform.position = new Vector3(posX, 0, posZ);
        transform.DOMoveX(60,30f).OnComplete(() => {
            if(Random.Range(0, 2) == 0){
                RestartTween();
            }
            else{
                RestartTween2();
            }
        });
    }

    void RestartTween2(){
        transform.position = new Vector3(posX, 0, posZ);
        transform.DOMoveX(20,10f).OnComplete(() => {
            transform.DOLocalRotate(new Vector3(0,0,0),5 , RotateMode.Fast).OnComplete(() => {
                transform.DOMoveZ(90,10f).OnComplete(() => {
                    transform.DOLocalRotate(new Vector3(0,90,0), 5, RotateMode.Fast).OnComplete(() => {
                        transform.DOMoveX(100,5).OnComplete(() => {
                            if(Random.Range(0, 2) == 0){
                                RestartTween();
                            }
                            else{
                                RestartTween2();
                            }
                        });
                    });
                });
            });
        });
    }
}
