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
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(int type, int num)
    {
        switch (type)
        {
            case 0:
                _audioSource.clip = _BGM[num];
                _audioSource.Play();
                break;
            case 1:
                _audioSource.PlayOneShot(_SE[num]);
                break;
        }
    }
}
