using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour {

    public GameObject obstacleColliding { get; set; }
    public bool isColliding { get; set; }

    // Use this for initialization
    void Start()
    {
        obstacleColliding = null;
        isColliding = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ObstacleTrigger"))
        {
            obstacleColliding = other.gameObject;
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ObstacleTrigger"))
        {
            obstacleColliding = null;
            isColliding = false;
        }
    }
}
