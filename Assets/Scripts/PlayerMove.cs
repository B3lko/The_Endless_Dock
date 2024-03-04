using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour{
    private CharacterController controller;
    private Animator animator;
    float horizontal;
    float vertical;
    Vector3 move = Vector3.zero;
    private int index = 0; //0 Middle 1 Left 2 Right
    [SerializeField] private Transform left;
    [SerializeField] private Transform middle;
    [SerializeField] private Transform right;
    [SerializeField] private float gravity;
    [SerializeField] private float gravityInitial;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpForceInitial;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject gameController;
    [SerializeField] private AudioSource coinSound;
    private float speedTween = 0.25f;
    private float fallSpeed;
    private float speed = 0;
    bool isPause = true;

    public void SetSpeed(float spd){
        speed = spd;
    }
    void Awake(){  
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        jumpForceInitial = jumpForce;
        gravityInitial = gravity;
        transform.rotation = Quaternion.Euler(0,180,0);
    }

    void FixedUpdate(){
        if(!isPause){
            move = new Vector3(0, move.y, speed * Time.deltaTime);
            controller.Move(move * Time.deltaTime);
        }
    }

    public void SetPause(bool state){
        isPause = state;
        animator.enabled = !state;
    }

    void Update(){
        if(!isPause){
            Inputs();
            SetGravity();
            SetDown();
            SetJump();
        }
    }

    private void SetGravity(){
        if(controller.isGrounded){
            animator.SetBool("isJumping",false);
            fallSpeed = -gravity * Time.deltaTime;
        }
        else{
            fallSpeed -= gravity * Time.deltaTime;
        }
        move.y = fallSpeed;
    }
    private void SetDown(){
        if(!controller.isGrounded && Input.GetKeyDown(KeyCode.S)){
            fallSpeed = -gravity * Time.deltaTime * 30;
        }
    }

    private void SetJump(){
        if(controller.isGrounded && Input.GetKeyDown(KeyCode.W)){
            fallSpeed = jumpForce;
            move = Vector3.zero;
            move.y = fallSpeed;
            controller.Move(move * Time.deltaTime);
            animator.SetBool("isJumping",true);
        }
    }

    public void StopDancing(){
        animator.SetBool("isDancing",false);
        transform.DOLocalRotate(new Vector3(0,0,0),0.0f , RotateMode.Fast);
    }

    void Inputs(){

        if(Input.GetKeyDown(KeyCode.A)){

            if(index == 2){
                animator.SetBool("GoingLeft",true);
                model.transform.DOMoveX(middle.position.x, speedTween).OnUpdate(() => {
                    Vector3 newCenter = controller.center;
                    newCenter.x = model.transform.position.x;
                    controller.center = newCenter;
                }).OnComplete(() => {
                    animator.SetBool("GoingLeft",false);
                });
                index = 0;
                return;
            }
            if(index == 0){
                animator.SetBool("GoingLeft",true);
                model.transform.DOMoveX(left.position.x, speedTween).OnUpdate(() => {
                    Vector3 newCenter = controller.center;
                    newCenter.x = model.transform.position.x;
                    controller.center = newCenter;
                }).OnComplete(() => {
                    animator.SetBool("GoingLeft",false);
                });
                index = 1;
                return;
            }
        }

        if(Input.GetKeyDown(KeyCode.D)){
            if(index == 1){
                animator.SetBool("GoingRight",true);
                model.transform.DOMoveX(middle.position.x, speedTween).OnUpdate(() => {
                    Vector3 newCenter = controller.center;
                    newCenter.x = model.transform.position.x;
                    controller.center = newCenter;
                }).OnComplete(() => {
                    animator.SetBool("GoingRight",false);
                });
                index = 0;
                return;
            }
            if(index == 0){
                animator.SetBool("GoingRight",true);
                model.transform.DOMoveX(right.position.x, speedTween).OnUpdate(() => {
                    Vector3 newCenter = controller.center;
                    newCenter.x = model.transform.position.x;
                    controller.center = newCenter;
                }).OnComplete(() => {
                    animator.SetBool("GoingRight",false);
                });
                index = 2;
                return;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit.collider.gameObject.tag == "Obstacle" || hit.collider.gameObject.tag == "ObstacleLarge"){
            animator.SetBool("isColliding",true);
            isPause = !isPause;
            transform.DOMoveZ(transform.position.z - 3, 0.5f);
            transform.DOMoveY(-0.35f, 0.5f);
            gameController.GetComponent<GameController>().Lost();
        }
        
    }
    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Collider>().gameObject.tag == "Coin"){
            gameController.GetComponent<GameController>().SetTextCoin();
            other.GetComponent<CoinController>().SetState();
            gameController.GetComponent<GameController>().EmitParticle(new Vector3(other.transform.position.x,other.transform.position.y,other.transform.position.z));
            coinSound.Play();
        }
    }
}
