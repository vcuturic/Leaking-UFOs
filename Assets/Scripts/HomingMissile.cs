using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float turnSpeed = 10;
    public float rocketVelocity = 15;
    public float lifeTime = 10f;
    private float missileRange = 2f;
    private float missileDamage = 20f;

    private GameObject enemyPlayer;
    private Rigidbody rb;

    public void Initialize(GameObject enemyPlayer)
    {
        this.enemyPlayer = enemyPlayer;

        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * rocketVelocity;

        var rocketTargetRotation = Quaternion.LookRotation(enemyPlayer.transform.position - transform.position);

        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, turnSpeed));

        if(IsMissileInRangeOf(enemyPlayer, missileRange))
        {
            Debug.Log("Enemy hit!");
            PlayerController enemyScript = enemyPlayer.GetComponent<PlayerController>();

            if(!enemyScript.shieldActive)
            {
                enemyScript.TakeDamage(missileDamage);
                enemyScript.StunPlayer();
                SoundManager.Instance.PlayRocketImpactSFX();
                VFXManager.Instance.PlayExplosion(enemyPlayer.transform.position);
            }

            Destroy(gameObject);
        }
    }

    bool IsMissileInRangeOf(GameObject target, float range)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= range;
    }
}
