using System;
using System.Collections.Generic;
using UnityEngine;

public class RulesManager : MonoBehaviour
{
    int rulesSet = 0;
    Dictionary<string, PowerUpTypes> playerForbiddenRules = new Dictionary<string, PowerUpTypes>();
    private GameManager gameManager;
    private PunishmentTypes activePunishment;
    public float healthPunishmentDamage = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        activePunishment = PunishmentTypes.Health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PowerUpTypes? GetPlayerForbiddenRule(string playerId)
    {
        if (!playerForbiddenRules.ContainsKey(playerId)) return null;
        
        return playerForbiddenRules[playerId];
    }

    public void SetPlayerForbiddenRule(string playerId, int powerUpId)
    {
        if(!playerForbiddenRules.ContainsKey(playerId))
        {
            playerForbiddenRules.Add(playerId, (PowerUpTypes)powerUpId);

            rulesSet++;

            if(rulesSet == 2)
                gameManager.StartGame();
        }
    }

    public void Punish(PlayerController player)
    {
        if(activePunishment == PunishmentTypes.Health)
        {
            player.TakeDamage(healthPunishmentDamage);
            activePunishment = PunishmentTypes.Stun;
        }
        else if(activePunishment == PunishmentTypes.Stun)
        {
            player.StunPlayer();
            activePunishment = PunishmentTypes.Health;
        }
    }
}
