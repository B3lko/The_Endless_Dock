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
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject creditsLayer;
    [SerializeField] private GameObject btnPlay;
    [SerializeField] private GameObject btnCredits;
    [SerializeField] private GameObject btnExit;
    [SerializeField] private GameObject btnHome;
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject student;
    [SerializeField] private GameObject year;
    [SerializeField] private GameObject commission;
    [SerializeField] private GameObject xImage;
    private List<Tween> tweens = new List<Tween>();

    void Start(){
        btnCredits.transform.DOScale(0,0f);
        btnPlay.transform.DOScale(0,0f);
        btnExit.transform.DOScale(0,0f);
        btnHome.transform.DOScale(0,0f);
        logo.transform.DOScale(0,0f);
        SetMenuUI();
        SetFade0();  
    }

    public void SetFade0(){
        creditsLayer.GetComponent<Image>().DOFade(0f, 0f);
        for(int i = 1; i<21; i++){
            creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(0, 0);
        }
        student.GetComponent<TextMeshProUGUI>().DOFade(0f, 0f);
        year.GetComponent<TextMeshProUGUI>().DOFade(0f, 0f);
        commission.GetComponent<TextMeshProUGUI>().DOFade(0f, 0f);
        xImage.GetComponent<Image>().DOFade(0f, 0f);
    }

    public void Credits(){
        btnCredits.transform.DOScale(0,0.5f);
        btnPlay.transform.DOScale(0,0.5f);
        btnExit.transform.DOScale(0,0.5f);
        logo.transform.DOScale(0,0.5f);
        mainCamera.transform.DOLocalRotate(new Vector3(5,-35,0),4f , RotateMode.Fast);
        player.GetComponent<PlayerMenuMove>().GoTo1();
    }

    public void ReadyPlayer1(){
        door.transform.DOLocalRotate(new Vector3(0,100,0),0.5f , RotateMode.Fast).OnComplete(() =>{
            creditsLayer.transform.DOScale(1,0.5f).SetDelay(1f);
            creditsLayer.GetComponent<Image>().DOFade(1f, 0.5f).OnComplete(() => {
                for(int i = 1; i<21; i++){
                    if(i == 6){
                        tweens.Add(creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(1f, 1f).SetDelay(i * 0.5f).OnComplete(() => {
                            student.GetComponent<TextMeshProUGUI>().DOFade(1f, 0.5f);
                        }));
                    }
                    if(i == 11){
                        tweens.Add(creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(1f, 1f).SetDelay(i * 0.5f).OnComplete(() => {
                            year.GetComponent<TextMeshProUGUI>().DOFade(1f, 0.5f);
                        }));
                    }
                    if(i == 17){
                        tweens.Add(creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(1f, 1f).SetDelay(i * 0.5f).OnComplete(() => {
                            commission.GetComponent<TextMeshProUGUI>().DOFade(1f, 0.5f);
                        }));
                    }
                    if(i == 19){
                        tweens.Add(creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(1f, 1f).SetDelay(i * 0.5f).OnComplete(() => {
                            xImage.GetComponent<Image>().DOFade(1f, 0.5f).OnComplete(() => {
                                transform.DOScale(1,1f).OnComplete(() => {
                                    FinishCredits();
                                });
                            });
                        }));
                    }
                    else{
                        tweens.Add(creditsLayer.transform.Find("Step" + i).GetComponent<Image>().DOFade(1f, 1f).SetDelay(i * 0.5f));
                    }
                }
                btnHome.transform.DOScale(1,0.5f);
                btnHome.GetComponent<Button>().interactable = true;
            });
        });
    }

    public void FinishCredits(){
        foreach (var tween in tweens){
            tween.Kill();
        }
        btnHome.GetComponent<Button>().interactable = false;
        btnHome.transform.DOScale(0,0.5f);
        creditsLayer.transform.DOScale(0,0.5f).OnComplete(() => {
            SetFade0();
            creditsLayer.GetComponent<Image>().DOFade(0f, 0.5f);
            mainCamera.transform.DOLocalRotate(new Vector3(5,0,0),4f , RotateMode.Fast);
            door.transform.DOLocalRotate(new Vector3(0,0,0),0.5f , RotateMode.Fast).OnComplete(() => {
                player.GetComponent<PlayerMenuMove>().GoTo2();
            });
        });
    }

    public void SetMenuUI(){
        logo.transform.DOScale(1,1f).SetEase(Ease.OutBounce).OnComplete(() => {
            btnPlay.transform.DOScale(1.5f,0.5f).SetEase(Ease.OutBounce);
            btnCredits.transform.DOScale(1.5f,0.5f).SetEase(Ease.OutBounce).SetDelay(0.5f);
            btnExit.transform.DOScale(1.5f,0.5f).SetEase(Ease.OutBounce).SetDelay(1f);
        });
    }

}
