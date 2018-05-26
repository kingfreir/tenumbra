using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetection : MonoBehaviour {

    private MovementDetection _MD;
    

	// Use this for initialization
	void Start ()
    {
        _MD = MovementDetection.Instance();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        _MD.notifyEvent(other, this.gameObject);
    }
}
