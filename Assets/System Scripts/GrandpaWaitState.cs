using UnityEngine;

[CreateAssetMenu(fileName = "GrandpaWaitState", menuName = "SO/GrandpaWaitState")]
public class GrandpaWaitState : AUnitState
{
    public const int StateId = 1;

    [SerializeField] private float waitingTime = 1.5f;
    [SerializeField] private float minWaitingTime = 0.25f;
    private float alreadyWaited;
    public override void EnterState(Unit unit)
    {
        unit.SpriteRenderer.sprite = stateSprite;
        alreadyWaited = 0f;
    }

    public override void FixedUpdateState(Unit unit)
    {
        // Nothing
    }

    public override void UpdateState(Unit unit)
    {
        alreadyWaited += Time.deltaTime;
        if (alreadyWaited >= GrampsBrain.Instance.GrandpaScaling(waitingTime, minWaitingTime))
        {
            unit.SwitchState(GrandpaFightingState.StateId);
        }
    }

    public override void OnCollisionEnterAction(Unit unit, Collision2D collision)
    {
        // Nothing
    }
}
