using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DisturbEnemy : MonoBehaviour
{
    [SerializeField]
    private Vector3 _movePos;

    [SerializeField]
    private RectTransform _transform;

    [SerializeField]
    private float _moveInterval = 1f;

    [SerializeField]
    private float _stayDuration = 5f;

    [SerializeField]
    private Ease _ease;

    private Vector3 _defaultPos;

    private void Start()
    {
        _defaultPos = 
            new Vector3(
                _transform.localPosition.x, 
                _transform.localPosition.y, 
                _transform.localPosition.z);

        var sequence = DOTween.Sequence();
        sequence
            .Insert(0f, _transform.DOLocalMove(_movePos, _moveInterval)).SetEase(_ease)
            .Insert(_stayDuration + _moveInterval, _transform.DOLocalMove(_defaultPos, _moveInterval)).SetEase(_ease)
            .Play()
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });       
    }
}
