using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    public float speed = 5.0f;
    public float turnSpeed;
    private float horizontalInput;
    private float forwardInput;
    // Shooting
    public GameObject projectilePrefab;
    [SerializeField] private Transform[] gunSpawnPoints;
    [SerializeField] private float fireRate = 0.2f;
    private float nextFireTime = 0f;
    // Health Bar
    private float maxHealth = 100f;
    private float currentHealth;
    public delegate void HealthChanged(string playerId, float newHealth);
    public event HealthChanged OnHealthChanged;
    // Local Multiplayer
    public string playerId;
    // PoweruUps
    public event EventHandler<PowerUpEventArgs> OnPowerUpPicked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(playerId, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal" + playerId);
        forwardInput = Input.GetAxis("Vertical" + playerId);

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

        if (Input.GetButton("Jump" + playerId) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            foreach (var spawnPoint in gunSpawnPoints)
            {
                GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
                // Set the owner/spawner of the projectile so it can ignore colliding with owner (See Projectile.cs)
                Projectile projScript = projectile.GetComponent<Projectile>();
                projScript.SetProjectileOwner(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            OnPowerUpPicked?.Invoke(this, new PowerUpEventArgs(playerId));
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(playerId, currentHealth);
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
}
