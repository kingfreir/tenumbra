using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LightControl
{
    private const int MIN_TIME = 5000;
    private const int MAX_TIME = 10000;

    private const float LIGHT_OFF = 0.0f;
    private const float LIGHT_ON = 2.0f;

    private const int HR_MIN = 100;
    private const int HR_MAX = 140;

    private Light flashlight;
    private bool isLightOn;
    private Stopwatch stopWatch;
    private int timeout;
    private double y_intercept;
    private double slope;

    // Use this for initialization
    public LightControl(Light flashlight)
    {
        this.flashlight = flashlight;
        this.flashlight.intensity = 0.0f;
        isLightOn = false;

        CalculateLinearEquation();

        stopWatch = new Stopwatch();
        stopWatch.Reset();
        stopWatch.Start();
    }

    public void LightStatusUpdate(int HeartRate, bool activate)
    {
        //  If light is on do not check for input
        if (!isLightOn && activate)
        {
            TurnLightOn(HeartRate);
        }
        else
        {
            if (stopWatch.ElapsedMilliseconds > timeout) TurnLightOff();
        }
    }

    private void TurnLightOn(int HeartRate)
    {
        SetTimeout(HeartRate);
        ResetTimer();
        flashlight.intensity = LIGHT_ON;
        isLightOn = true;
    }

    private void SetTimeout(int HeartRate)
    {
        int aux = GetTimeoutValue(HeartRate);

        //  Clamping value
        if (aux < MIN_TIME) timeout = MIN_TIME;
        else if (aux > MAX_TIME) timeout = MAX_TIME;
        else timeout = aux;
    }

    private void TurnLightOff()
    {
        flashlight.intensity = LIGHT_OFF;
        isLightOn = false;
    }

    private void ResetTimer()
    {
        stopWatch.Stop();
        stopWatch.Reset();
        stopWatch.Start();
    }

    private void CalculateLinearEquation()
    {
        slope = (double)(HR_MAX - HR_MIN) / (MIN_TIME - MAX_TIME);
        y_intercept = (double)HR_MAX - (double)(slope * MIN_TIME);
    }

    private int GetTimeoutValue(int HeartRate)
    {
        double temp = (HeartRate - y_intercept) / slope;
        return (int)temp;
    }
}
