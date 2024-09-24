using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    Transform _target;
    Vector3 _initialPos;
    [Range(0f, 3f)]
    public float Intensity;

    // Start is called before the first frame update
    void Start()
    {
        _target = GetComponent<Transform>();
        _initialPos = _target.position;
    }

    float _pendingShakeDuration = 0f;

    public void Shake(float duration)
    {
        if (duration > 0){
            _pendingShakeDuration += duration;
        }
    }

    bool _isShaking = false;
    void Update()
    {
        if (_pendingShakeDuration > 0 && !_isShaking){
            StartCoroutine(DoShake());
        }
    }

    IEnumerator DoShake()
    {
        _isShaking = true;

        var startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + _pendingShakeDuration)
        {
            var randomPoint = new Vector3(Random.Range(8f, 9f)*Intensity, Random.Range(8f, 9f)*Intensity, _initialPos.z);
            _target.localPosition = randomPoint;
            yield return null;
        }
            _pendingShakeDuration = 0f;
            _isShaking = false;
            
    }

}
