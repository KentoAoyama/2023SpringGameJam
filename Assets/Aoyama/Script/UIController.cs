using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Slider _timeSlider;

    [SerializeField]
    private Text _timeText;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Graphic[] _camera;

    private int _beforeChangeCamera = 0;

    public void Initialized()
    {
        if (_camera.Length != 0)
        _camera.ToList().ForEach(c => c.gameObject.SetActive(false));
    }

    public void ChangeTimeSlider(float value)
    {
        _timeSlider.value = value;
    }

    public void ChangeTimeText(string time)
    {
        _timeText.text = time;
    }

    public void ChangeScoreText(string score)
    {
        _scoreText.text = score;
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
        _camera[cameraNumber - 1].gameObject.SetActive(true);
    }
}
