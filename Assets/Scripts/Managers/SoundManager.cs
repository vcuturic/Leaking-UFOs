using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    float maxVolume = 0.6f;
    public static SoundManager Instance;
    // Sources
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource sfxLoopSource;
    // Game Music
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    // UFO
    public AudioClip ufoHum;
    // Rocket launcher
    public AudioClip rocketFireClip;
    public AudioClip rocketImpactClip;
    public AudioClip rocketPickupClip;
    // Hammer
    public AudioClip hammerClip;
    public AudioClip hammerPickupClip;
    // Shield
    public AudioClip shieldClip;
    public AudioClip shieldPickupClip;
    // Beam
    public AudioClip beamClip;
    // Status effects
    public AudioClip stunClip;
    public AudioClip malfunctionClip;
    // Judge 
    public AudioClip ruleChangeClip;
    // Nitrous
    public AudioClip nitrousClip;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayNitrousSFX() => PlayLoopingSFX(nitrousClip);
    public void PlayRuleChangeSFX() => PlaySFX(ruleChangeClip);
    public void PlayStunSFX() => PlaySFX(stunClip);
    public void PlayMalfunctionSFX() => PlaySFX(malfunctionClip);
    public void PlayRocketPickupSFX() => PlaySFX(rocketPickupClip);
    public void PlayHammerPickupSFX() => PlaySFX(hammerPickupClip);
    public void PlayShieldPickupSFX() => PlaySFX(shieldPickupClip);
    public void PlayShieldSFX() => PlaySFX(shieldClip);
    public void PlayBeamSFX(float volume = 1f) => PlaySFX(beamClip, volume);
    public void PlayHammerSFX() => PlaySFX(hammerClip);
    public void PlayUFOHummingSFX() => PlayLoopingSFX(ufoHum);
    public void PlayRocketImpactSFX() => PlaySFX(rocketImpactClip);
    public void PlayRocketFireSFX() => PlaySFX(rocketFireClip);
    public void PlayMenuMusic() => PlayMusic(menuMusic);
    public void PlayGameMusic() => PlayMusic(gameMusic);

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    private void PlayLoopingSFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxLoopSource.clip = clip;
        sfxLoopSource.loop = true;
        sfxLoopSource.Play();
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public IEnumerator FadeMusic(AudioClip newClip, float fadeDuration = 1f)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            musicSource.volume = Mathf.Lerp(maxVolume, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        musicSource.clip = newClip;
        musicSource.Play();

        timer = 0f;
        while (timer < fadeDuration)
        {
            musicSource.volume = Mathf.Lerp(0f, maxVolume, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        musicSource.volume = maxVolume;
    }
}
