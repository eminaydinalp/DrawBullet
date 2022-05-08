using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private float force;
    [SerializeField] private float radius;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Random.InitState(System.DateTime.Now.Millisecond * (int)transform.position.z);
            _rigidbody.AddExplosionForce(force, transform.position, radius);
            _rigidbody.AddForce(new Vector3(Random.Range(-20, 20), Random.Range(-10, 20), Random.Range(0, 10)) * force,
                ForceMode.Impulse);
        }
    }
}
