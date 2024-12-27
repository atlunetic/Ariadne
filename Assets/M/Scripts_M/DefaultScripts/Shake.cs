using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay = 0.2f;
	public float shake_intensity = 0.3f;

	private float temp_shake_intensity = 0;

	void Start()
    {
		Shakes();
    }

	void Update()
	{
		if (temp_shake_intensity > 0)
		{
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .02f,
				originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .02f,
				originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .02f,
				originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .02f);
			temp_shake_intensity -= shake_decay;
		}
	}

	void Shakes()
	{
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;

	}
}
