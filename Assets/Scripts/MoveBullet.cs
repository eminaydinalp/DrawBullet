using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public bool isMove;
    [SerializeField] private Transform firstPosition;
    [SerializeField] private float objectSpeed;
    private Vector3 _nextPos;
    private int _nextPosIndex;

    [SerializeField] private DrawLine drawLine;
    
    private void OnEnable()
    {
        _nextPos = firstPosition.position;
    }
    private void Update()
    {
        if(isMove) Move();
    }

    void Move()
    {
        if (transform.position == _nextPos)
        {
            _nextPosIndex++;
            if (_nextPosIndex >= drawLine.fingerPosition.Count)
            {
                _nextPosIndex = 0;
                isMove = false;
                gameObject.SetActive(false);
                transform.position = firstPosition.position;
            }
            _nextPos = new Vector3(drawLine.fingerPosition[_nextPosIndex].x,
                firstPosition.position.y, drawLine.fingerPosition[_nextPosIndex].z);
        }
        else
        {
            transform.position = 
                Vector3.MoveTowards(transform.position, _nextPos, objectSpeed * Time.deltaTime);
        }
    }
}
