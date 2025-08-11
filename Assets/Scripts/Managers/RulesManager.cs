using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesManager : MonoBehaviour
{
    int rulesSet = 0;
    Dictionary<string, PowerUpTypes> playerForbiddenRules = new Dictionary<string, PowerUpTypes>();
    private GameManager gameManager;
    private PunishmentTypes activePunishment;
    public float healthPunishmentDamage = 20f;
    private GameObject judge;
    public float changeDuration = 0.5f;  // time to fade down and up
    public float holdDuration = 1f;      // time to hold at blue=150

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        activePunishment = PunishmentTypes.Health;
        judge = GameObject.FindGameObjectWithTag("Judge");
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
            SoundManager.Instance.PlayMalfunctionSFX();
            activePunishment = PunishmentTypes.Stun;
        }
        else if(activePunishment == PunishmentTypes.Stun)
        {
            player.StunPlayer();
            SoundManager.Instance.PlayStunSFX();
            VFXManager.Instance.PlaySparks(player.transform.position);
            activePunishment = PunishmentTypes.Health;
        }

        SoundManager.Instance.PlayRuleChangeSFX();
        ChangeBlueChannel();
    }

    public void ChangeBlueChannel()
    {
        StartCoroutine(ChangeBlueCoroutine());
    }

    private IEnumerator ChangeBlueCoroutine()
    {
        RawImage judgeRawImage = judge.GetComponent<RawImage>();
        Color originalColor = judgeRawImage.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, 150f / 255f, originalColor.a);

        // Fade blue from original to target (150/255)
        float elapsed = 0f;
        while (elapsed < changeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / changeDuration;
            judgeRawImage.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
        }

        judgeRawImage.color = targetColor;

        // Hold at target color
        yield return new WaitForSeconds(holdDuration);

        // Fade blue back to original (255)
        elapsed = 0f;
        while (elapsed < changeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / changeDuration;
            judgeRawImage.color = Color.Lerp(targetColor, originalColor, t);
            yield return null;
        }

        judgeRawImage.color = originalColor;
    }
}
