using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreSystem
{
    private static float _score = 0f;
    public float Score => _score;


    public void Initialize()
    {
        _score = 0f;
    }

    public void AddScore(int score)
    {
        _score += score;

        Debug.Log("score" + _score);
    }
}
