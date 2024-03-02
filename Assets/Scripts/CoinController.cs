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
}
