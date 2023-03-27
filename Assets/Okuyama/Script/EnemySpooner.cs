using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class GimmickPrefabs
{
    [Tooltip("時間ごとの重さ"), SerializeField] public float[] _enemyPopWeight;
}
public class EnemySpooner : MonoBehaviour
{
    [SerializeField, Tooltip("GameManager")]
    private GameManager _gameManager = null;
    [SerializeField, Tooltip("Enemy生成タイミング")]
    private float _time = 0.5f;
    [SerializeField, Tooltip("エネミー")]
    private GameObject[] _enemys = null;
    [SerializeField, Header("上から朝から夜\n各敵の出現確率\n上からEnemysに入れた順")]
    private GimmickPrefabs[] _sceneWeight = default;
    [SerializeField, Tooltip("生成場所")]
    private GameObject[] _enemyPos = null;
    [SerializeField, Header("時間帯ごとの総数")]
    private int [] _timeEnemyTotal = default;
    [SerializeField]
    private int _addScore = 1;

    private float _totalWeight = 0f;
    private int _enemyCount = 0;//現在の敵の数
    private int _enemyTotal = 10;//総数
    private int _subtraction = -1;
    private int _nowPos = 0;

    public int EnemyCount { get => _enemyCount; set => _enemyCount = value; }
    public int Subtraction { get => _subtraction; set => _subtraction = value; }

    void Start()
    {
        Weight();
        InvokeRepeating("EnemyRandm", 0, _time);
        Debug.Log($"{_sceneWeight[_nowPos]._enemyPopWeight[_nowPos]}");
    }

    
    void Update()
    {
        switch (_gameManager.State)
        {
            case GameManager.InGameState.InGame_Morning:
                _enemyTotal = _timeEnemyTotal[0];
                _nowPos = 0;
                Weight();
                break;
            case GameManager.InGameState.InGame_Noon:
                _enemyTotal = _timeEnemyTotal[1];
                _nowPos = 1;
                Weight();
                break;
            case GameManager.InGameState.InGame_Night:
                _enemyTotal = _timeEnemyTotal[2];
                _nowPos = 2;
                Weight();
                break;
        }
        
    }

    /// <summary>
    /// 初期重さの再計算
    /// </summary>
    private void Weight()
    {
        _totalWeight = 0;
        for (var i = 0; i < _sceneWeight[_nowPos]._enemyPopWeight.Length; i++)
        {
            _totalWeight += _sceneWeight[_nowPos]._enemyPopWeight[i];
        }
    }


    /// <summary>
    /// Enemy生成
    /// </summary>
    private void EnemyRandm()
    {
        if (EnemyCount >= _enemyTotal) { return; }
        var index = UnityEngine.Random.Range(0, _enemyPos.Length);
        var num = PramProbability();
        Debug.Log($"num:{num},index:{index}");
        if(num == _enemys.Length)
        {
            Instantiate(_enemys[num]);
            return;
        }
        EnemyPop(num, index);
    }

    private void EnemyPop(int num, int index)
    {
        var obj = Instantiate(_enemys[num], _enemyPos[index].transform);
        obj.GetComponent<Enemy>().EnemySpooner = this;
        obj.GetComponent<Enemy>().GameManager = _gameManager;
        obj.GetComponent<Enemy>().AddScore = _addScore;
        _enemyCount++;
    }

    /// <summary>
    /// 重さ計算
    /// </summary>
    int PramProbability()
    {
        Debug.Log($"total{_totalWeight}");
        var randomPoint = UnityEngine.Random.Range(0, _totalWeight);

        // 乱数値が属する要素を先頭から順に選択
        var currentWeight = 0f;
        for (var i = 0; i < _sceneWeight[_nowPos]._enemyPopWeight.Length; i++)
        {
            // 現在要素までの重みの総和を求める
            currentWeight += _sceneWeight[_nowPos]._enemyPopWeight[i];

            // 乱数値が現在要素の範囲内かチェック
            if (randomPoint < currentWeight)
            {
                return i;
            }
        }
        // 乱数値が重みの総和以上なら末尾要素とする
        return _sceneWeight.Length - 1;
    }
}
