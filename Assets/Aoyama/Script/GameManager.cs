using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public enum InGameState
    {
        WaitStart,
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

    [SerializeField]
    private CinemachineVirtualCamera _cinemachine;

    [SerializeField]
    private string _finishText = "�I���I";

    /// <summary>
    /// ���Ԃ̊Ǘ�������N���X
    /// </summary>
    public TimeSystem Timer => _timer;

    /// <summary>
    /// �X�R�A�̊Ǘ�������N���X
    /// </summary>
    public ScoreSystem Score => _score;

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
        ChangeState(InGameState.WaitStart);
        Initialize();
    }

    private void Initialize()
    {
        _score.Initialize();
        _uiController.Initialize();
        if (_fade) _fade.StartFadeIn();

        StartCoroutine(SceneStart());
    }

    void Update()
    {
        if (_isInGame)
        {
            _timer.AddTime();
            UpdateGUI();
        }

        switch(_state)
        {
            case InGameState.Title:
                if (Input.GetMouseButtonDown(1)) //�{�^������������Q�[���J�n�i���j
                {
                    StartCoroutine(GameStart());
                }

                break;
            case InGameState.InGame_Morning: //���̎��Ԃɍs������

                if (_timer.CurrentTime > _timer.MorningTime)
                {
                    ChangeState(InGameState.InGame_Noon);
                }

                break;
            case InGameState.InGame_Noon: //���̎��Ԃɍs������

                if (_timer.CurrentTime > _timer.NoonTime)
                {
                    ChangeState(InGameState.InGame_Night);
                }

                break;
            case InGameState.InGame_Night: //��̎��Ԃɍs������

                if (_timer.CurrentTime > _timer.NightTime)
                {
                    ChangeState(InGameState.Finish);
                }

                break;
            case InGameState.Finish:
                StartCoroutine(GameOver());
                
                break;
        }
    }

    /// <summary>
    /// InGameScene���J�n���ꂽ�ۂɎ��s���鏈��
    /// </summary>
    private IEnumerator SceneStart()
    {
        //Start�̉��o���I�������Q�[���J�n�\�ɂ���
        yield return _uiController.SceneStart();

        ChangeState(InGameState.Title);
    }

    /// <summary>
    /// Game��Start����ۂɎ��s���鏈��
    /// </summary>
    private IEnumerator GameStart()
    {
        _cinemachine.Priority = -1;

        yield return _uiController.GameStart();

        ChangeState(InGameState.InGame_Morning);
    }

    private IEnumerator GameOver()
    {
        _uiController.Finish(_finishText);

        yield return new WaitForSeconds(_timer.FinishInterval);

        _fade.StartFadeOut("");
    }

    /// <summary>
    /// Update�Ŏ��s����UI�̏���
    /// </summary>
    private void UpdateGUI()
    {
        if (!_uiController) return;

        _uiController.ChangeTimeSlider(
            _timer.CurrentAllTime / _timer.AllTime);

        if (_state != InGameState.Finish)
        {
            _uiController.ChangeTimeText(
                _timer.CurrentAllTime * (24 / _timer.AllTime));
        }
        else
        {
            _uiController.ChangeTimeText(24);
        }

        _uiController.ChangeScoreText();
    }

    private void ChangeState(InGameState state)
    {
        _timer.ResetTime();
        _state = state;
    }

    public void AddScore(int score = 1)
    {
        _score.AddScore(score);
        _uiController.ChangeScore(score);
    }

    /// <summary>
    /// �J������UI��ω������郁�\�b�h
    /// </summary>
    /// <param name="cameraNumber">�ύX����J�����̔z��ԍ�</param>
    public void ChangeCameraUI(int cameraNum)
    {
        _uiController.ChangeCameraUINum(cameraNum);
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
