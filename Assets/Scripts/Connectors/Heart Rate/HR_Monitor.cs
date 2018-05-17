using System.Collections;
using System.Collections.Generic;

public abstract class HR_Monitor : IHR_Monitor
{
    //  Variables for these classes of objects
    public int UDP_PORT { get; set; }
    public string IPAddress { get; set; }
    public UDP_Listener UListener { get; set; }

    public abstract void Close();

    public abstract int GetHR();

    public abstract void Initialize();

    public abstract void FetchHR();
}
