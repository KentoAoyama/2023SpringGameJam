using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField, Tooltip("�X�s�[�h")]
    private float _speed = 1.0f;
    [SerializeField, Tooltip("�����G�t�F�N�g")]
    private ParticleSystem _effect = null;
    [SerializeField, Tooltip("�����Ƃ���Pos")]
    private float _dsPos = -15;
    [SerializeField] float _bomForce = 20;
    [SerializeField] float _bomRadius = 1;
    [SerializeField] float _bomUpwards = 5;

    private EnemySpooner _enemySpooner = null;
    private GameManager _gameManager = null;
    private Rigidbody _rb = default;
    private int _subtraction = 0;
    private ParticleSystem _myPS = null;
    private int _addScore = 1;

    public EnemySpooner EnemySpooner { get => _enemySpooner; set => _enemySpooner = value; }
    public GameManager GameManager { get => _gameManager; set => _gameManager = value; }
    public int AddScore { get => _addScore; set => _addScore = value; }

    void Start()
    {
        _speed /= 100;
        _rb = GetComponent<Rigidbody>();
        _subtraction = _enemySpooner.Subtraction;
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
        _myPS = Instantiate(_effect,gameObject.transform);
        _myPS.gameObject.transform.parent = null;
        if (this.gameObject.CompareTag("DarkEnemy")) { GameManager.AddScore(AddScore); }
        else { GameManager.AddScore(_subtraction); }
        Explosion();
        EnemySpooner.EnemyCount--;
    }

    private void Explosion()
    {   
        gameObject.layer = 7;
        _myPS.Play();
        SoundManager.Instance.Play(1, 1);
        _rb.AddExplosionForce(_bomForce, gameObject.transform.position, _bomRadius, _bomUpwards, ForceMode.Impulse);
        StartCoroutine(destroy());
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        _myPS.gameObject.SetActive(false);
    }
}
