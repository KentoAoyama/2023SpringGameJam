using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [Header("Title�ɕ\���������")]
    [SerializeField]
    private Image _titleLogo;

    [SerializeField]
    private Text _titleText;

    [SerializeField]
    private Graphic _helpImage;

    [SerializeField]
    private float _titleAnimationDuration = 1f;

    [SerializeField]
    private float _titleFadeDuration = 1f;

    [SerializeField]
    private Ease _titleLogoEase;

    [SerializeField]
    private Ease _loopEase;

    [Header("�C���Q�[���Ŏg�p�������")]

    [SerializeField]
    private GameObject _ui;

    [SerializeField]
    private Slider _timeSlider;

    [SerializeField]
    private Text _timeText;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Graphic[] _camera;

    [SerializeField]
    private Graphic _warningPanel;

    [Range(0f, 1f)]
    [SerializeField]
    private float _waringPanelAlpha = 0.2f;

    [Header("�Q�[���I�����Ɏg�p�������")]
    [SerializeField]
    private Text _finishText;

    private int _beforeChangeCamera = 0;

    private float _score;

    /// <summary>
    /// Warning�̏������A���ŌĂ΂�Ȃ��悤�ɂ��邽�߂̕ϐ�
    /// </summary>
    private bool _isCheckWorning = false;

    /// <summary>
    /// Finish���̏������A���ŌĂ΂�Ȃ��悤�ɂ���
    /// </summary>
    private bool _isCheck = false;

    public void Initialize()
    {
        if (_camera.Length != 0)
        _camera.ToList().ForEach(c => c.gameObject.SetActive(false));

        _finishText.text = "";
        _ui.SetActive(false);
    }

    /// <summary>
    /// �V�[�����s���̃^�C�g���ōs������
    /// </summary>
    public IEnumerator SceneStart()
    {
        Sequence sequence = DOTween.Sequence();

        yield return sequence
            .Insert(0f, _titleLogo.rectTransform.DOScale(0f, 0f))
            .Insert(0f, _titleText.rectTransform.DOScale(0f, 0f))
            .Insert(0f, _helpImage.rectTransform.DOScale(0f, 0f))
            .Insert(_titleAnimationDuration, _titleLogo.rectTransform.DOScale(1f, _titleAnimationDuration).SetEase(_titleLogoEase))
            .Insert(_titleAnimationDuration, _titleText.rectTransform.DOScale(1f, _titleAnimationDuration).SetEase(_titleLogoEase))
            .Insert(_titleAnimationDuration, _helpImage.rectTransform.DOScale(1f, _titleAnimationDuration).SetEase(_titleLogoEase))
            .OnComplete(() => _titleText.rectTransform.DOMoveY(150f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(_loopEase))
            .WaitForCompletion();
    }

    /// <summary>
    /// �Q�[���̊J�n���ɍs������
    /// </summary>
    public IEnumerator GameStart()
    {
        Sequence sequence = DOTween.Sequence();

        sequence
            .Insert(0f, _titleLogo.DOFade(0f, _titleFadeDuration))
            .Insert(0f, _titleText.DOFade(0f, _titleFadeDuration))
            .Insert(0f, _helpImage.DOFade(0f, _titleFadeDuration));

        yield return new WaitForSeconds(1f);

        _ui.SetActive(true);

        ChangeScore(0f);
    }

    public void Warning()
    {
        if (_isCheckWorning) return; 

        _warningPanel.DOFade(_waringPanelAlpha, 0.3f).SetLoops(6, LoopType.Yoyo);
        SoundManager.Instance.Play(1, 2);
        _isCheckWorning = true;
    }

    public void ChangeTimeSlider(float value)
    {
        _timeSlider.value = value;
    }

    public void ChangeTimeText(float time)
    {
        _timeText.text = $"{time:00}";
    }

    public void ChangeScoreText()
    {
        _scoreText.text = _score.ToString("0000");
    }

    /// <summary>
    /// �X�R�A���ς�����ۂɌĂт������\�b�h
    /// </summary>
    public void ChangeScore(float addScore)
    {
        float endValue = _score + addScore;

        DOTween.To(
            () => _score,
            (s) => _score = s,
            endValue,
            1f);
    }

    /// <summary>
    /// �J������UI��ω������郁�\�b�h
    /// </summary>
    /// <param name="cameraNumber">�ύX����J�����̔z��ԍ�</param>
    public void ChangeCameraUINum(int cameraNumber)
    {
        if (cameraNumber < 0 || cameraNumber > _camera.Length)
        {
            Debug.LogError("�J�����̔ԍ��ɂȂ��l���w�肳��܂���");
        }

        _camera[_beforeChangeCamera].gameObject.SetActive(false);
        _camera[cameraNumber].gameObject.SetActive(true);

        //�ύX���ɑO�̃J������UI���������߁A�C���f�b�N�X��ۑ����Ă���
        _beforeChangeCamera = cameraNumber;
    }

    public void Finish(string text)
    {
        if (_isCheck) return;

        _finishText.text = text;

        _finishText.rectTransform.DOScale(1f, 0.5f).SetEase(Ease.OutElastic);
        _finishText.DOFade(1f, _titleFadeDuration);

        _isCheck = true;
    }
}
