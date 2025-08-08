using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEventArgs : EventArgs
{
    public string playerId;
    public Dictionary<PowerUpTypes, int> powerUps;

    public PowerUpEventArgs(string playerId, Dictionary<PowerUpTypes, int> powerUps)
    {
        this.playerId = playerId;
        this.powerUps = powerUps;
    }
}
