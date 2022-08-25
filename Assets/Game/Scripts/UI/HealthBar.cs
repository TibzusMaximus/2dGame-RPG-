using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        _text.text = health.ToString();
        _slider.value = health;
    }
    public void SetCurrentHealth(int health)
    {
        _slider.value = health;
        _text.text = health.ToString();
    }
}