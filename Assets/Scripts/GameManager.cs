using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int milliMinute = 60000;

    //  Variables for this class.
    public int UDP_PORT;
    public string IPAddress;
    public string HR_Monitor_Type;
    private HR_Manager hr_manager;
    public Text HeartRateText;
    private int HeartRate;
    private Stopwatch stopwatch;


    // Use this for initialization
    void Start()
    {
        hr_manager = new HR_Manager();
        hr_manager.Initialize(UDP_PORT, IPAddress, HR_Monitor_Type);

        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeartRate();
        HeartRateText.text = HeartRate.ToString();
        PulseHR();
    }

    /// <summary>
    /// Reads the stored value for the heart rate and saves it.
    /// </summary>
    private void UpdateHeartRate()
    {
        HeartRate = hr_manager.ReadData();
    }

    /// <summary>
    /// This function is for the pulsing heart beat effect according to the heart rate value.
    /// </summary>
    private void PulseHR()
    {
        if(HeartRate == 0)
        {
            return;
        }
        //  Time it takes for a heart beat to happen.
        float millisBetweenBeats = milliMinute / HeartRate;

        if(stopwatch.ElapsedMilliseconds >= millisBetweenBeats )
        {
            //  Make a heart beat pulse.
            HeartRateText.text = "Beat";

            //  Reset the stopwatch without stoping.
            stopwatch.Reset();
        }
    }
}