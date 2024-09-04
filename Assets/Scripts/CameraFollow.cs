using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offSet;
    [SerializeField] private Vector4 _borders;
    [SerializeField] private float _speed;

    private void Update()
    {
        Vector3 targetPosition = _target.position + _offSet;

        if (targetPosition.x > _borders.z)
            targetPosition.x = _borders.z;
        if (targetPosition.x < _borders.x)
            targetPosition.x = _borders.x;
        
        if (targetPosition.y > _borders.w)
            targetPosition.y = _borders.w;
        if (targetPosition.y < _borders.y)
            targetPosition.y = _borders.y;
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed);
    }
}
