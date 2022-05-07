using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isMove;
    private DrawLine _drawLine;
    [SerializeField] private Transform firstPosition;
    [SerializeField] private float bulletSpeed;
    private Vector3 _nextPos;
    public int _nextPosIndex;
    

    private void Awake()
    {
        _drawLine = FindObjectOfType<DrawLine>();
    }

    private void Update()
    {
        if(isMove) MoveBullet();
    }

    public void MoveBullet()
    {
        _nextPos = firstPosition.position;

        if (Vector3.Distance(transform.position, _nextPos) < 1f)
        {
            _nextPosIndex++;
            if (_nextPosIndex >= _drawLine.fingerPosition.Count)
            {
                isMove = false;
                _nextPosIndex = 0;
                Destroy(gameObject);
            }
            _nextPos = _drawLine.fingerPosition[_nextPosIndex];
        }
        else
        {
            Debug.Log(transform.position.magnitude);
            Debug.Log(_nextPos.magnitude);
            Debug.Log("distance : " + Vector3.Distance(transform.position, _nextPos));
            transform.position =
                Vector3.MoveTowards(transform.position, _nextPos, bulletSpeed * Time.deltaTime);
        }
    }
}
