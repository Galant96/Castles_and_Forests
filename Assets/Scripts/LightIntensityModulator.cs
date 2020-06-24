using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightIntensityModulator : MonoBehaviour
{
	[SerializeField]
	private Light2D light2D;

	[SerializeField]
	private float lightSpeed = 0.75f; // Set how fast the light will change

	[SerializeField]
	private float minIntensity = 0.75f;

	[SerializeField]
	private float maxIntensity = 1.2f;

	[SerializeField]
	private float intensity = 1f;

	private bool isIntensityGrowing = true;

    // Start is called before the first frame update
    void Start()
    {
		light2D = GetComponentInChildren<Light2D>();

		// Prevent setting intensity out of the range
		if (intensity < minIntensity)
		{
			intensity = minIntensity;
		}
		else if (intensity > minIntensity)
		{
			intensity = maxIntensity;
		}

		light2D.intensity = intensity;
	}

	// Update is called once per frame
	void Update()
    {
		// Check if ntensity reaches its ighest value
		if (light2D.intensity >= maxIntensity)
		{
			isIntensityGrowing = false;
		}

		if (light2D.intensity <= minIntensity)
		{
			isIntensityGrowing = true;
		}

		if (isIntensityGrowing != true)
		{
			light2D.intensity -= lightSpeed * Time.deltaTime;
		}
		else
		{
			light2D.intensity += lightSpeed * Time.deltaTime;
		}

	}
}
