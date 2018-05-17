using System.Threading;

public sealed class HR_Manager
{
    private HR_Factory hr_factory;
    private HR_Monitor hr_monitor;
    private Thread UD;

    public HR_Manager()
    {
        hr_factory = HR_Factory.Instance();
    }

    /// <summary>
    /// Initialize the heart rate monitor and start a thread to keep the data updated.
    /// </summary>
    /// <param name="UDP_PORT"></param>
    /// <param name="IPAddress"></param>
    /// <param name="type"></param>
    public void Initialize(int UDP_PORT, string IPAddress, string type)
    {
        hr_monitor = hr_factory.createHR_Monitor(UDP_PORT, IPAddress, type);

        UD = new Thread(UpdateData);
        UD.Start();
    }

    public void Close()
    {
        //  Kill the thread and close the connections.
        UD.Abort();
        hr_monitor.Close();
        hr_monitor = null;
    }

    private void UpdateData()
    {
        while (true)
        {
            //  Read data from device.
            hr_monitor.FetchHR();

            //  Send thread to sleep for 50ms
            Thread.Sleep(50);
        }
    }

    public int ReadData()
    {
        return hr_monitor.GetHR();
    }
}
