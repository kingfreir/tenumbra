using System;

/// <summary>
/// 
/// </summary>
public sealed class HR_Monitor_BWATCH : HR_Monitor
{
    //  Force to use the GetHR method to avoid concurrency problems.
    private int HeartRate;
    private Object HR_Lock;

    public HR_Monitor_BWATCH() { }

    /// <summary>
    /// 
    /// </summary>
    public override void Close()
    {
        UListener.Close();
    }

    /// <summary>
    /// Initializes the needed functions.
    /// </summary>
    public override void Initialize()
    {
        HR_Lock = new Object();
        UListener = new UDP_Listener(UDP_PORT);
    }

    /// <summary>
    /// Read data and store it on the Heart Rate variable.
    /// </summary>
    public override void FetchHR()
    {
        int aux = 0;

        string msg = UListener.ReceiveData();

        aux = int.Parse(msg.Split(',')[0]);

        //  Lock to avoid concurrency problems while writing the heart rate value.
        lock (HR_Lock)
        {
            HeartRate = aux;
        }
    }

    /// <summary>
    /// Returns the last value obtained for the Heart Rate.
    /// </summary>
    /// <returns></returns>
    public override int GetHR()
    {
        int aux;

        //  Lock to avoid concurrency problems while reading the heart rate value.
        lock (HR_Lock)
        {
            aux = HeartRate;
        }

        return aux;
    }
}
