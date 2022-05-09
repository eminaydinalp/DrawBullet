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
    private Vector3 _lastPos;

    public bool isMove;
    private int _nextPosIndex;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        transform.position = firstPosition.position;
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
            StartCoroutine(FinishMove());
            return;
        }
        
        _lastPos = _nextPos;

        _nextPos = new Vector3(drawLine.fingerPosition[_nextPosIndex].x,
            firstPosition.position.y, drawLine.fingerPosition[_nextPosIndex].z);

        if(!(_nextPos.magnitude > _lastPos.magnitude)) return;
        
        Vector3 direction = _nextPos - transform.position;
        
        direction.Normalize();

        _rigidbody.AddForce(direction * (objectSpeed * Time.deltaTime), ForceMode.Impulse);
    }

    IEnumerator FinishMove()
    {
        yield return new WaitForSeconds(0.9f);
        _rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
        transform.position = firstPosition.position;
    }
    
}