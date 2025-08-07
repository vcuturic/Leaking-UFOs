using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    private float lerpSpeed = 0.05f;

    private float targetHealth;
    private PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        targetHealth = player.GetCurrentHealth();

        healthSlider.maxValue = player.GetMaxHealth();
        easeHealthSlider.maxValue = player.GetMaxHealth();

        player.OnHealthChanged += UpdateHealthDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != targetHealth)
        {
            healthSlider.value = targetHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, targetHealth, lerpSpeed);
        }
    }

    void UpdateHealthDisplay(float newHealth)
    {
        targetHealth = newHealth;
    }

    void OnDestroy()
    {
        if (player != null)
            player.OnHealthChanged -= UpdateHealthDisplay;
    }
}
