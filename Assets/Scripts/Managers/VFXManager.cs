using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    public ParticleSystem explosionPrefab;
    public ParticleSystem sparkPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayExplosion(Vector3 position)
    {
        ParticleSystem ps = Instantiate(explosionPrefab, position, Quaternion.identity);
        ps.Play();
        Destroy(ps.gameObject, ps.main.duration);
    }

    public void PlaySparks(Vector3 position)
    {
        ParticleSystem ps = Instantiate(sparkPrefab, position, Quaternion.identity);
        ps.Play();
        Destroy(ps.gameObject, ps.main.duration);
    }
}
