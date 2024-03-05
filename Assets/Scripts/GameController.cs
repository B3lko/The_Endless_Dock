using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject prefab_dock;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject middle;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject obstacle_barrel;
    [SerializeField] private GameObject obstacle_cannon;
    [SerializeField] private GameObject obstacle_red_barrel;
    [SerializeField] private GameObject obstacle_box_and_bullets;
    [SerializeField] private GameObject obstacle_barrel_stack_3;
    [SerializeField] private GameObject obstacle_box_and_rum;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject btnPause;
    [SerializeField] private GameObject btnResume;
    [SerializeField] private GameObject btnMenu;
    [SerializeField] private GameObject btnMenu2;
    [SerializeField] private GameObject btnRestart;
    [SerializeField] private GameObject btnRestart2;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject prefab_sea;
    [SerializeField] private GameObject prefab_island_1;
    [SerializeField] private GameObject prefab_island_2;
    [SerializeField] private GameObject prefab_island_3;
    [SerializeField] private GameObject lost;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject coinsText;
    [SerializeField] private GameObject uiPressToStart;
    [SerializeField] private GameObject uiCoins;
    [SerializeField] private GameObject uiScore;
    [SerializeField] private GameObject ship_1;
    [SerializeField] private GameObject ship_2;
    [SerializeField] private GameObject map;
    [SerializeField] private AudioSource music;
    [SerializeField] Material materialConShader;
    public ParticleSystem particles_coin;
    private GameObject ship = null;
    private float coinsGrab = 0;
    private float obstacle_pos_y = -0.3f;
    private float obstacles_sep = 50f;
    private float cord2;
    private float gameSpeed = 500f;
    private float speed_increase = 0.025f;
    private int score = 0;
    int posZ_dock = 32;
    private float pref_separation = 150;
    private float pref_separation_island = 100;
    bool isPause = true;
    bool isStart = true;
    List<GameObject> docks = new List<GameObject>();
    List<GameObject> seas = new List<GameObject>();
    List<GameObject> bloks = new List<GameObject>();
    List<GameObject> leftIsland = new List<GameObject>();
    List<GameObject> rightIsland = new List<GameObject>();
    List<GameObject> obstacles = new List<GameObject>();
    List<GameObject> obstacles_aux_container = new List<GameObject>();
    List<GameObject> randObjects = new List<GameObject>();
    List<GameObject> coins = new List<GameObject>();
    List<bool> isLarge = new List<bool>();

    void Start(){
        GenerateDocks();
        GenerateSeas();
        GenerateIsland();
        GenerateObstaculos();
        GenerateCoins();
        GenerateShip();
        ship_1.transform.localScale = new Vector3(5,5,5);
        ship_2.transform.localScale = new Vector3(5,5,5);
    }
    public void SetTextCoin(){
        coinsGrab += 1;
        coinsText.GetComponent<TextMeshProUGUI>().text = coinsGrab.ToString();
    }

    void UpdateShip(){
        if(ship.transform.position.z < player.transform.position.z -20){
            Destroy(ship);
            GenerateShip();
        }
    }

    void GenerateShip(){

        GameObject typeShip = null;
        int posShipX = 0;

        if(Random.Range(0, 2) == 0){
            posShipX = -25;
        }
        else{
            posShipX = 25;
        }

        if(Random.Range(0, 2) == 0){
            typeShip = ship_1;
        }
        else{
            typeShip = ship_2;
        }

        ship = Instantiate(typeShip, new Vector3(posShipX,-5,player.transform.position.z + 2000), transform.rotation);
        ship.transform.rotation = Quaternion.Euler(0,180,0);
        ship.transform.DOMoveZ(player.transform.position.z,15f);
        ship.transform.parent = map.transform;
    }
    void GenerateIsland(){

        GameObject aux;
        GameObject aux2;


        switch(Random.Range(0, 3)){
            case 0: aux = prefab_island_1;break;
            case 1: aux = prefab_island_2;break;
            case 2: aux = prefab_island_3;break;
            default: aux = prefab_island_1;break;
        }

        switch(Random.Range(0, 3)){
            case 0: aux2 = prefab_island_1;break;
            case 1: aux2 = prefab_island_2;break;
            case 2: aux2 = prefab_island_3;break;
            default: aux2 = prefab_island_1;break;
        }
        
        leftIsland.Add(Instantiate(aux, new Vector3(-pref_separation_island,0,pref_separation_island), transform.rotation));
        rightIsland.Add(Instantiate(aux2, new Vector3(pref_separation_island,0,pref_separation_island), transform.rotation));

        for(int i = 1; i < 3; i++){

            switch(Random.Range(0, 3)){
                case 0: aux = prefab_island_1;break;
                case 1: aux = prefab_island_2;break;
                case 2: aux = prefab_island_3;break;
                default: aux = prefab_island_1;break;
            }

            switch(Random.Range(0, 3)){
                case 0: aux2 = prefab_island_1;break;
                case 1: aux2 = prefab_island_2;break;
                case 2: aux2 = prefab_island_3;break;
                default: aux2 = prefab_island_1;break;
            }

            leftIsland.Add( Instantiate(aux, new Vector3(-pref_separation_island,0,leftIsland[i-1].transform.position.z + pref_separation), transform.rotation) );
            rightIsland.Add( Instantiate(aux2, new Vector3(pref_separation_island,0,rightIsland[i-1].transform.position.z + pref_separation), transform.rotation) );
            leftIsland[i].transform.parent = map.transform;
            rightIsland[i].transform.parent = map.transform;
        }
    }

    void GenerateDocks(){
        docks.Add( Instantiate(prefab_dock, new Vector3(0,-3,13), transform.rotation));
        for(int i = 1; i < 5; i++){
            docks.Add( Instantiate(prefab_dock, new Vector3(0,-3,docks[i-1].transform.position.z + 32), transform.rotation) );
        }
    }

    public void Lost(){
        isPause = true;
        lost.transform.localScale = Vector3.zero;
        btnPause.transform.DOScale(Vector3.zero,0.5f);
        lost.SetActive(true);
        lost.transform.DOScale(Vector3.one,0.5f).SetDelay(1f);
    }

    void GenerateSeas(){
        seas.Add(Instantiate(prefab_sea, new Vector3(0,-4,70), transform.rotation) );
        for(int i = 1; i < 2; i++){
            seas.Add(Instantiate(prefab_sea, new Vector3(0,-4,seas[i-1].transform.position.z + pref_separation), transform.rotation) );
        }
    }

    void GenerateCoins(){
        coins.Clear();
        coins.Add(Instantiate(coin, new Vector3(0,1,player.transform.position.z + 100 + Random.Range(-10, 11)), coin.transform.rotation) );
        for(int i = 1; i < Random.Range(5, 11); i++){
            float posCoinX;
            switch(Random.Range(0, 3)){
                case 0:posCoinX = left.transform.position.x;break;
                case 1:posCoinX = middle.transform.position.x;break;
                case 2:posCoinX = right.transform.position.x;break;
                default: posCoinX = 0;break;
            }

            coins.Add( Instantiate(coin, new Vector3(posCoinX,1,coins[i-1].transform.position.z + 5), coin.transform.rotation) );
        }
    }

    void UpdateDocks(){
        if(docks[0].transform.position.z + 20 < player.transform.position.z){
            GameObject aux = docks[0];
            for(int i = 0; i < docks.Count - 1; i++){
                docks[i] = docks[i + 1]; 
            }
            aux.transform.position = new Vector3(0, -3, docks[docks.Count -1].transform.position.z + posZ_dock);
            docks[docks.Count -1] = aux;
        }
    }

    void UpdateCoins(){
        if(coins[coins.Count - 1].transform.position.z + 20 < player.transform.position.z){
            for(int i = 0; i < coins.Count; i++){
                Destroy(coins[i]);
            }
            GenerateCoins();
        }
    }

    void UpdateSeas(){
        if(seas[0].transform.position.z + 80 < player.transform.position.z){
            GameObject aux = seas[0];
            for(int i = 0; i < seas.Count - 1; i++){
                seas[i] = seas[i + 1]; 
            }
            aux.transform.position = new Vector3(0, -4, seas[seas.Count -1].transform.position.z + pref_separation);
            seas[seas.Count -1] = aux;
        }
    }

    void UpdateIslands(){
        if(leftIsland[0].transform.position.z + 80 < player.transform.position.z){
            GameObject aux = leftIsland[0];
            GameObject aux2 = rightIsland[0];
            for(int i = 0; i < leftIsland.Count - 1; i++){
                leftIsland[i] = leftIsland[i + 1]; 
                rightIsland[i] = rightIsland[i + 1]; 
            }
            aux.transform.position = new Vector3(-pref_separation_island, 0, leftIsland[leftIsland.Count -1].transform.position.z + pref_separation);
            aux2.transform.position = new Vector3(pref_separation_island, 0, rightIsland[rightIsland.Count -1].transform.position.z + pref_separation);

            leftIsland[leftIsland.Count -1] = aux;
            rightIsland[rightIsland.Count -1] = aux2;
        }
    }
    
    void UpdateObstacles(){
        if(obstacles[0].transform.position.z + pref_separation < player.transform.position.z){
            GameObject aux = obstacles[0];
            for(int i = 0; i < obstacles.Count - 1; i++){
                obstacles[i] = obstacles[i + 1]; 
            }
            aux.transform.position = new Vector3(0, 0, obstacles[obstacles.Count -1].transform.position.z + pref_separation);
            obstacles[obstacles.Count -1] = aux;
        }
    }

    public void EmitParticle(Vector3 posParticles){
        particles_coin.transform.position = posParticles;
        particles_coin.Play();
        particles_coin.transform.DOMoveY(particles_coin.transform.position.y + 2f, 0.25f);
    }
    
    void GenerateObstaculos(){

        for(int i = 0; i < 3; i++){
            isLarge.Add(false);
        }

        for(int k = 0; k < 10; k++){

            List<GameObject> obstacles_aux_container = new List<GameObject>();
            List<GameObject> randObjects = new List<GameObject>();

            for(int i = 0; i < 3; i++){
                for(int j = 0; j < 3; j++){
                    isLarge[j] = false;
                    int rand = Random.Range(0, 6);
                    switch(rand){
                        case 0: randObjects.Add(obstacle_barrel); isLarge[j] = false; break;
                        case 1: randObjects.Add(obstacle_cannon); isLarge[j] = false; break;
                        case 2: randObjects.Add(obstacle_red_barrel); isLarge[j] = false; break;
                        case 3: randObjects.Add(obstacle_barrel_stack_3); isLarge[j] = true; break;
                        case 4: randObjects.Add(obstacle_box_and_bullets); isLarge[j] = true; break;
                        case 5: randObjects.Add(obstacle_box_and_rum); isLarge[j] = true; break;
                    }

                }
                if(isLarge[0] && isLarge[1] && isLarge[2]){
                    switch(i){
                        case 0: 
                            randObjects[Random.Range(0, 3)] = obstacle_barrel; 
                            break;
                        case 1: randObjects[Random.Range(3, 6)] = obstacle_cannon; break;
                        case 2: randObjects[Random.Range(6, 9)] = obstacle_red_barrel; break;
                    }
                }

                for(int h = 0; h < 3; h++){
                    isLarge[h] = false;
                }
            }
            
            if(k == 0){
                obstacles.Add(Instantiate(new GameObject(), new Vector3(0, 0, 50f), transform.rotation));
            }
            else{
                obstacles.Add(Instantiate(new GameObject(), new Vector3(0, 0, obstacles[k - 1].transform.position.z + pref_separation), transform.rotation));
            }

            cord2 = obstacles[k].transform.position.z;

            obstacles_aux_container.Add(Instantiate(randObjects[0], new Vector3(left.transform.position.x - 1, obstacle_pos_y, cord2), transform.rotation));
            obstacles_aux_container.Add(Instantiate(randObjects[1], new Vector3(middle.transform.position.x, obstacle_pos_y, cord2), transform.rotation));
            obstacles_aux_container.Add(Instantiate(randObjects[2], new Vector3(right.transform.position.x + 1, obstacle_pos_y, cord2), transform.rotation));

            int randMiddle = Random.Range(-10, 11);
            obstacles_aux_container.Add(Instantiate(randObjects[3], new Vector3(left.transform.position.x - 1, obstacle_pos_y, cord2 + obstacles_sep + randMiddle), transform.rotation));
            obstacles_aux_container.Add(Instantiate(randObjects[4], new Vector3(middle.transform.position.x, obstacle_pos_y, cord2 + obstacles_sep + randMiddle), transform.rotation));
            obstacles_aux_container.Add(Instantiate(randObjects[5], new Vector3(right.transform.position.x + 1, obstacle_pos_y, cord2 + obstacles_sep + randMiddle), transform.rotation));

            int randForward = Random.Range(-10, 11);
            obstacles_aux_container.Add(Instantiate(randObjects[6], new Vector3(left.transform.position.x - 1, obstacle_pos_y, cord2 + (obstacles_sep + randForward) * 2), transform.rotation));
            obstacles_aux_container.Add(Instantiate(randObjects[7], new Vector3(middle.transform.position.x, obstacle_pos_y, cord2 + (obstacles_sep + randForward) * 2), transform.rotation));
            obstacles_aux_container.Add(Instantiate(randObjects[8], new Vector3(right.transform.position.x + 1, obstacle_pos_y, cord2 + (obstacles_sep + randForward) * 2), transform.rotation));


            for(int n = 0; n < 9; n++){
                obstacles_aux_container[n].transform.SetParent(obstacles[k].transform);
            }

        }
        
    }
    void Update(){
        if(!isPause){

            gameSpeed += speed_increase;
            UpdateDocks();
            UpdateSeas();
            UpdateIslands();
            UpdateObstacles();
            UpdateCoins();
            UpdateShip();

            player.GetComponent<PlayerMove>().SetSpeed(gameSpeed);
            score = Mathf.RoundToInt(gameSpeed/10) - 50;
            scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();

            if(btnPause.GetComponent<PressBtn>().isPressed){
                btnPause.GetComponent<PressBtn>().isPressed = false;
                btnPause.transform.DOScale(0,0.5f);
                pause.transform.localScale = Vector3.zero;
                pause.SetActive(true);
                pause.transform.DOScale(1,0.5f);
                isPause = true;
                player.GetComponent<PlayerMove>().SetPause(isPause);
            }
        }
        else if(isStart){
            if(Input.GetKeyDown(KeyCode.Space)){
                player.GetComponent<PlayerMove>().StopDancing();
                isStart = !isStart;
                isPause = !isPause;
                music.DOFade(1, 0.5f);
                uiPressToStart.transform.DOScale(0,0.5f);
                btnPause.transform.DOScale(1,0.5f);
                uiCoins.transform.DOScale(1,0.5f);
                uiScore.transform.DOScale(1,0.5f);
                player.GetComponent<PlayerMove>().SetPause(isPause);
            }
        }
        else{

            if(btnResume.GetComponent<PressBtn>().isPressed){
                btnResume.GetComponent<PressBtn>().isPressed = false;
                isPause = false;
                pause.transform.DOScale(0,0.5f).OnComplete(() => {
                    pause.SetActive(false);
                });
                btnPause.transform.DOScale(1,0.5f);
                player.GetComponent<PlayerMove>().SetPause(isPause);
            }

            if(btnMenu.GetComponent<PressBtn>().isPressed){
                btnMenu.GetComponent<PressBtn>().isPressed = false;
                SceneManager.LoadScene("MainMenu");
            }

            if(btnMenu2.GetComponent<PressBtn>().isPressed){
                btnMenu2.GetComponent<PressBtn>().isPressed = false;
                SceneManager.LoadScene("MainMenu");
            }

            if(btnRestart.GetComponent<PressBtn>().isPressed){
                btnRestart.GetComponent<PressBtn>().isPressed = false;
                ResetGame();
            }

            if(btnRestart2.GetComponent<PressBtn>().isPressed){
                btnRestart2.GetComponent<PressBtn>().isPressed = false;
                ResetGame();
            }

        }
    }

    void ResetGame(){
        SceneManager.LoadScene("World");
    }
}
