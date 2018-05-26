using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FireflyCollider : MonoBehaviour {

    public GameObject objectColliding { get; set; }
    public bool isFireflyColliding { get; set; }
    public bool isObstacleColliding { get; set; }
    public Material fireflyMaterial, obstacleMaterial;

    private Stopwatch stopWatch;
    private bool isFireflyCatchEnabled = true;

	// Use this for initialization
	void Start ()
    {
        objectColliding = null;
        isFireflyColliding = false;
        isObstacleColliding = false;

        ChangeToFirefly();

        stopWatch = new Stopwatch();
        stopWatch.Reset();
        stopWatch.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LeftHandVR") || other.CompareTag("RightHandVR"))
        {
            if (stopWatch.ElapsedMilliseconds < 1000) return;
            else if (isFireflyCatchEnabled) ChangeToObstacle();
            else if (!isFireflyCatchEnabled) ChangeToFirefly();
            stopWatch.Stop();
            stopWatch.Reset();
            stopWatch.Start();
        }
        else if(other.CompareTag("Firefly") && isFireflyCatchEnabled)
        {
            objectColliding = other.gameObject;
            isFireflyColliding = true;
        }
        else if(other.CompareTag("ObstacleTrigger") && !isFireflyCatchEnabled)
        {
            objectColliding = other.gameObject;
            isObstacleColliding = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        objectColliding = null;
        if (other.CompareTag("LeftHandVR") || other.CompareTag("RightHandVR")) return;
        else if (other.CompareTag("Firefly")) isFireflyColliding = false;
        else if (other.CompareTag("ObstacleTrigger")) isObstacleColliding = false;
    }

    /// <summary>
    /// 
    /// </summary>
    private void ChangeToObstacle()
    {
        gameObject.GetComponent<Renderer>().material = obstacleMaterial;
        isFireflyCatchEnabled = false;
    }

    /// <summary>
    /// 
    /// </summary>
    private void ChangeToFirefly()
    {
        gameObject.GetComponent<Renderer>().material = fireflyMaterial;
        isFireflyCatchEnabled = true;
    }
}
