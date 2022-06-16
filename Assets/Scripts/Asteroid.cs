using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IPooledObject
{
    public Sprite[] sprites;

    private float _size;
    private float _minSize = .5f;
    private float _maxSize = 1.5f;
    private float _trajectoryVariance = 3.0f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] AsteroidSpawner _asteroidSpawner;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
    }

    private void Start()
    {
        _size = Random.Range(_minSize, _maxSize);
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * _size;
    }

    public void OnObjectSpawn()
    {
        Vector3 target = Player.playerPosition;

        float _trajectoryOffset = Random.Range(-_trajectoryVariance, _trajectoryVariance);
        float _speed = Random.Range(_asteroidSpawner._avgSpeed - _asteroidSpawner._speedVariance, _asteroidSpawner._avgSpeed + _asteroidSpawner._speedVariance);

        transform.right = new Vector3(target.x + _trajectoryOffset, target.y + _trajectoryOffset) - transform.position;
        _rigidbody2D.velocity = transform.rotation * Vector3.right * _speed;
        _rigidbody2D.angularVelocity = Random.Range(-40.0f, 40.0f);
        transform.rotation = new Quaternion(0, 0, transform.rotation.z, 0);
    }

    public void DespawnObject()
    {
        _rigidbody2D.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Bullet(Clone)") {
            EventManager.NotifyAsteroidExploded(gameObject.transform);
            CinemachineShake.Instance.ShakeCamera(transform.localScale.x, transform.localScale.x);
            DespawnObject();
        }
    }
}
