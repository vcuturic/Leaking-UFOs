using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRulesButton : MonoBehaviour
{
    private Button ruleButton;
    public int powerUpId;
    private PlayerController player;
    private RulesManager rulesManager;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ruleButton = GetComponent<Button>();
        player = GetComponentInParent<PlayerController>();
        rulesManager = FindFirstObjectByType<RulesManager>();
        gameManager = FindFirstObjectByType<GameManager>();

        ruleButton.onClick.AddListener(SetEnemyRule);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetEnemyRule()
    {
        Debug.Log($"Player {player.playerId} chooses to forbid {powerUpId} Id for enemy.");

        rulesManager.SetPlayerForbiddenRule(player.playerId.Equals("1") ? "2" : "1", powerUpId);

        gameManager.ClosePlayerRulesMenu(player.playerId);
    }
}
