using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Has all the coding to forward the trigger detections to the game manager.
/// </summary>
public sealed class MovementDetection
{
    public GameManager gameManager { get; set; }
    public GameObject Kinect;

    private BodySourceViewer bsv;

    private static MovementDetection _instance = null;
    
    private MovementDetection() { }

    public static MovementDetection Instance()
    {
        if (_instance == null) _instance = new MovementDetection();
        return _instance;
    }

    public void notifyEvent(Collider collidingObject, GameObject gameObject)
    {
        switch (collidingObject.tag)
        {
            case "FootColliderTop":
                if(gameObject.CompareTag("FootAvatar")) gameManager.Pedal();
                break;
            case "FootColliderBottom":
                if (gameObject.CompareTag("FootAvatar")) gameManager.Pedal();
                break;
            case "LightTrigger":
                if (gameObject.CompareTag("HandAvatar")) gameManager.LightTriggered();
                break;
        }
    }
    
}
