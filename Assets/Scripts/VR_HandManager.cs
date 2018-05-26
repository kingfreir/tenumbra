using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_HandManager : MonoBehaviour
{
    public GameObject CameraVR, HandVR_l, HandVR_r;
    public float filter;

    private bool isTracked = false;
    private GameObject handAvatar_l, handAvatar_r, headAvatar;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        FindFirstAvatar();
        UpdateHandPos();

    }

    private void UpdateHandPos()
    {
        if(isTracked)
        {
            Vector3 leftHand = new Vector3();
            Vector3 rightHand = new Vector3();

            //  Calculate the distance of the left hand to the head
            leftHand.x = headAvatar.transform.position.x - handAvatar_l.transform.position.x;
            leftHand.y = headAvatar.transform.position.y - handAvatar_l.transform.position.y;
            leftHand.z = headAvatar.transform.position.z - handAvatar_l.transform.position.z;

            //  Calculate the distance of the right hand to the head.
            rightHand.x = headAvatar.transform.position.x - handAvatar_r.transform.position.x;
            rightHand.y = headAvatar.transform.position.y - handAvatar_r.transform.position.y;
            rightHand.z = headAvatar.transform.position.z - handAvatar_r.transform.position.z;

            //  Calculate the new position of the hands on the VR World
            Vector3 newHandPos_l = new Vector3(
                CameraVR.transform.position.x + leftHand.z,
                CameraVR.transform.position.y - leftHand.y,
                CameraVR.transform.position.z - leftHand.x);
            Vector3 newHandPos_r = new Vector3(
                CameraVR.transform.position.x + rightHand.z,
                CameraVR.transform.position.y - rightHand.y,
                CameraVR.transform.position.z - rightHand.x);

            HandVR_l.transform.position = new Vector3(
                ((1 - filter) * newHandPos_l.x) + (filter * HandVR_l.transform.position.x),
                ((1 - filter) * newHandPos_l.y) + (filter * HandVR_l.transform.position.y),
                ((1 - filter) * newHandPos_l.z) + (filter * HandVR_l.transform.position.z));

            HandVR_r.transform.position = new Vector3(
                ((1 - filter) * newHandPos_r.x) + (filter * HandVR_r.transform.position.x),
                ((1 - filter) * newHandPos_r.y) + (filter * HandVR_r.transform.position.y),
                ((1 - filter) * newHandPos_r.z) + (filter * HandVR_r.transform.position.z));

            //CameraVR.transform.position = new Vector3(
            //    CameraVR.transform.position.x,
            //    C,
            //    CameraVR.transform.position.z);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void FindFirstAvatar()
    {
        if(!isTracked)
        {
            headAvatar = GameObject.FindGameObjectWithTag("HeadAvatar");
            handAvatar_l = GameObject.FindGameObjectWithTag("LeftHandAvatar");
            handAvatar_r = GameObject.FindGameObjectWithTag("RightHandAvatar");

            if(headAvatar != null && handAvatar_l != null && handAvatar_r != null) isTracked = true;
        }
    }
}
