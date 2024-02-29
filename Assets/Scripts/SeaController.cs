using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SeaController : MonoBehaviour
{
    public float posX;

    void Start(){
        RestartTween();
    }
     void RestartTween(){
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        transform.DOMoveX(135,50f * -posX/10 ).SetEase(Ease.Linear).OnComplete(() => {
            RestartTween();
        });
    }
}
