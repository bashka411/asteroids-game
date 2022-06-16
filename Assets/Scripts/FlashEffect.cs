using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private Material _flashMaterial;
    private Material _defaultMaterial;
    private Material _currentMaterial;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;   
        _currentMaterial = _defaultMaterial;
    }

    public void Flash(float duration, float frequency)
    {
        StartCoroutine(FlashCoroutine(duration, frequency));
    }

    private IEnumerator FlashCoroutine(float duration = 0.5f, float frequency = 0.1f)
    {
        float counter = duration;
        while (counter > 0) {
            _spriteRenderer.material = _flashMaterial;
            yield return new WaitForSeconds(frequency);
            counter -= frequency;
            _spriteRenderer.material = _defaultMaterial;
            yield return new WaitForSeconds(frequency);
            counter -= frequency;
        }
        _spriteRenderer.material = _defaultMaterial;
    }
}
