using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class GimmickPrefabs
{
    [Tooltip("���Ԃ��Ƃ̏d��"), SerializeField] public float[] _enemyPopWeight;
}
public class EnemySpooner : MonoBehaviour
{
    [SerializeField, Tooltip("GameManager")]
    private GameManager _gameManager = null;
    [SerializeField, Tooltip("Enemy�����^�C�~���O")]
    private float _time = 0.5f;
    [SerializeField, Tooltip("�G�l�~�[")]
    private GameObject[] _enemys = null;
    [SerializeField, Header("�ォ�璩�����\n�e�G�̏o���m��\n�ォ��Enemys�ɓ��ꂽ��")]
    private GimmickPrefabs[] _sceneWeight = default;
    [SerializeField, Tooltip("�����ꏊ")]
    private GameObject[] _enemyPos = null;
    [SerializeField, Header("���ԑт��Ƃ̑���")]
    private int [] _timeEnemyTotal = default;
    [SerializeField]
    private int _addScore = 1;

    private float _totalWeight = 0f;
    private int _enemyCount = 0;//���݂̓G�̐�
    private int _enemyTotal = 10;//����
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
    /// �����d���̍Čv�Z
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
    /// Enemy����
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
    /// �d���v�Z
    /// </summary>
    int PramProbability()
    {
        Debug.Log($"total{_totalWeight}");
        var randomPoint = UnityEngine.Random.Range(0, _totalWeight);

        // �����l��������v�f��擪���珇�ɑI��
        var currentWeight = 0f;
        for (var i = 0; i < _sceneWeight[_nowPos]._enemyPopWeight.Length; i++)
        {
            // ���ݗv�f�܂ł̏d�݂̑��a�����߂�
            currentWeight += _sceneWeight[_nowPos]._enemyPopWeight[i];

            // �����l�����ݗv�f�͈͓̔����`�F�b�N
            if (randomPoint < currentWeight)
            {
                return i;
            }
        }
        // �����l���d�݂̑��a�ȏ�Ȃ疖���v�f�Ƃ���
        return _sceneWeight.Length - 1;
    }
}
