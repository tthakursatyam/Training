using System;
using System.Collections.Generic;
public delegate void SecurityAction(string zone); 

public class MotionSensor
{
    // The delegate instance (The Panic Button)
    public SecurityAction OnEmergency; // instance creation

    public void DetectIntruder(string zoneName)
    {
        Console.WriteLine($"[SENSOR] Motion detected in {zoneName}!");
        
        // 3. INVOCATION: Triggering the Panic Button
        if (OnEmergency != null)
        {
            OnEmergency(zoneName); // string value = Main Lobby or panicSequence?
        }
    }
}

// Diverse classes that don't know about each other
public class AlarmSystem
{
    public void SoundSiren(string zone) => Console.WriteLine($"[ALARM] WOO-OOO! High-decibel siren active in {zone}.");
}

public class PoliceNotifier
{
    public void CallDispatch(string zone) => Console.WriteLine($"[POLICE] Notifying local precinct of intrusion in {zone}.");
}
