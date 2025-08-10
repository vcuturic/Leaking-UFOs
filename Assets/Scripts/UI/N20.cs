using UnityEngine;
using UnityEngine.UI;

public class N20 : MonoBehaviour
{
    private Slider nitrousSlider;
    private float nitrousValue;

    private PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        nitrousSlider = GetComponent<Slider>();

        nitrousValue = player.GetCurrentNitrous();
        nitrousSlider.maxValue = player.GetMaxNitrous();

        player.OnNitrousChange += UpdateNitrousDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        if(nitrousSlider.value != nitrousValue)
        {
            nitrousSlider.value = nitrousValue;
        }
    }

    void UpdateNitrousDisplay(object sender, float value)
    {
        nitrousValue = value;
    }

    private void OnDestroy()
    {
        player.OnNitrousChange -= UpdateNitrousDisplay;
    }
}
