using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "BlobFightngState", menuName = "SO/BlobFightngState")]
public class BlobFightngState : AUnitState
{
    public const int StateId = 0;

    [SerializeField] private float jumpDistance = 2f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float angleMaxDistortion = 25f;
    [SerializeField] private float distanceMaxDistortion = 0.5f;
    private float distanceToFlip = 3f;
    private float distanceTraveled = 0f;
    private Vector2 lastPosition = Vector2.one * 9999;

    private Vector2 targetPosition;
    [SerializeField] private List<AudioClip> hopClips;

    public override void EnterState(Unit unit)
    {
        unit.SpriteRenderer.sprite = stateSprite;
        if (Vector2.Distance(unit.transform.position, unit.PlayerReference.transform.position) > jumpDistance)
            targetPosition = unit.transform.position + Quaternion.AngleAxis(Random.Range(-angleMaxDistortion, angleMaxDistortion), Vector3.forward) * (unit.PlayerReference.transform.position - unit.transform.position).normalized * Random.Range(jumpDistance - distanceMaxDistortion, jumpDistance + distanceMaxDistortion);
        else
            targetPosition = unit.PlayerReference.transform.position;
        AudioManager.instance.PlaySound(GetRandomSound(hopClips));
    }

    private AudioClip GetRandomSound(List<AudioClip> clipsToRandomize)
    {
        int randomInt = Random.Range(0, hopClips.Count);
        AudioClip returnRandom = clipsToRandomize[randomInt];
        return returnRandom;
    }

    public override void FixedUpdateState(Unit unit)
    {
        unit.Rigidbody2D.MovePosition(Vector2.MoveTowards(unit.transform.position, targetPosition, jumpSpeed * Time.fixedDeltaTime));

        if (Vector2.Distance(unit.transform.position, targetPosition) <= 0.1f)
            unit.SwitchState(BlobChillingState.StateId);

        if (lastPosition == Vector2.one * 9999)
        {
            lastPosition = unit.Rigidbody2D.position;
            return;
        }
        distanceTraveled += (unit.Rigidbody2D.position - lastPosition).magnitude;
        lastPosition = unit.Rigidbody2D.position;
        if (distanceTraveled >= distanceToFlip)
        {
            distanceTraveled = 0;
            unit.SpriteRenderer.flipX = !unit.SpriteRenderer.flipX;
        }
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
