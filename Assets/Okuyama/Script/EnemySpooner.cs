using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpooner : MonoBehaviour
{
    [SerializeField, Tooltip("GameManager")]
    private GameManager _gameManager = null;
    [SerializeField, Tooltip("Enemy�����^�C�~���O")]
    private float _time = 0.5f;
    [SerializeField, Tooltip("�G�l�~�[")]
    private GameObject[] _enemys = null;
    [SerializeField, Header("�e�G�̏o���m��\n�ォ��Enemys�ɓ��ꂽ��"), Range(0f, 100f)]
    private float[] _enemyPopWeight = default;
    [SerializeField, Tooltip("�����ꏊ")]
    private GameObject[] _enemyPos = null;
    [SerializeField, Header("���ԑт��Ƃ̑���")]
    private int [] _timeEnemyTotal = default;

    private float _totalWeight = 0f;
    private int _enemyCount = 0;//���݂̓G�̐�
    private int _enemyTotal = 10;//����

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
    /// Enemy����
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
    /// �d���v�Z
    /// </summary>
    int PramProbability()
    {
        var randomPoint = Random.Range(0, _totalWeight);

        // �����l��������v�f��擪���珇�ɑI��
        var currentWeight = 0f;
        for (var i = 0; i < _enemyPopWeight.Length; i++)
        {
            // ���ݗv�f�܂ł̏d�݂̑��a�����߂�
            currentWeight += _enemyPopWeight[i];

            // �����l�����ݗv�f�͈͓̔����`�F�b�N
            if (randomPoint < currentWeight)
            {
                return i;
            }
        }
        // �����l���d�݂̑��a�ȏ�Ȃ疖���v�f�Ƃ���
        return _enemyPopWeight.Length - 1;
    }
}
