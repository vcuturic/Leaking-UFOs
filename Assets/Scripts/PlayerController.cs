using System;
using System.Collections;
using System.Collections.Generic;
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
    // PowerUps/Abilities
    public event EventHandler<PowerUpEventArgs> OnPowerUpPicked;
    private Dictionary<PowerUpTypes, int> powerUps = new Dictionary<PowerUpTypes, int>();
    // Rocket/Bazooka
    public GameObject rocketPrefab;
    private Vector3 rocketOffset = new Vector3(0, 1, 0);
    // Hammer/CQC
    public GameObject hammerPrefab;
    private Vector3 hammerOffset = new Vector3(0, 1, 0);
    private float hammerDamage = 30f;
    // Shield
    public GameObject shieldPrefab;
    public bool shieldActive = false;
    public float shieldDurationSeconds = 4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        InitializePowerUps();
        OnHealthChanged?.Invoke(playerId, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal" + playerId);
        forwardInput = Input.GetAxis("Vertical" + playerId);

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

        HandleShooting();
        HandleAbilities();
    }

    void HandleShooting()
    {
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
    void HandleAbilities()
    {
        GameObject enemyPlayer = GetEnemyGameObject();

        if (Input.GetButtonDown("Ability1_" + playerId))
        {
            if (powerUps[PowerUpTypes.BazookaLauncher] > 0)
            {
                Debug.Log($"Player {playerId} shoot Bazooka launcher!");
                powerUps[PowerUpTypes.BazookaLauncher] -= 1;

                GameObject rocket = Instantiate(rocketPrefab, transform.position + rocketOffset, rocketPrefab.transform.rotation);
                
                HomingMissile missileScript = rocket.GetComponent<HomingMissile>();
                
                missileScript.Initialize(enemyPlayer);
            }
        }

        if (Input.GetButtonDown("Ability2_" + playerId) && IsPlayerInRangeOf(enemyPlayer, 2f))
        {
            if (powerUps[PowerUpTypes.CQC] > 0)
            {
                Debug.Log($"Player {playerId} engaged in Close Quarters Combat!");
                powerUps[PowerUpTypes.CQC] -= 1;

                var HammerTargetRotation = Quaternion.LookRotation(enemyPlayer.transform.position - transform.position);
                GameObject spawnedHammer = Instantiate(hammerPrefab, transform.position + hammerOffset, HammerTargetRotation);
               
                PlayerController enemyScript = enemyPlayer.GetComponent<PlayerController>();
                
                enemyScript.TakeDamage(hammerDamage);

                StartCoroutine(RemoveSpawnedHammer(spawnedHammer));
            }
        }

        if (Input.GetButtonDown("Ability3_" + playerId) && !shieldActive)
        {
            if (powerUps[PowerUpTypes.Shield] > 0)
            {
                Debug.Log($"Player {playerId} activated shield!");

                powerUps[PowerUpTypes.Shield] -= 1;

                GameObject spawnedShield = Instantiate(shieldPrefab, transform.position, transform.rotation, transform);

                shieldActive = true;

                StartCoroutine(StartShieldDeactivationCountdown(spawnedShield));
            }
        }

        // ADD power used here
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            GeneratePowerUp();
            OnPowerUpPicked?.Invoke(this, new PowerUpEventArgs(playerId, powerUps));
        }
    }

    IEnumerator StartShieldDeactivationCountdown(GameObject obj)
    {
        yield return new WaitForSeconds(shieldDurationSeconds);
        shieldActive = false;
        Destroy(obj);
    }

    IEnumerator RemoveSpawnedHammer(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }

    GameObject GetEnemyGameObject()
    {
        if (playerId == null) return null;

        if (playerId.Equals("1"))
            return GameObject.FindWithTag("Player2");
        else
            return GameObject.FindWithTag("Player");
    }

    public void AddPowerUp(PowerUpTypes powerUpType)
    {
        if (!powerUps.ContainsKey(powerUpType)) return;

        powerUps.Add(powerUpType, 1);
    }

    public void UsePowerUp(PowerUpTypes powerUpType)
    {
        if (powerUps[powerUpType] <= 0) return;

        powerUps[powerUpType]--;
    }

    void InitializePowerUps()
    {
        powerUps.Add(PowerUpTypes.BazookaLauncher, 5);
        powerUps.Add(PowerUpTypes.CQC, 5);
        powerUps.Add(PowerUpTypes.Shield, 5);
    }

    public void GeneratePowerUp()
    {
        PowerUpTypes powerUp = (PowerUpTypes)UnityEngine.Random.Range(0, 3);

        Debug.Log($"Generated {powerUp} PowerUp!");

        if(powerUps.ContainsKey(powerUp))
        {
            powerUps[powerUp] += 1;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(playerId, currentHealth);
    }

    bool IsPlayerInRangeOf(GameObject target, float range)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= range;
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
}
