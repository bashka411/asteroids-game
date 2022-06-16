using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidExplosionEvent : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        EventManager.OnAsteroidExploded.AddListener(ShowExplosion);
    }

    private void ShowExplosion(Transform transform)
    {
        gameObject.transform.position = transform.position;
        gameObject.transform.localScale = transform.localScale * 2;
        _particleSystem.Play();
        _audioSource.pitch = 3.0f - transform.localScale.x;
        _audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        _audioSource.Play();
    }
}