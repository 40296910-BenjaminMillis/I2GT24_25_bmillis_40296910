using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] AnimationCurve curve;

    public IEnumerator ScreenShake(){
        Vector3 startPosition = transform.localPosition;
        float shakeTime = 0f;

        while(shakeTime < shakeDuration){
            shakeTime += Time.deltaTime;
            float strength = curve.Evaluate(shakeTime / shakeDuration);
            transform.localPosition = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.localPosition = startPosition;
    }
}
