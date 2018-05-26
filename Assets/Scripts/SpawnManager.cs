using System.Diagnostics;
using UnityEngine;

public class SpawnManager
{
    public static int maxGameTime { get; set; }
    public int Gametime { get; set; }
    public GameObject fireflyObject { get; set; }
    public GameObject obstacleObject { get; set; }

    private static SpawnManager _instance = null;

    private const double min_f_spawn_rate = 3.0;
    private const double max_f_spawn_rate = 7.5;
    private const double min_o_spawn_rate = 7.5;
    private const double max_o_spawn_rate = 3.0;
    private const double SPAWN_DISTANCE = 130.0;

    private double f_y_intercept, f_slope, o_y_intercept, o_slope;
    private Stopwatch f_stopWatch, o_stopWatch;

    private SpawnManager()
    {
        f_stopWatch = new Stopwatch();
        o_stopWatch = new Stopwatch();

        f_stopWatch.Reset();
        f_stopWatch.Start();

        o_stopWatch.Reset();
        o_stopWatch.Start();

        CalculateLinearEquation(maxGameTime, 0, min_f_spawn_rate, max_f_spawn_rate, out f_slope, out f_y_intercept);
        CalculateLinearEquation(maxGameTime, 0, max_o_spawn_rate, min_o_spawn_rate, out o_slope, out o_y_intercept);
    }

    public static SpawnManager Instance()
    {
        if (_instance == null) _instance = new SpawnManager();
        return _instance;
    }

    /// <summary>
    /// 
    /// </summary>
    public void StopStopWatches()
    {
        f_stopWatch.Stop();
        o_stopWatch.Stop();
    }

    /// <summary>
    /// 
    /// </summary>
    public void StartStopWatches()
    {
        f_stopWatch.Start();
        o_stopWatch.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="posX"></param>
    public void manageSpawn(float posX)
    {
        double spawnPos = posX + SPAWN_DISTANCE;
        if (IsTimeToSpawnFirefly()) spawnFirefly((float) spawnPos);
        if (IsTimeToSpawnObstacle()) spawnObstacle((float) spawnPos); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="posX"></param>
    private void spawnFirefly(float posX)
    {
        GameObject go = GameObject.Instantiate(fireflyObject);
        go.transform.position = new Vector3(
            posX, 
            UnityEngine.Random.Range(1.0f, 1.9f),
            UnityEngine.Random.Range(15.5f, 17.5f));

        f_stopWatch.Stop();
        f_stopWatch.Reset();
        f_stopWatch.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="posX"></param>
    private void spawnObstacle(float posX)
    {
        GameObject go = GameObject.Instantiate(obstacleObject);
        go.transform.position = new Vector3(posX, 0, 0);

        o_stopWatch.Stop();
        o_stopWatch.Reset();
        o_stopWatch.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private bool Random()
    {
        int random = UnityEngine.Random.Range(0, 2);
        if (random == 0) return false;
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="posX"></param>
    /// <returns></returns>
    private bool IsTimeToSpawnFirefly()
    {
        double y = ((f_slope * Gametime) + f_y_intercept) * 1000;

        if (f_stopWatch.ElapsedMilliseconds < (int) y) return false;
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="posX"></param>
    /// <returns></returns>
    private bool IsTimeToSpawnObstacle()
    {
        double y = ((o_slope * Gametime) + o_y_intercept) * 1000;

        if (o_stopWatch.ElapsedMilliseconds < (int) y) return false;
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="X_MAX"></param>
    /// <param name="X_MIN"></param>
    /// <param name="Y_MAX"></param>
    /// <param name="Y_MIN"></param>
    /// <param name="slope"></param>
    /// <param name="y_intercept"></param>
    private void CalculateLinearEquation(double X_MAX, double X_MIN, double Y_MAX, double Y_MIN, out double slope, out double y_intercept)
    {
        slope = (Y_MAX - Y_MIN) / (X_MAX - X_MIN);
        y_intercept = Y_MAX - (slope * X_MAX);
    }
}
