using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField, Tooltip("�X�s�[�h")]
    private float _speed = 1.0f;
    [SerializeField, Tooltip("�����G�t�F�N�g")]
    private ParticleSystem _effect = null;
    [SerializeField, Tooltip("")]
    private float _dsPos = 20;
    private EnemySpooner _enemySpooner;

    public EnemySpooner EnemySpooner { get => _enemySpooner; set => _enemySpooner = value; }

    void Start()
    {
        _speed /= 100;
    }

    
    void Update()
    {
        transform.position += transform.forward * _speed;
        if(this.gameObject.transform.forward.z <= _dsPos)
        {
            EnemySpooner.EnemyCount--;
            gameObject.SetActive(false);
        }
        
    }
    public void EnemyBom()
    {
        _effect.Play();
        gameObject.SetActive(false); 
        EnemySpooner.EnemyCount--;
    }
}
