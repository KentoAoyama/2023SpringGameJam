using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpooner : MonoBehaviour
{
    [SerializeField, Tooltip("GameManager")]
    private GameManager _gameManager = null;
    [SerializeField, Tooltip("Enemy生成タイミング")]
    private float _time = 0.5f;
    [SerializeField, Tooltip("エネミー")]
    private GameObject[] _enemys = null;
    [SerializeField, Header("各敵の出現確率\n上からEnemysに入れた順"), Range(0f, 100f)]
    private float[] _enemyPopWeight = default;
    [SerializeField, Tooltip("生成場所")]
    private GameObject[] _enemyPos = null;
    [SerializeField, Header("時間帯ごとの総数")]
    private int [] _timeEnemyTotal = default;

    private float _totalWeight = 0f;
    private int _enemyCount = 0;//現在の敵の数
    private int _enemyTotal = 10;//総数

    public int EnemyCount { get => _enemyCount; set => _enemyCount = value; }

    void Start()
    {
        for (var i = 0; i < _enemyPopWeight.Length; i++)
        {
            _totalWeight += _enemyPopWeight[i];
        }
        InvokeRepeating("EnemyPop", 0, _time);
    }

    
    void Update()
    {
        switch (_gameManager.State)
        {
            case GameManager.InGameState.InGame_Morning:
                _enemyTotal = _timeEnemyTotal[0];
                break;
            case GameManager.InGameState.InGame_Noon:
                _enemyTotal = _timeEnemyTotal[1];
                break;
            case GameManager.InGameState.InGame_Night:
                _enemyTotal = _timeEnemyTotal[2];
                break;
        }
        
    }

    /// <summary>
    /// Enemy生成
    /// </summary>
    private void EnemyPop()
    {
        if (EnemyCount >= _enemyTotal) { return; }
        var index = Random.Range(0, _enemyPos.Length);
        var obj = Instantiate(_enemys[PramProbability()], _enemyPos[index].transform);
        obj.GetComponent<Enemy>().EnemySpooner = this;
        _enemyCount++;
    }

    /// <summary>
    /// 重さ計算
    /// </summary>
    int PramProbability()
    {
        var randomPoint = Random.Range(0, _totalWeight);

        // 乱数値が属する要素を先頭から順に選択
        var currentWeight = 0f;
        for (var i = 0; i < _enemyPopWeight.Length; i++)
        {
            // 現在要素までの重みの総和を求める
            currentWeight += _enemyPopWeight[i];

            // 乱数値が現在要素の範囲内かチェック
            if (randomPoint < currentWeight)
            {
                return i;
            }
        }
        // 乱数値が重みの総和以上なら末尾要素とする
        return _enemyPopWeight.Length - 1;
    }
}
