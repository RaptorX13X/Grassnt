using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.UIElements.UxmlAttributeDescription;

// Singleton class
public sealed class BoyzBrain : MonoBehaviour
{
    [SerializeField] private float chilinTime = 2f;
    [SerializeField] private float chilinTimeVariety = 0.5f;

    private List<Unit> units = new List<Unit>();

    private float remainingChillingTime;

    public static BoyzBrain Instance = null;

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

    public void AddBro(Unit unit)
    {
        units.Add(unit);

        unit.GetComponent<Health>().OnDeath += () => units.Remove(unit);

        remainingChillingTime = Random.Range(chilinTime - chilinTimeVariety, chilinTime + chilinTimeVariety);
    }

    private void Update()
    {
        remainingChillingTime -= Time.deltaTime;

        if (remainingChillingTime <= 0 && units.Count > 0)
        {
            remainingChillingTime = Random.Range(chilinTime - chilinTimeVariety, chilinTime + chilinTimeVariety);

            var possibleUnits = units.Where(x => x.CurrentState is BoyzRoaminState);
            if (!possibleUnits.Any())
                return;

            var unitToFight = possibleUnits.Skip(Random.Range(0, possibleUnits.Count())).FirstOrDefault();

            unitToFight.SwitchState(BoyzFightinState.StateId);
        }
    }
}
