/// <summary>
/// This class is responsible for the creation of HR_Monitors.
/// </summary>
public sealed class HR_Factory
{
    private static HR_Factory _instance = null;

    private HR_Factory() { }

    public static HR_Factory Instance()
    {
        if (_instance == null)
        {
            _instance = new HR_Factory();
        }
        return _instance;
    }

    /// <summary>
    /// Returns an instance of a HR_Monitor according to the build parameters passed.
    /// </summary>
    /// <param name="UDP_PORT"></param>
    /// <param name="IPAddress"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public HR_Monitor createHR_Monitor(int UDP_PORT, string IPAddress, string type)
    {
        HR_Monitor hrm;

        switch (type)
        {
            case "Generic":
                hrm = new HR_Monitor_Generic();
                break;
            case "BWATCH":
                hrm = new HR_Monitor_BWATCH();
                break;
            default:
                hrm = new HR_Monitor_Generic();
                break;
        }

        hrm.UDP_PORT = UDP_PORT;
        hrm.IPAddress = IPAddress;

        hrm.Initialize();

        return hrm;
    }
}
