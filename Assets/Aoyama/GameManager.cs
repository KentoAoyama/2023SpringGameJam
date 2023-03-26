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
    /// éûä‘ÇÃä«óùÇÇ∑ÇÈÉNÉâÉX
    /// </summary>
    public TimeSystem Timer => _timer;

    private InGameState _state;

    void Start()
    {
        _state = InGameState.Title;
        _fade.StartFadeIn();
    }
    
    void Update()
    {
        
    }

    public void AddScore(int score = 1)
    {

    }
}
