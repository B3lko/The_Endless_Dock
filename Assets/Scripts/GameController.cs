using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject Dock;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Left;
    [SerializeField] private GameObject Middle;
    [SerializeField] private GameObject Right;
    [SerializeField] private GameObject Box;
    [SerializeField] private GameObject Canyon;
    [SerializeField] private GameObject Barrel;
    [SerializeField] private GameObject Sombrilla;
    [SerializeField] private GameObject Barriles;
    [SerializeField] private GameObject Fish;
    [SerializeField] private GameObject ButtonPause;
    [SerializeField] private GameObject ScoreText;
    [SerializeField] private GameObject btnPause;
    [SerializeField] private GameObject btnResume;
    [SerializeField] private GameObject btnMenu;
    [SerializeField] private GameObject btnMenu2;
    [SerializeField] private GameObject Pause;
    [SerializeField] private GameObject Sea;
    [SerializeField] private GameObject Island1;
    [SerializeField] private GameObject Island2;
    [SerializeField] private GameObject Island3;
    [SerializeField] private GameObject Obs;
    [SerializeField] private GameObject lost;
    [SerializeField] private GameObject camera;
    private float first = 100;
    private float sep = 20f;
    private float cord;
    private float cord2;
    private float GameSpeed = 350f;
    private float SpeedAUment = 0.025f;
    private int score = 0;
    int zzz = 32;
    bool isPause = false;
    List<GameObject> Docks = new List<GameObject>();
    List<GameObject> Seas = new List<GameObject>();
    List<GameObject> Bloks = new List<GameObject>();
    List<GameObject> leftIsland = new List<GameObject>();
    List<GameObject> rightIsland = new List<GameObject>();
    List<GameObject> Obstaculos = new List<GameObject>();
    List<GameObject> OOBBSS = new List<GameObject>();
    List<GameObject> randObjects = new List<GameObject>();
    GameObject Aux;
    void Start(){
        GenerateDocks();
        GenerateSeas();
        GenerateIsland();
        GenerateObstaculos();
        cord = Player.transform.position.z;
    }
    
    void GenerateIsland(){

        GameObject aux;
        GameObject aux2;


        switch(Random.Range(0, 3)){
            case 0: aux = Island1;break;
            case 1: aux = Island2;break;
            case 2: aux = Island3;break;
            default: aux = Island1;break;
        }

        switch(Random.Range(0, 3)){
            case 0: aux2 = Island1;break;
            case 1: aux2 = Island2;break;
            case 2: aux2 = Island3;break;
            default: aux2 = Island1;break;
        }
        
        leftIsland.Add(Instantiate(aux, new Vector3(-100,0,100), transform.rotation));
        rightIsland.Add(Instantiate(aux2, new Vector3(100,0,100), transform.rotation));

        for(int i = 1; i < 6; i++){

            switch(Random.Range(0, 3)){
                case 0: aux = Island1;break;
                case 1: aux = Island2;break;
                case 2: aux = Island3;break;
                default: aux = Island1;break;
            }

            switch(Random.Range(0, 3)){
                case 0: aux2 = Island1;break;
                case 1: aux2 = Island2;break;
                case 2: aux2 = Island3;break;
                default: aux2 = Island1;break;
            }

            leftIsland.Add( Instantiate(aux, new Vector3(-100,0,leftIsland[i-1].transform.position.z + 150), transform.rotation) );
            rightIsland.Add( Instantiate(aux2, new Vector3(100,0,rightIsland[i-1].transform.position.z + 150), transform.rotation) );
        }
    }

    void GenerateDocks(){
        Docks.Add( Instantiate(Dock, new Vector3(0,-3,13), transform.rotation) );
        for(int i = 1; i < 13; i++){
            Docks.Add( Instantiate(Dock, new Vector3(0,-3,Docks[i-1].transform.position.z + 32), transform.rotation) );
        }
    }

    public void Lost(){
        isPause = true;
        lost.transform.localScale = Vector3.zero;
        btnPause.transform.DOScale(Vector3.zero,0.5f);
        camera.transform.DOLocalRotate(new Vector3(30,0,0),0.5f , RotateMode.Fast).OnComplete(() => {
            lost.SetActive(true);
            lost.transform.DOScale(Vector3.one,0.5f).SetDelay(1f);
        });
    }

    void GenerateSeas(){
        Seas.Add( Instantiate(Sea, new Vector3(0,-4,70), transform.rotation) );
        for(int i = 1; i < 5; i++){
            Seas.Add( Instantiate(Sea, new Vector3(0,-4,Seas[i-1].transform.position.z + 153), transform.rotation) );
        }
    }

    void UpdateDocks(){
        if(Docks[0].transform.position.z + 20 < Player.transform.position.z){
            GameObject aux = Docks[0];
            for(int i = 0; i < Docks.Count - 1; i++){
                Docks[i] = Docks[i + 1]; 
            }
            aux.transform.position = new Vector3(0, -3, Docks[Docks.Count -1].transform.position.z + zzz);
            Docks[Docks.Count -1] = aux;
        }
    }

    void UpdateSeas(){
        if(Seas[0].transform.position.z + 80 < Player.transform.position.z){
            GameObject aux = Seas[0];
            for(int i = 0; i < Seas.Count - 1; i++){
                Seas[i] = Seas[i + 1]; 
            }
            aux.transform.position = new Vector3(0, -4, Seas[Seas.Count -1].transform.position.z + 153);
            Seas[Seas.Count -1] = aux;
        }
    }

    void UpdateIslands(){
        if(leftIsland[0].transform.position.z + 80 < Player.transform.position.z){
            GameObject aux = leftIsland[0];
            GameObject aux2 = rightIsland[0];
            for(int i = 0; i < leftIsland.Count - 1; i++){
                leftIsland[i] = leftIsland[i + 1]; 
                rightIsland[i] = rightIsland[i + 1]; 
            }
            aux.transform.position = new Vector3(-100, 0, leftIsland[leftIsland.Count -1].transform.position.z + 150);
            aux2.transform.position = new Vector3(100, 0, rightIsland[rightIsland.Count -1].transform.position.z + 150);

            leftIsland[leftIsland.Count -1] = aux;
            rightIsland[rightIsland.Count -1] = aux2;
        }
    }
    
    void UpdateObstacles(){
        if(Obstaculos[0].transform.position.z + 80 < Player.transform.position.z){
            GameObject aux = Obstaculos[0];
            for(int i = 0; i < Obstaculos.Count - 1; i++){
                Obstaculos[i] = Obstaculos[i + 1]; 
            }
            aux.transform.position = new Vector3(0, 0, Obstaculos[Obstaculos.Count -1].transform.position.z + 50);
            Obstaculos[Obstaculos.Count -1] = aux;
        }
    }
    
    void GenerateObstaculos(){
        GameObject Aux = Obs;
        for(int k = 0; k < 10; k++){

            Aux = Obs;
            List<GameObject> OOBBSS = new List<GameObject>();
            List<GameObject> randObjects = new List<GameObject>();

            for(int i = 0; i < 3; i++){
                List<bool> isLarge = new List<bool>();

                for(int j = 0; j < 3; j++){
                    isLarge.Add(false);
                    int rand = Random.Range(0, 6);
                    switch(rand){
                        case 0: randObjects.Add(Box); break;
                        case 1: randObjects.Add(Canyon); break;
                        case 2: randObjects.Add(Barrel); break;
                        case 3: randObjects.Add(Barriles); isLarge[j] = true; break;
                        case 4: randObjects.Add(Sombrilla); isLarge[j] = true; break;
                        case 5: randObjects.Add(Fish); isLarge[j] = true; break;
                    }

                }

                if(isLarge[0] && isLarge[1] && isLarge[2]){
                    switch(Random.Range(0, 3)){
                        case 0: randObjects[Random.Range(3, 6)] = Box; break;
                        case 1: randObjects[Random.Range(3, 6)] = Canyon; break;
                        case 2: randObjects[Random.Range(3, 6)] = Barrel; break;
                    }
                }

            }
            
            if(k == 0){
                Obstaculos.Add(Instantiate(Aux, new Vector3(0, 0, 50f), transform.rotation));
            }
            else{
                Obstaculos.Add(Instantiate(Aux, new Vector3(0, 0, Obstaculos[k - 1].transform.position.z + 50), transform.rotation));
            }

            cord2 = Obstaculos[k].transform.position.z;

            OOBBSS.Add(Instantiate(randObjects[0], new Vector3(Left.transform.position.x - 1,  0, cord2), transform.rotation));
            OOBBSS.Add(Instantiate(randObjects[1], new Vector3(Middle.transform.position.x,0, cord2), transform.rotation));
            OOBBSS.Add(Instantiate(randObjects[2], new Vector3(Right.transform.position.x + 1, 0, cord2), transform.rotation));

            OOBBSS.Add(Instantiate(randObjects[3], new Vector3(Left.transform.position.x - 1,  0, cord2 + sep), transform.rotation));
            OOBBSS.Add(Instantiate(randObjects[4], new Vector3(Middle.transform.position.x,0, cord2 + sep), transform.rotation));
            OOBBSS.Add(Instantiate(randObjects[5], new Vector3(Right.transform.position.x + 1, 0, cord2 + sep), transform.rotation));

            OOBBSS.Add(Instantiate(randObjects[6], new Vector3(Left.transform.position.x - 1,  0, cord2 + sep * 2), transform.rotation));
            OOBBSS.Add(Instantiate(randObjects[7], new Vector3(Middle.transform.position.x,0, cord2 + sep * 2), transform.rotation));
            OOBBSS.Add(Instantiate(randObjects[8], new Vector3(Right.transform.position.x + 1, 0, cord2 + sep * 2), transform.rotation));


            for(int n = 0; n < 9; n++){
                OOBBSS[n].transform.SetParent(Obstaculos[k].transform);
            }

        }
        
    }
    void Update(){
        if(!isPause){

            GameSpeed += SpeedAUment;
            UpdateDocks();
            UpdateSeas();
            UpdateIslands();
            UpdateObstacles();

            Player.GetComponent<PlayerMove>().SetSpeed(GameSpeed);
            score = Mathf.RoundToInt(GameSpeed/10) - 35;
            ScoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();

            if(btnPause.GetComponent<PressBtn>().isPressed){
                btnPause.GetComponent<PressBtn>().isPressed = false;
                btnPause.transform.DOScale(0,0.5f);
                Pause.transform.localScale = Vector3.zero;
                Pause.SetActive(true);
                Pause.transform.DOScale(1,0.5f);
                isPause = true;
                Player.GetComponent<PlayerMove>().SetPause(isPause);
            }
        }
        else{

            if(btnResume.GetComponent<PressBtn>().isPressed){
                btnResume.GetComponent<PressBtn>().isPressed = false;
                isPause = false;
                Pause.transform.DOScale(0,0.5f).OnComplete(() => {
                    Pause.SetActive(false);
                });
                btnPause.transform.DOScale(1,0.5f);
                Player.GetComponent<PlayerMove>().SetPause(isPause);
            }

            if(btnMenu.GetComponent<PressBtn>().isPressed){
                btnMenu.GetComponent<PressBtn>().isPressed = false;
                SceneManager.LoadScene("MainMenu");
            }

            if(btnMenu2.GetComponent<PressBtn>().isPressed){
                btnMenu2.GetComponent<PressBtn>().isPressed = false;
                SceneManager.LoadScene("MainMenu");
            }

        }
    }
}