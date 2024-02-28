using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour{
    //[SerializeField] private float speed;
    private CharacterController Controller;
    private Animator animator;
    float horizontal;
    float vertical;
    Vector3 move = Vector3.zero;
    private int index = 0; //0 Middle 1 Left 2 Right
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Middle;
    [SerializeField] private Transform Right;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject Model;
    [SerializeField] private GameObject gameController;
    private float speedTween = 0.25f;
    private float fallSpeed;
    private float speed = 0;
    bool isPause = false;

    public void SetSpeed(float spd){
        speed = spd;
    }
    void Awake(){  
        Controller = GetComponent<CharacterController>(); //Componente CharacterController
        animator = GetComponent<Animator>(); //Componente animator
    }

    void FixedUpdate() {
        if(!isPause){
            //move = Vector3.zero;
            move = new Vector3(0, move.y, speed * Time.deltaTime);
            Controller.Move(move * Time.deltaTime);
        }
    }

    public void SetPause(bool state){
        isPause = state;
        animator.enabled = !state;
    }

    public void Restart(){
        isPause = false;
        animator.SetBool("isColliding",false);
        //animator.enabled = !state;
    }

    void Update(){
        if(!isPause){
            //move = Vector3.zero;
            Inputs();
            SetGravity();
            SetJump();
            //Controller.Move(move * Time.deltaTime);
        }
    }

    private void SetGravity(){
        if(Controller.isGrounded){
            animator.SetBool("isJumping",false);
            fallSpeed = -gravity * Time.deltaTime;
        }
        else{
            fallSpeed -= gravity * Time.deltaTime;
        }
        move.y = fallSpeed;
    }

    private void SetJump(){
        if(Controller.isGrounded && Input.GetKeyDown(KeyCode.W)){
            fallSpeed = jumpForce;
            move = Vector3.zero;
            move.y = jumpForce;
            Controller.Move(move * Time.deltaTime);
            animator.SetBool("isJumping",true);
        }
    }

    void Inputs(){

        if(Input.GetKeyDown(KeyCode.A)){

            if(index == 2){
                animator.SetBool("GoingLeft",true);
                Model.transform.DOMoveX(Middle.position.x, speedTween).OnComplete(() => {
                    animator.SetBool("GoingLeft",false);
                });
                float newCenterX = Middle.position.x; // Puedes ajustar este valor según tus necesidades
                Vector3 newCenter = Controller.center;
                newCenter.x = newCenterX;
                Controller.center = newCenter;
                index = 0;
                return;
            }
            if(index == 0){
                animator.SetBool("GoingLeft",true);
                Model.transform.DOMoveX(Left.position.x, speedTween).OnComplete(() => {
                    animator.SetBool("GoingLeft",false);
                });
                float newCenterX = Left.position.x; // Puedes ajustar este valor según tus necesidades
                Vector3 newCenter = Controller.center;
                newCenter.x = newCenterX;
                Controller.center = newCenter;
                index = 1;
                return;
            }

        }

        if(Input.GetKeyDown(KeyCode.D)){
            if(index == 1){
                animator.SetBool("GoingRight",true);
                Model.transform.DOMoveX(Middle.position.x, speedTween).OnComplete(() => {
                    animator.SetBool("GoingRight",false);
                });
                float newCenterX = Middle.position.x; // Puedes ajustar este valor según tus necesidades
                Vector3 newCenter = Controller.center;
                newCenter.x = newCenterX;
                Controller.center = newCenter;
                index = 0;
                return;
            }
            if(index == 0){
                animator.SetBool("GoingRight",true);
                Model.transform.DOMoveX(Right.position.x, speedTween).OnComplete(() => {
                    animator.SetBool("GoingRight",false);
                });
                float newCenterX = Right.position.x; // Puedes ajustar este valor según tus necesidades
                Vector3 newCenter = Controller.center;
                newCenter.x = newCenterX;
                Controller.center = newCenter;
                index = 2;
                return;
            }

        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit.collider.gameObject.tag == "Obstacle"){
            animator.SetBool("isColliding",true);
            isPause = !isPause;
            transform.DOMoveZ(transform.position.z - 13, 0.5f);
            transform.DOMoveY(-0.35f, 0.5f);
            gameController.GetComponent<GameController>().Lost();
        }
    }

}
