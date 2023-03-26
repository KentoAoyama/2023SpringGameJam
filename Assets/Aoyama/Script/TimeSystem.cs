using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeSystem
{
    [SerializeField]
    private float _morningTime = 20f;

    [SerializeField]
    private float _noonTime = 20f;

    [SerializeField]
    private float _nightTime = 20f;

    [SerializeField]
    private float _finishInterval = 3f;

    [Header("デバッグ用")]
    [SerializeField]
    private float _time = 0f;

    [SerializeField]
    private float _currentAllTime = 0f; 

    /// <summary>
    /// 朝の時間のプロパティ
    /// </summary>
    public float MorningTime => _morningTime;

    /// <summary>
    /// 昼の時間のプロパティ
    /// </summary>
    public float NoonTime => _noonTime;

    /// <summary>
    /// 夜の時間のプロパティ
    /// </summary>
    public float NightTime => _nightTime;

    /// <summary>
    /// ゲーム終了後にリザルトに飛ぶまでの時間
    /// </summary>
    public float FinishInterval => _finishInterval;

    /// <summary>
    /// ゲームの合計時間のプロパティ
    /// </summary>
    public float AllTime => _morningTime + _noonTime + _nightTime;

    /// <summary>
    /// 時間帯ごとの経過時間のプロパティ
    /// </summary>
    public float CurrentTime => _time;

    /// <summary>
    /// ゲームの経過時間のプロパティ
    /// </summary>
    public float CurrentAllTime => _currentAllTime;

    public void ResetTime()
    {
        _time = 0f;
    }

    public void AddTime()
    {
        var deltaTime = Time.deltaTime;

        _time += deltaTime;
        _currentAllTime += deltaTime;
    }
}
