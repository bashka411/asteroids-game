using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    private float _duration = 0.5f;
    private float _health;

    private void Start()
    {
        EventManager.OnPlayerDamage.AddListener(Damage);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        _health = health;
    }

    public void Damage(float damage)
    {
        StartCoroutine(DamageCoroutine(damage));
    }

    private IEnumerator DamageCoroutine(float damage)
    {
        float oldHealth = _health;
        _health -= damage;

        float timer = 0.0f;
        while (timer < _duration) {
            slider.value = Mathf.Lerp(oldHealth, _health, timer / _duration);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
