using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject playerObject, fireflyObject, obstacleObject;
    public Light flashLight;
    public Text livesText, firefliesText, pausedText;
    public float speedZ, speedY, speedX;
    public int gametime_MIN;
    public AudioSource fireflyCatch, obstacleHit, obstacleDamage;

    private MovementDetection _MD;
    private SpawnManager _SM;
    private bool isMoving, activateLight, activateObstacleAvoid, isPaused;
    private bool canStart = false;
    private Stopwatch stopWatch, gameTime, lightTimeout;
    private GameObject LFC, RFC;
    private FireflyCollider lfc, rfc;
    private ObstacleCollider player_oc;
    private int Lives, firefliesCatched;
    private LightControl lightController;


    private const int TIMEOUT = 1250, STARTING_LIVES = 3, MAX_LIVES = 5, NUM_FIREFLIES_TO_GIVE_LIFE = 5, HeartRate = 120;

    /// <summary>
    /// Game Manager Initialization
    /// </summary>
	void Start ()
    {
        //  Set the number of lives the player starts with
        Lives = STARTING_LIVES;

        //  Get a reference of the script that mediates the notifications 
        //  of collisions
        _MD = MovementDetection.Instance();
        _MD.gameManager = this;

        //  Spawn Manager
        SpawnManager.maxGameTime = (gametime_MIN * 60) * 1000;
        _SM = SpawnManager.Instance();
        _SM.fireflyObject = fireflyObject;
        _SM.obstacleObject = obstacleObject;
        _SM.Gametime = 0;

        isMoving = false;
        isPaused = false;
        activateLight = false;

        //  Obtain a reference of the game objects that make up the collision
        //  planes of the player in the world
        LFC = GameObject.FindGameObjectWithTag("LeftHandVR");
        RFC = GameObject.FindGameObjectWithTag("RightHandVR");

        //  Get the script instances attached to these game objects
        lfc = LFC.GetComponent<FireflyCollider>();
        rfc = RFC.GetComponent<FireflyCollider>();
        player_oc = playerObject.GetComponent<ObstacleCollider>();

        //  Start a new stopwatch to track the timeout the triggering of the 
        //  pause screen after a player stops pedalling 
        stopWatch = new Stopwatch();
        stopWatch.Start();
        gameTime = new Stopwatch();
        gameTime.Start();
        lightTimeout = new Stopwatch();
        lightTimeout.Start();

        //  Script that controls the light according to a heart rate value
        lightController = new LightControl(flashLight);

        //  Set the number of lives in the screen
        UpdateLivesText();

        canStart = true;
	}

    /// <summary>
    /// Updates the player position and does other actions in the world
    /// at a rate independent of the frame rate
    /// </summary>
    void FixedUpdate()
    {
        if (canStart)
        {
            // If player keeps pedalling then move forward otherwise stop
            if (stopWatch.ElapsedMilliseconds < TIMEOUT && isMoving)
            {
                ResumeGame();
                MoveForward();
            }
            else
            {
                isMoving = false;
                PauseGame();
            }

            ManageSpawns();

            //
            TurnLightOn();

            //
            CheckForHandActions();
            
            //
            DetectPlayerObstacleCollision();

        }
    }

    ///////////////////////////////////////////////////////////////////////////
    ///                                                                     ///
    ///             Private methods to make the mechanics work              ///
    ///                                                                     ///
    ///////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 
    /// </summary>
    private void ManageSpawns()
    {
        _SM.Gametime = (int)gameTime.ElapsedMilliseconds;
        _SM.manageSpawn(playerObject.transform.position.x);
    }

    /// <summary>
    /// 
    /// </summary>
    private void PauseGame()
    {
        isPaused = true;
        _SM.StopStopWatches();
        gameTime.Stop();
        pausedText.text = "PAUSED";
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResumeGame()
    {
        isPaused = false;
        _SM.StartStopWatches();
        gameTime.Start();
        pausedText.text = "";
    }

    /// <summary>
    /// Move the Player Forward 
    /// </summary>
    private void MoveForward()
    {
        playerObject.transform.position = new Vector3(
               playerObject.transform.position.x + speedX,
               playerObject.transform.position.y + speedY,
               playerObject.transform.position.z + speedZ);


    }

    /// <summary>
    /// Makes the light turn on if the light as been trigerred before by the
    /// "LightTriggered" method and the light is not currently on (this is 
    /// managed by the lightController script)
    /// </summary>
    private void TurnLightOn()
    {
        lightController.LightStatusUpdate(HeartRate, activateLight);
        activateLight = false;
    }



    /// <summary>
    /// Increases the number of fireflies catched and if it's in a condition of 
    /// increasing the lives it adds another life
    /// </summary>
    private void FireflyCatched()
    {
        firefliesCatched++;
        UpdateFireflyText();
        fireflyCatch.Play();

        int aux = firefliesCatched % NUM_FIREFLIES_TO_GIVE_LIFE;
        if (firefliesCatched != 0 && aux == 0)
        {
            if (Lives != 0 && Lives < MAX_LIVES)
            {
                Lives++;
                UpdateLivesText();
            }
        }
    }

    /// <summary>
    /// Detects if the player is colliding with an obstacle if this happens then
    /// the player loses a life and the obstacle is immediately destroyed
    /// </summary>
    private void DetectPlayerObstacleCollision()
    {
        if (player_oc.isColliding)
        {
            GameObject go = player_oc.obstacleColliding;
            Destroy(go);
            player_oc.obstacleColliding = null;
            player_oc.isColliding = false;

            Lives--;
            UpdateLivesText();
            obstacleDamage.Play();
        }
    }

    /// <summary>
    /// Update the number of lives on Screen
    /// </summary>
    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + Lives; 
    }

    /// <summary>
    /// Updates the number of fireflies catched on Screen
    /// </summary>
    private void UpdateFireflyText()
    {
        firefliesText.text = "Fireflies: " + firefliesCatched;
    }

    /// <summary>
    /// Attempt to catch a firefly on the left side, if the firefly is at the 
    /// correct position to be catched then the firefly is destroyed and the 
    /// number of fireflies catched is increased
    /// </summary>
    private void CheckForHandActions()
    {
        //  Check for collisions on the left hand
        if (lfc.isFireflyColliding)
        {
            GameObject go = lfc.objectColliding;
            Destroy(go);
            lfc.objectColliding = null;
            lfc.isFireflyColliding = false;

            FireflyCatched();
        }
        else if(lfc.isObstacleColliding)
        {
            GameObject go = lfc.objectColliding;
            Destroy(go);
            lfc.objectColliding = null;
            lfc.isObstacleColliding = false;

            obstacleHit.Play();
        }

        //  Check for collisions on the right hand
        if(rfc.isFireflyColliding)
        {
            GameObject go = rfc.objectColliding;
            Destroy(go);
            rfc.objectColliding = null;
            rfc.isFireflyColliding = false;

            FireflyCatched();
        }
        else if (rfc.isObstacleColliding)
        {
            GameObject go = rfc.objectColliding;
            Destroy(go);
            rfc.objectColliding = null;
            rfc.isObstacleColliding = false;

            obstacleHit.Play();
        }
    }

    ///////////////////////////////////////////////////////////////////////////
    ///                                                                     ///
    ///   Public methods to be accessed by other scripts to report events   ///
    ///                                                                     ///
    ///////////////////////////////////////////////////////////////////////////
    
    /// <summary>
    /// Pedalling Detected
    /// </summary>
    public void Pedal()
    {
        stopWatch.Stop();
        stopWatch.Reset();
        stopWatch.Start();
        isMoving = true;
    }

    /// <summary>
    /// Light "Switch" was triggered
    /// </summary>
    public void LightTriggered()
    {
        if (lightTimeout.ElapsedMilliseconds > TIMEOUT)
        {
            Lives--;
            UpdateLivesText();
            activateLight = true;
        }

    }
}
