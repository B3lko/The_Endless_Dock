using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour{
    [SerializeField] private GameObject gameManager;
    public void Exit(){
        Application.Quit();
    }

    public void SetScene(string name){
        SceneManager.LoadScene(name);
    }

    public void Credits(){
        gameManager.GetComponent<MainMenuController>().Credits();
    }

    public void Home(){
        DOTween.Kill(gameManager);
        gameManager.GetComponent<MainMenuController>().FinishCredits();
    }

}
