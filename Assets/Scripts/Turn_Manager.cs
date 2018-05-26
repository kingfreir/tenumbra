using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Manager : MonoBehaviour {

    public GameObject playerObject;
    public float var;

    private GameObject spine_mid, shoulder_mid;
    private bool isTracked = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        FindFirstAvatar();
        UpdatePlayerSidePos();
    }

    private void UpdatePlayerSidePos()
    {
        if(isTracked)
        {
            float ChangeX = (spine_mid.transform.position.x - shoulder_mid.transform.position.x);

            if (ChangeX < 0.04f && ChangeX > -0.04f) return;

            ChangeX /= 2;

            playerObject.transform.position = new Vector3(
                playerObject.transform.position.x,
                playerObject.transform.position.y,
                playerObject.transform.position.z - ChangeX);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void FindFirstAvatar()
    {
        if (!isTracked)
        {
            shoulder_mid = GameObject.FindGameObjectWithTag("ShoulderBaseAvatar");
            spine_mid = GameObject.FindGameObjectWithTag("SpineBaseAvatar");
            
            if (shoulder_mid != null && spine_mid != null) isTracked = true;
        }
    }
}
