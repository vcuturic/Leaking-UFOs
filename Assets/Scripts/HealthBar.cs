using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    private float lerpSpeed = 0.05f;

    private float playerHealth;
    private PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        playerHealth = player.GetCurrentHealth();

        healthSlider.maxValue = player.GetMaxHealth();
        easeHealthSlider.maxValue = player.GetMaxHealth();

        player.OnHealthChanged += UpdateHealthDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != playerHealth)
        {
            healthSlider.value = playerHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, playerHealth, lerpSpeed);
        }
    }

    void UpdateHealthDisplay(string targetId, float newHealth)
    {
        if(player.playerId == targetId)
        {
            playerHealth = newHealth;
        }
    }

    void OnDestroy()
    {
        if (player != null)
            player.OnHealthChanged -= UpdateHealthDisplay;
    }
}
