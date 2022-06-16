using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn();
    void DespawnObject();
}
