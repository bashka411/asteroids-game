using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    private Rigidbody2D _rigidbody2D;
    private float _speed = 20.0f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void OnObjectSpawn()
    {
        _rigidbody2D.velocity = transform.rotation * Vector3.up * _speed;
    }

    public void DespawnObject()
    {
        _rigidbody2D.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player") {
            DespawnObject();
        }
    }
}