using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [SerializeField] private DrawLine drawLine;
    [SerializeField] private Transform firstPosition;
    [SerializeField] private float objectSpeed;
    
    private Rigidbody _rigidbody;
    private Vector3 _nextPos;

    public bool isMove;
    private int _nextPosIndex;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _nextPos = firstPosition.position;
    }

    private void FixedUpdate()
    {
        if (isMove) Move();
    }

    void Move()
    {
        _nextPosIndex++;
        if (_nextPosIndex >= drawLine.fingerPosition.Count)
        {
            _nextPosIndex = 0;
            isMove = false;
            _rigidbody.velocity = Vector3.zero;
            gameObject.SetActive(false);
            transform.position = firstPosition.position;
        }

        _nextPos = new Vector3(drawLine.fingerPosition[_nextPosIndex].x,
            firstPosition.position.y, drawLine.fingerPosition[_nextPosIndex].z);

        Vector3 direction = _nextPos - transform.position;
        
        direction.Normalize();

        _rigidbody.AddForce(direction * (objectSpeed * Time.deltaTime), ForceMode.Impulse);
    }
    
}