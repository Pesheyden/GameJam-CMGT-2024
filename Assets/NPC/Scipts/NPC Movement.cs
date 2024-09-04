using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _xMovementBorders;
    [SerializeField] private float _timeBetweenMovement;
    private bool _canMove;
    private Vector3 _startPosition;

    private void Awake()
    {
        _canMove = true;
    }

    private void Start()
    {
        StartMovement();
    }

    private IEnumerator MovementCoroutine()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(Random.Range(_startPosition.x + _xMovementBorders.x, _startPosition.x + _xMovementBorders.y), _startPosition.y, _startPosition.z);
        float distance = Vector3.Distance(startPosition, endPosition);
        float movementTime = distance / _speed;
        float timeElapsed = 0;
        while (timeElapsed < movementTime)
        {
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, timeElapsed / movementTime);
            transform.position = newPosition;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_timeBetweenMovement);
        if (_canMove)
            StartCoroutine(MovementCoroutine());
    }

    public void StopMovement()
    {
        _canMove = false;
        StartCoroutine(MovementCoroutine());
    }

    public void StartMovement()
    {
        _startPosition = transform.position;
        _canMove = true;
        StartCoroutine(MovementCoroutine());
    }
}
