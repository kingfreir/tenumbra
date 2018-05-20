using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public Light _light;

	// Use this for initialization
	void Start ()
    {
        _light.intensity = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKey("space"))
        {
            _light.intensity = 1.5f;
        }
        else
        {
            _light.intensity = 0.0f;
        }
	}
}
