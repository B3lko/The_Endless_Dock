using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour{
    [SerializeField] private GameObject GameManager;
    public void Exit(){
        Application.Quit();
    }

    public void SetScene(string name){
        SceneManager.LoadScene(name);
    }

    public void Credits(){
        GameManager.GetComponent<MainMenuController>().Credits();
    }

}
