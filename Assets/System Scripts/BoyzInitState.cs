using UnityEngine;

[CreateAssetMenu(fileName = "BoyzInitState", menuName = "SO/BoyzInitState")]
public class BoyzInitState : AUnitState
{
    public const int StateId = 0;

    public override void EnterState(Unit unit)
    {
        unit.SpriteRenderer.sprite = stateSprite;
        BoyzBrain.Instance.AddBro(unit);

        unit.SwitchState(BoyzRoaminState.StateId);
    }

    public override void FixedUpdateState(Unit unit)
    {
        // Nothing
    }

    public override void UpdateState(Unit unit)
    {
        // Nothing
    }

    public override void OnCollisionEnterAction(Unit unit, Collision2D collision)
    {
        // Nothing
    }
}
