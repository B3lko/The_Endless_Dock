using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Door;
    [SerializeField] private GameObject creditsLayer;
    [SerializeField] private GameObject btnPlay;
    [SerializeField] private GameObject btnCredits;
    [SerializeField] private GameObject btnExit;
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject alumno;
    [SerializeField] private GameObject anio;
    [SerializeField] private GameObject comision;
    // Start is called before the first frame update
    void Start()
    {
        btnCredits.transform.DOScale(0,0f);
        btnPlay.transform.DOScale(0,0f);
        btnExit.transform.DOScale(0,0f);
        logo.transform.DOScale(0,0f);
        ReadyPlayer2();
        SetFade0();

        
    }

    public void SetFade0(){
        creditsLayer.GetComponent<Image>().DOFade(0f, 0f);
        for(int i = 1; i<19; i++){
            creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(0, 0);
        }
        alumno.GetComponent<TextMeshProUGUI>().DOFade(0f, 0f);
        anio.GetComponent<TextMeshProUGUI>().DOFade(0f, 0f);
        comision.GetComponent<TextMeshProUGUI>().DOFade(0f, 0f);
    }

    public void Credits(){
        btnCredits.transform.DOScale(0,0.5f);
        btnPlay.transform.DOScale(0,0.5f);
        btnExit.transform.DOScale(0,0.5f);
        logo.transform.DOScale(0,0.5f);
        mainCamera.transform.DOLocalRotate(new Vector3(5,-35,0),4f , RotateMode.Fast);
        Player.GetComponent<PlayerMenuMove>().GoTo1();
    }

    public void ReadyPlayer1(){
        Door.transform.DOLocalRotate(new Vector3(0,100,0),0.5f , RotateMode.Fast).OnComplete(() =>{
            creditsLayer.transform.DOScale(1,0.5f).SetDelay(1f);
            creditsLayer.GetComponent<Image>().DOFade(1f, 0.5f).OnComplete(() => {
                for(int i = 1; i<19; i++){
                    if(i == 7){
                        creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(1f, 1f).SetDelay(i * 0.5f).OnComplete(() => {
                            alumno.GetComponent<TextMeshProUGUI>().DOFade(1f, 0.5f);
                        });
                    }
                    if(i == 12){
                        creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(1f, 1f).SetDelay(i * 0.5f).OnComplete(() => {
                            anio.GetComponent<TextMeshProUGUI>().DOFade(1f, 0.5f);
                        });
                    }
                    if(i == 18){
                        creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(1f, 1f).SetDelay(i * 0.5f).OnComplete(() => {
                            comision.GetComponent<TextMeshProUGUI>().DOFade(1f, 0.5f).OnComplete(() => {
                                creditsLayer.transform.DOScale(0,0.5f).SetDelay(2f).OnComplete(() => {
                                    creditsLayer.GetComponent<Image>().DOFade(0f, 0.5f);
                                    SetFade0();
                                    mainCamera.transform.DOLocalRotate(new Vector3(5,0,0),4f , RotateMode.Fast);
                                    Door.transform.DOLocalRotate(new Vector3(0,0,0),0.5f , RotateMode.Fast).OnComplete(() => {
                                        Player.GetComponent<PlayerMenuMove>().GoTo2();
                                    });
                                });
                            });
                        });
                    }
                    else{
                        creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(1f, 1f).SetDelay(i * 0.5f);
                    }
                }
            });
        });
    }

    public void ReadyPlayer2(){
        logo.transform.DOScale(1,1f).SetEase(Ease.OutBounce).OnComplete(() => {
            btnPlay.transform.DOScale(1.5f,0.5f).SetEase(Ease.OutBounce);
            btnCredits.transform.DOScale(1.5f,0.5f).SetEase(Ease.OutBounce).SetDelay(0.5f);
            btnExit.transform.DOScale(1.5f,0.5f).SetEase(Ease.OutBounce).SetDelay(1f);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
