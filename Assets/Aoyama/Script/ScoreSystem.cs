using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreSystem
{
    private  int _score = 0;
    public int Score => _score;


    public void Initialize()
    {
        _score = 0;
    }

    public void AddScore(int score)
    {
        _score += score;

        Debug.Log("score" + _score);
    }
}
