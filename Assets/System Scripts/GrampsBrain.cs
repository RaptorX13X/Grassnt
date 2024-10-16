using UnityEngine;
// Singleton class
public sealed class GrampsBrain : MonoBehaviour
{

    private Unit gramp;
    private Unit granny;
    private Health grannyHealth;

    public float GrannyHealhtPercent => grannyHealth != null ? grannyHealth.Percent : 0f;
    public Unit Grandpa => gramp;
    public static GrampsBrain Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetGranny(Unit granny)
    {
        this.granny = granny;
        grannyHealth = granny.GetComponent<Health>();
    }

    public void SetGrandpa(Unit grandpa)
    {
        this.gramp = grandpa;
    }

    public float GrandpaScaling(float baseValue, float extremeValue)
    {
        return Mathf.Lerp(extremeValue, baseValue, GrannyHealhtPercent);
    }
}
