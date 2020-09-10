using System;
using System.Threading;

public class Timeout
{
    /// <summary>
    /// Setups up a new thread that uses an Alarm to determine when a specified amount of time has gone by.
    /// Set the event handler to handle what happens when the Alarm rings (time expires). 
    /// The duration of the alarm may be set at instantiation.
    /// </summary>

    private Thread timeoutCounter;
    private Alarm timer;
    private int timeout_duration;
    
    public Timeout(int timeout_duration)
    {
        this.timeout_duration = timeout_duration;
        timer = new Alarm(timeout_duration);
        timeoutCounter = new Thread(new ThreadStart(timer.Run)); //create new thread each time
    }

    public void Start()
    {
        timeoutCounter.Start();
    }

    public void Stop()
    {
        timer.Stop();
    }

    public void Set_Event_Handler(EventHandler method)
    {
        timer.OnRing += method;
    }
}

public class Alarm
{
    /// <summary>
    /// Used by the Timeout class to start a thread that counts in 1 second increments to a specified number
    /// When the thread reaches its timeout_duration, it will start ringing which sets off an Event to be handled by Timeout class
    /// </summary>

    private bool active = true;
    private int timeout_duration=10;

    public Alarm(int timeout_duration)
    {
        this.timeout_duration = timeout_duration;
    }

    public void Run()
    {
        int i = 0;
        while (active && i < timeout_duration)
        {
            Thread.Sleep(1000);
            i++;
        }

        if (active) Ring(EventArgs.Empty); //If no longer active, user has stopped the timer manually
    }

    private void Ring(EventArgs e)
    {
        EventHandler handler = OnRing;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    public event EventHandler OnRing;

    public void Stop()
    {
        active = false;
    }
}

