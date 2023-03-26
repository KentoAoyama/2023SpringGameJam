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

    [Header("�f�o�b�O�p")]
    [SerializeField]
    private float _time = 0f;

    [SerializeField]
    private float _currentAllTime = 0f; 

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

    /// <summary>
    /// �Q�[���I����Ƀ��U���g�ɔ�Ԃ܂ł̎���
    /// </summary>
    public float FinishInterval => _finishInterval;

    /// <summary>
    /// �Q�[���̍��v���Ԃ̃v���p�e�B
    /// </summary>
    public float AllTime => _morningTime + _noonTime + _nightTime;

    /// <summary>
    /// ���ԑт��Ƃ̌o�ߎ��Ԃ̃v���p�e�B
    /// </summary>
    public float CurrentTime => _time;

    /// <summary>
    /// �Q�[���̌o�ߎ��Ԃ̃v���p�e�B
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
