using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [Header("Titleに表示するもの")]
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

    [Header("インゲームで使用するもの")]

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

    [Header("ゲーム終了時に使用するもの")]
    [SerializeField]
    private Text _finishText;

    private int _beforeChangeCamera = 0;

    private float _score;

    /// <summary>
    /// Warningの処理が連続で呼ばれないようにするための変数
    /// </summary>
    private bool _isCheckWorning = false;

    /// <summary>
    /// Finish時の処理が連続で呼ばれないようにする
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
    /// シーン実行時のタイトルで行う処理
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
    /// ゲームの開始時に行う処理
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
    /// スコアが変わった際に呼びだすメソッド
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
    /// カメラのUIを変化させるメソッド
    /// </summary>
    /// <param name="cameraNumber">変更するカメラの配列番号</param>
    public void ChangeCameraUINum(int cameraNumber)
    {
        if (cameraNumber < 0 || cameraNumber > _camera.Length)
        {
            Debug.LogError("カメラの番号にない値が指定されました");
        }

        _camera[_beforeChangeCamera].gameObject.SetActive(false);
        _camera[cameraNumber].gameObject.SetActive(true);

        //変更時に前のカメラのUIを消すため、インデックスを保存しておく
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
