using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class SunLightController
{
    [SerializeField]
    private Transform _lightTransform;

    [SerializeField]
    private float _finish = 190f;

    public void RotateLight(float interval)
    {
        _lightTransform.DORotate(new Vector3(_finish, -30, 0f), interval);
    }
}
