//Might not work --- Untested
//If it's gonna be used then it should be placed under a game object called TimeManager or some other meme
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float SlowDownFactor = 0.05f;
    public float SlowDownLength = 2f;

    public float SpeedUpFactor = 20f;
    public float SpeedUpLength = 2f;

    private void Update()
    {
        if (Time.timeScale < 1)
        {
            Time.timeScale += (1f / SlowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        };

        if (Time.timeScale > 1)
        {
            Time.timeScale -= (1f / SpeedUpLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 1f, 100f);
        };
        
    }

    public void SlowMotion ()
    {
        Time.timeScale = SlowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void FastMotion ()
    {
        Time.timeScale = SpeedUpFactor;
        Time.fixedDeltaTime = Time.timeScale / .02f;
    }
}