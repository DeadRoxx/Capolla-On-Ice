using UnityEngine;
using System.Collections;

public class CameraShakeEffect : MonoBehaviour {

    public bool shake;
    public float shakeIntensityAmount = 1.0f;
    public float shakeDecayAmount = 0.2f;

    private float shakeIntensity;
    private float shakeDecay;

    Vector3 initialPosition;

	void Start () {
        shakeIntensity = shakeIntensityAmount;
        shakeDecay = shakeDecayAmount;
        initialPosition = transform.position;
	}
	
	void Update () {
        if (shake)
        {
            if (shakeIntensity > 0)
            {
                transform.position = initialPosition + Vector3.up * Random.Range(0.0f, shakeIntensity);
                 shakeIntensity -= shakeDecay;
            }
            else
            {
                shake = false;
                shakeIntensity = shakeIntensityAmount;
                shakeDecay = shakeDecayAmount;
                transform.position = initialPosition;
            }
        }
	}

    public void Shake()
    {
        shake = true;
    }

    public void Shake(float intensity, float decay)
    {
        shake = true;
        shakeIntensity = intensity;
        shakeDecay = decay;
    }
}
