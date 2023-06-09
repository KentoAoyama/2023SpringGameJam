using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField, Tooltip("BGM")]
    private AudioClip[] _BGM;
    [SerializeField, Tooltip("SE")]
    private AudioClip[] _SE;

    private AudioSource _audioSource;
    public static SoundManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        GetComponent<AudioSource>();
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        Play(0, 0);
    }

    public void Play(int type, int num)
    {
        switch (type)
        {
            case 0:
                _audioSource.clip = _BGM[num];
                _audioSource.Play();
                _audioSource.loop = true;
                break;
            case 1:
                _audioSource.PlayOneShot(_SE[num]);
                break;
        }
    }
}
