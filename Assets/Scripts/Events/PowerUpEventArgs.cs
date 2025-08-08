using System;
using UnityEngine;

public class PowerUpEventArgs : EventArgs
{
    public string playerId;

    public PowerUpEventArgs(string playerId)
    {
        this.playerId = playerId;
    }
}
