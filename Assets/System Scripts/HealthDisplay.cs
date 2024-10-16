using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    private void Start()
    {
        playerHealth.OnDamaged += DisplayHealth;

        DisplayHealth();
    }

    private void DisplayHealth()
    {
        text.text = playerHealth.Value.ToString();
    }
}
