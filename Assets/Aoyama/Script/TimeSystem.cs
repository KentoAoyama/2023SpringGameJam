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

    [Header("デバッグ用")]
    [SerializeField]
    private float _time = 0f;

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

    public void Initialize()
    {
        _time = 0f;
    }

    public void AddTime()
    {
        _time += Time.deltaTime;


    }
}
