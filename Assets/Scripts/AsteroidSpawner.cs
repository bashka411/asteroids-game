using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private float _spawnRate = 2.0f;
    private float _spawnDistance = 15.0f;
    public float _avgSpeed = 1.5f;
    public float _speedVariance = 0.5f;

    private void Start()
    {
        Invoke(nameof(BeginSpawning), 1.0f);
    }

    private void Spawn()
    {
        Vector3 spawnPosition = Random.insideUnitCircle.normalized * _spawnDistance;
        Vector3 spawnPoint = transform.position + spawnPosition;

        ObjectPooler.Instance.SpawnFromPool("Asteroid", spawnPoint, new Quaternion());
    }

    private void BeginSpawning()
    {
        StartCoroutine(RepeatingSpawng());
        StartCoroutine(GrowingDifficulty());
        StartCoroutine(IncreasingSpeed());
    }

    private IEnumerator IncreasingSpeed()
    {
        yield return new WaitForSeconds(20);
        while (true) {
            _avgSpeed += 0.2f;
            Debug.Log($"Average speed: {_avgSpeed}");
            yield return new WaitForSeconds(10);
        }
    }

    private IEnumerator GrowingDifficulty()
    {
        yield return new WaitForSeconds(15);
        while (_spawnRate > 1f) {
            _spawnRate -= 0.2f;
            Debug.Log($"Spawn rate: {_spawnRate}");
            yield return new WaitForSeconds(15);
        }
        Debug.Log($"Spawn rate increasement ended. Constant rate is {_spawnRate} ms");
    }

    private IEnumerator RepeatingSpawng()
    {
        while (true) {
            Spawn();
            yield return new WaitForSeconds(_spawnRate);
        }
    }
}