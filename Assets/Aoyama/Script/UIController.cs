using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIController : MonoBehaviour
{
    [Header("Title�ɕ\���������")]
    [SerializeField]
    private Image _titleLogo;

    [SerializeField]
    private Text _titleText;

    [SerializeField]
    private Text _helpText;

    [Header("�C���Q�[���Ŏg�p�������")]
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

    [Header("�Q�[���I�����Ɏg�p�������")]
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
    /// �V�[�����s���̃^�C�g���ōs������
    /// </summary>
    public IEnumerator SceneStart()
    {
        _titleLogo.gameObject.SetActive(true);
        _titleText.gameObject.SetActive(true);
        _helpText.gameObject.SetActive(true);

        //���u���@DoTween�ƕ��p����\��
        yield return new WaitForSeconds(0f);
    }

    /// <summary>
    /// �Q�[���̊J�n���ɍs������
    /// </summary>
    public void GameStart()
    {
        //�����ɃJ������ύX���鏈��������



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
        _finishText.text = text;
    }
}
