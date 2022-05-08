using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed;
    private Vector3 _movement;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        _movement = direction;
    }

    private void FixedUpdate()
    {
        MoveEnemy(_movement);
    }

    void MoveEnemy(Vector3 direction)
    {
        _rigidbody.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
