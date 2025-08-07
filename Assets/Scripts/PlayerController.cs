using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed;
    public GameObject projectilePrefab;

    private float horizontalInput;
    private float verticalInput;
    private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private Transform[] gunSpawnPoints;

    public delegate void HealthChanged(float newHealth);
    public event HealthChanged OnHealthChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // DEBUG - temporary disable player2 movement and shooting
        if(gameObject.CompareTag("Player"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

            if (Input.GetKey(KeyCode.Space))
            {
                foreach (var spawnPoint in gunSpawnPoints)
                {
                    GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
                    // Set the owner/spawner of the projectile so it can ignore colliding with owner
                    Projectile projScript = projectile.GetComponent<Projectile>();
                    projScript.owner = gameObject;
                }
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
}
