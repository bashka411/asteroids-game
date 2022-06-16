using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Movement

    private float _inputX;
    private float _inputY;
    private float _directSpeed = 3.0f;
    private float _rotationSpeed = 3.5f;
    private Quaternion _playerRotation;
    private Vector2 _normalizedRotation;
    private Rigidbody2D _playerRigidbody2D;
    public static Vector3 playerPosition;

    #endregion

    #region Game Logic

    private float _health = 50.0f;

    #endregion

    [SerializeField] FlashEffect flashEffect;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] ParticleSystem thrust;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Healthbar healthbar;

    private void Awake()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        healthbar.SetMaxHealth(_health);
    }

    private void Update()
    {
        _normalizedRotation = _playerRotation * Vector2.up;
        _playerRotation = transform.rotation;
        _inputX = Input.GetAxis("Horizontal");
        _inputY = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) {
            ObjectPooler.Instance.SpawnFromPool("Bullet", transform.position, transform.rotation);
            audioSource.pitch = Random.Range(0.9f, 1.2f);
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            thrust.Play();
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) {
            thrust.Stop();
        }
    }

    private void FixedUpdate()
    {
        _playerRigidbody2D.AddForce(_normalizedRotation * _inputY * _directSpeed);
        _playerRigidbody2D.rotation += -_inputX * _rotationSpeed;
        thrust.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        thrust.transform.rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w);

        playerPosition = transform.position;
    }

    private void Death()
    {
        Time.timeScale = 0.5f;
        gameObject.SetActive(false);
        healthbar.gameObject.SetActive(false);
        thrust.Stop();
        EventManager.NotifyAsteroidExploded(gameObject.transform);
        EventManager.NotifyGameover();
    }

    private void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0) Death();
        flashEffect.Flash(0.5f, 0.1f);
        healthbar.Damage(damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Asteroid(Clone)")
            TakeDamage(collision.gameObject.transform.localScale.x * 10.0f);
    }
}