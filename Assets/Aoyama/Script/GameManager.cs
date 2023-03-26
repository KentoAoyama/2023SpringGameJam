using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum InGameState
    {
        Title,
        InGame_Morning,
        InGame_Noon,
        InGame_Night,
        Finish,
    }

    [SerializeField]
    private TimeSystem _timer;

    [SerializeField]
    private ScoreSystem _score;

    [SerializeField]
    private FadeSystem _fade;

    [SerializeField]
    private UIController _uiController;

    /// <summary>
    /// ���Ԃ̊Ǘ�������N���X
    /// </summary>
    public TimeSystem Timer => _timer;

    private InGameState _state;

    /// <summary>
    /// �Q�[���̏�Ԃ�\���񋓌^
    /// </summary>
    public InGameState State => _state;

    /// <summary>
    /// ���݂��Q�[�����ł��邩��\��bool
    /// </summary>
    private bool _isInGame => 
        _state == InGameState.InGame_Morning 
        || _state == InGameState.InGame_Noon 
        || _state == InGameState.InGame_Night;


    void Start()
    {
        //Debug�p��State��ύX�B��ŏ���
        _state = InGameState.InGame_Morning;

        _score.Initialize();
        if (_fade) _fade.StartFadeIn();
    }

    void Update()
    {
        UpdateUI();

        if (_isInGame)
        {
            _timer.AddTime();
        }

        switch(_state)
        {
            case InGameState.Title:

                break;
            case InGameState.InGame_Morning: //���̎��Ԃɍs������

                if (_timer.CurrentTime > _timer.MorningTime)
                {
                    _timer.ResetTime();
                    ChangeState(InGameState.InGame_Noon);
                }

                break;
            case InGameState.InGame_Noon: //���̎��Ԃɍs������

                if (_timer.CurrentTime > _timer.NoonTime)
                {
                    _timer.ResetTime();
                    ChangeState(InGameState.InGame_Night);
                }

                break;
            case InGameState.InGame_Night: //��̎��Ԃɍs������

                if (_timer.CurrentTime > _timer.NightTime)
                {
                    _timer.ResetTime();
                    ChangeState(InGameState.Finish);
                }

                break;
        }
    }

    private void UpdateUI()
    {
        _uiController.ChangeTimeSlider(
            _timer.CurrentAllTime / _timer.AllTime);

        _uiController.ChangeTimeText(
            (_timer.CurrentAllTime * (24 / _timer.AllTime))
            .ToString());

        _uiController.ChangeScoreText(
            _score.Score.ToString());
    }

    private void ChangeState(InGameState state)
    {
        _state = state;
        Debug.Log("InGameState��" + state + "�ɕύX����܂���");
    }

    public void AddScore(int score = 1)
    {
        _score.AddScore(score);
    }

    /// <summary>
    /// �J������UI��ω������郁�\�b�h
    /// </summary>
    /// <param name="cameraNumber">�ύX����J�����̔z��ԍ�</param>
    public void ChangeCameraUI(int cameraNum)
    {
        _uiController.ChangeCameraUINum(cameraNum);
    }
}
