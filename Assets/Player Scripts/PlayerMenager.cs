using UnityEngine;

public class PlayerMenager : MonoBehaviour
{
    [SerializeField] InputMenager inputMenager;
    [SerializeField] Movemnet movement;
    [SerializeField] Interacter interacter;
    [SerializeField] private Boomerang boomerang;
    [SerializeField] private ShardThrower shart;
    [SerializeField] private ShardThrower pillow;
    [SerializeField] private KeyboardRotator keyboardRotator;
    [SerializeField] private BlankieController blankieController;
    [SerializeField] private Animator animator;
    [SerializeField] private DashController dashController;
    public enum Weapons { Pillow, Shard, Hanger, Blanket, Keyboard, None };
    private Weapons activeWeapon = Weapons.None;
    private float actionCooldown;

    [SerializeField] private AudioClip pickUpSound;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void FixedUpdate()
    {
        if (inputMenager.dashPressed)
        {
            inputMenager.dashPressed = false;
            dashController.TryApplyDash(inputMenager.mousePos, inputMenager.input);
        }

        movement.SetAnimSpeed(animator);
        movement.Move(inputMenager.input, inputMenager.mousePos);
        if (inputMenager.action)
        {
            if (actionCooldown > 0) return;
            switch (activeWeapon)
            {
                case Weapons.Pillow:
                    pillow.ShardThrow(inputMenager.mousePos);
                    actionCooldown = 0.3f;
                    break;
                case Weapons.Shard:
                    shart.ShardThrow(inputMenager.mousePos);
                    actionCooldown = 0.3f;
                    break;
                case Weapons.Hanger:
                    boomerang.Throw(inputMenager.mousePos);
                    actionCooldown = 0.3f;
                    break;
            }
        }
    }

    private void Update()
    {
        actionCooldown -= Time.deltaTime;


        SetAnim(inputMenager.input);
        if (inputMenager.action && actionCooldown <= 0)
        {
            switch (activeWeapon)
            {
                case Weapons.Blanket:
                    blankieController.StartAttack(inputMenager.mousePos);
                    actionCooldown = 0.3f;
                    break;
                case Weapons.Keyboard:
                    keyboardRotator.StartAttack(inputMenager.mousePos);
                    actionCooldown = 0.3f;
                    break;
                case Weapons.None:
                    interacter.Interact();
                    actionCooldown = 0.3f;
                    break;
            }
        }
    }

    private void SetAnim(Vector2 input)
    {
        if (input == Vector2.zero) return;

        float angle = Vector2.SignedAngle(Vector2.up, input);
        if (angle > -45 && angle <= 45) animator.SetInteger("WalkDir", 0);
        else if (angle > 45 && angle <= 135) animator.SetInteger("WalkDir", 3);
        else if (angle > 135 || angle <= -135) animator.SetInteger("WalkDir", 2);
        else animator.SetInteger("WalkDir", 1);
    }

    public void SetWeapon(Weapons weapon)
    {
        if (weapon != Weapons.None)
        {
            AudioManager.instance.PlaySound(pickUpSound);
            FindObjectOfType<WaveManager>().NextWave();
        }

        switch (activeWeapon)
        {
            case Weapons.Shard:
                shart.enabled = false;
                break;
            case Weapons.Pillow:
                pillow.enabled = false;
                break;
            case Weapons.Blanket:
                blankieController.gameObject.SetActive(false);
                break;
            case Weapons.Hanger:
                boomerang.gameObject.SetActive(false);
                break;
            case Weapons.Keyboard:
                keyboardRotator.gameObject.SetActive(false);
                break;
        }

        activeWeapon = weapon;

        switch (activeWeapon)
        {
            case Weapons.Shard:
                shart.enabled = true;
                break;
            case Weapons.Pillow:
                pillow.enabled = true;
                break;
            case Weapons.Blanket:
                blankieController.gameObject.SetActive(true);
                break;
            case Weapons.Hanger:
                boomerang.gameObject.SetActive(true);
                break;
            case Weapons.Keyboard:
                keyboardRotator.gameObject.SetActive(true);
                break;
        }
    }
}
