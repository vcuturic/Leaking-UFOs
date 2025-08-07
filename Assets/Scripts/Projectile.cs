using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject owner;
    public float singleBeamDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (owner != null)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), owner.GetComponent<SphereCollider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2") || other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(singleBeamDamage);
            }

            Destroy(gameObject);
        }
    }

    public void SetProjectileOwner(GameObject projectileOwner) => owner = projectileOwner;
}
