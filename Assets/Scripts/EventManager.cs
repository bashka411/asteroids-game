using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent<Transform> OnAsteroidExploded = new UnityEvent<Transform>();
    public static UnityEvent OnGameover = new UnityEvent();
    public static UnityEvent<float> OnPlayerDamage = new UnityEvent<float>();

    public static void NotifyAsteroidExploded(Transform transform)
    {
        OnAsteroidExploded.Invoke(transform);
    }

    public static void NotifyGameover()
    {
        OnGameover.Invoke();
    }

    public static void NotifyPlayerDamage(float damage)
    {
        OnPlayerDamage.Invoke(damage);
    }
}
