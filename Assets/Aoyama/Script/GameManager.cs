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

        InGame = InGame_Morning | InGame_Noon | InGame_Night
    }

    [SerializeField]
    private TimeSystem _timer;

    [SerializeField]
    private ScoreSystem _score;

    [SerializeField]
    private FadeSystem _fade;

    /// <summary>
    /// 時間の管理をするクラス
    /// </summary>
    public TimeSystem Timer => _timer;

    private InGameState _state;

    /// <summary>
    /// ゲームの状態を表す列挙型
    /// </summary>
    public InGameState State => _state;

    void Start()
    {
        _state = InGameState.Title;
        if (_fade) _fade.StartFadeIn();
    }

    void Update()
    {
        if (_state == InGameState.InGame)
        {
            _timer.AddTime();
        }

        switch(_state)
        {
            case InGameState.Title:

                break;
            case InGameState.InGame_Morning:

                break;
            case InGameState.InGame_Noon:

                break;
            case InGameState.InGame_Night:

                break;
        }
    }

    public void AddScore(int score = 1)
    {
        _score.AddScore(score);
    }
}
