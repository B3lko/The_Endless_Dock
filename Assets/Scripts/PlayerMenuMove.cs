using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMenuMove : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject GameController;


    void Awake(){  
        animator = GetComponent<Animator>(); //Componente animator
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GoTo1(){
        animator.SetBool("isWalking",true);
        animator.SetBool("isDancing",false);
        transform.DOLocalRotate(new Vector3(0,-90,0),1f , RotateMode.Fast);
        transform.DOMoveX(-10,4f).SetEase(Ease.Linear).OnComplete(() => {
            GameController.GetComponent<MainMenuController>().ReadyPlayer1();
        });
    }
    public void GoTo2(){
        //animator.SetBool("isWalking",true);
        //animator.SetBool("isDancing",false);
        transform.DOLocalRotate(new Vector3(0,90,0),1f , RotateMode.Fast);
        transform.DOMoveX(2.8f,4f).SetEase(Ease.Linear).OnComplete(() => {
            transform.DOLocalRotate(new Vector3(0,210,0),1f , RotateMode.Fast);
            animator.SetBool("isWalking",false);
            animator.SetBool("isDancing",true);
            GameController.GetComponent<MainMenuController>().ReadyPlayer2();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
