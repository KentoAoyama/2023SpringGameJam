using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class FadeSystem : MonoBehaviour
{
    [Tooltip("�t�F�[�h�Ɏg��Image")]
    [SerializeField] 
    private Graphic _fadeImage; 

    [Tooltip("�t�F�[�h�ɂ����鎞��")]
    [SerializeField] 
    private float _fadeTime = 3f;


    void Awake()
    {
        _fadeImage.gameObject.SetActive(true);
    }


    public void StartFadeIn()//�t�F�[�h�C���֐�
    {
        _fadeImage.DOFade(endValue: 0f, duration: _fadeTime).OnComplete(() => _fadeImage.gameObject.SetActive(false));
    }


    public void StartFadeOut(string scene)//�t�F�[�h�A�E�g�֐�
    {
        _fadeImage.gameObject.SetActive(true);
        _fadeImage.DOFade(endValue: 1f, duration: _fadeTime).OnComplete(() => SceneManager.LoadScene(scene));
    }


    public void Exit()
    {
        Application.Quit();
    }


    void OnDisable()
    {
        DOTween.KillAll();
    }
}