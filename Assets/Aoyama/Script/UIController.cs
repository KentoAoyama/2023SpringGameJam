using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIController : MonoBehaviour
{
    [Header("Titleに表示するもの")]
    [SerializeField]
    private Image _titleLogo;

    [SerializeField]
    private Text _titleText;

    [SerializeField]
    private Text _helpText;

    [Header("インゲームで使用するもの")]
    [SerializeField]
    private Slider _timeSlider;

    [SerializeField]
    private Text _timeText;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private GameObject _cameras;

    [SerializeField]
    private Graphic[] _camera;

    [Header("ゲーム終了時に使用するもの")]
    [SerializeField]
    private Text _finishText;

    private int _beforeChangeCamera = 0;

    public void Initialize()
    {
        if (_camera.Length != 0)
        _camera.ToList().ForEach(c => c.gameObject.SetActive(false));

        _timeSlider.value = 0f;
        _timeText.text = "";
        _scoreText.text = "";
        _finishText.text = "";
    }

    /// <summary>
    /// シーン実行時のタイトルで行う処理
    /// </summary>
    public IEnumerator SceneStart()
    {
        _titleLogo.gameObject.SetActive(true);
        _titleText.gameObject.SetActive(true);
        _helpText.gameObject.SetActive(true);

        //仮置き　DoTweenと併用する予定
        yield return new WaitForSeconds(0f);
    }

    /// <summary>
    /// ゲームの開始時に行う処理
    /// </summary>
    public void GameStart()
    {
        //ここにカメラを変更する処理を書く



        _titleLogo.gameObject?.SetActive(false);
        _titleText.gameObject?.SetActive(false);
        _helpText.gameObject?.SetActive(false);
    }

    public void ChangeTimeSlider(float value)
    {
        _timeSlider.value = value;
    }

    public void ChangeTimeText(float time)
    {
        _timeText.text = $"{time:00}";
    }

    public void ChangeScoreText(float score)
    {
        _scoreText.text = score.ToString("0000");
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
        _finishText.text = text;
    }
}
