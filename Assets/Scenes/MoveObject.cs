using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public bool isMove;
    [SerializeField] private Vector3 firstPosition;
    [SerializeField] private float objectSpeed;
    private Vector3 _nextPos;
    private int _nextPosIndex;

    [SerializeField] private DrawLine drawLine;
    
    private void OnEnable()
    {
        _nextPos = firstPosition;
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
                transform.position = firstPosition;
            }
            _nextPos = new Vector3(drawLine.fingerPosition[_nextPosIndex].x,
                drawLine.fingerPosition[_nextPosIndex].y + 1f, drawLine.fingerPosition[_nextPosIndex].z);
        }
        else
        {
            transform.position = 
                Vector3.MoveTowards(transform.position, _nextPos, objectSpeed * Time.deltaTime);
        }
    }
}
