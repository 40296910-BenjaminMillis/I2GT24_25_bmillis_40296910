using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f; // How long the screenshake effect lasts
    [SerializeField] AnimationCurve curve; // Curve affects strength of the shake over time

    // Make the screen shake to a set duration
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
