using UnityEngine;
using TMPro;

public class Abilities : MonoBehaviour
{
    public TextMeshProUGUI bazookaText;
    public TextMeshProUGUI CQCText;
    public TextMeshProUGUI shieldText;
    private PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<PlayerController>();

        player.OnPowerUpPicked += UpdateUI;
        // ADD power used here

        bazookaText.text = "Bazooka: 0";
        CQCText.text = "CQC: 0";
        shieldText.text = "Shield: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI(object sender, PowerUpEventArgs pupArgs)
    {
        bazookaText.text = "Bazooka: " + pupArgs.powerUps[PowerUpTypes.BazookaLauncher];
        CQCText.text = "CQC: " + pupArgs.powerUps[PowerUpTypes.CQC];
        shieldText.text = "Shield: " + pupArgs.powerUps[PowerUpTypes.Shield];
    }
}
