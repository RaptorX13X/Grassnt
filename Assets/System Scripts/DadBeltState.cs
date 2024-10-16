using UnityEngine;

[CreateAssetMenu(fileName = "DadBeltState", menuName = "SO/DadBeltState")]
public class DadBeltState : AUnitState
{
    public const int StateId = 1;

    [SerializeField] private float beltingTime = 1f;
    private float remainingBeltingTime;

    public override void EnterState(Unit unit)
    {
        unit.SpriteRenderer.sprite = stateSprite;

        remainingBeltingTime = beltingTime;

        unit.GetComponentInChildren<BlankieController>().StartAttack(unit.PlayerReference.transform.position);
    }

    public override void FixedUpdateState(Unit unit)
    {
        // Nothing
    }

    public override void UpdateState(Unit unit)
    {
        remainingBeltingTime -= Time.deltaTime;

        if (remainingBeltingTime <= 0)
        {
            unit.SwitchState(DadChasingState.StateId);
        }
    }

    public override void OnCollisionEnterAction(Unit unit, Collision2D collision)
    {
        // Nothing
    }
}
