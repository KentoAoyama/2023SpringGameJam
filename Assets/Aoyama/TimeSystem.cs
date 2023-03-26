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

    /// <summary>
    /// ���̎��Ԃ̃v���p�e�B
    /// </summary>
    public float MorningTime => _morningTime;

    /// <summary>
    /// ���̎��Ԃ̃v���p�e�B
    /// </summary>
    public float NoonTime => _noonTime;

    /// <summary>
    /// ��̎��Ԃ̃v���p�e�B
    /// </summary>
    public float NightTime => _nightTime;

    private float _time = 0f;

}
