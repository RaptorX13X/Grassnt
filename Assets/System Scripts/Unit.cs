using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Unit : MonoBehaviour
{
    [SerializeField] private AUnitState[] possibleStates;
    private AUnitState currentState;
    private WaveManager waveManager;
    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sR;

    public PlayerMenager PlayerReference => waveManager.Player;
    public Rigidbody2D Rigidbody2D => rb;
    public SpriteRenderer SpriteRenderer => sR;
    public AUnitState CurrentState => currentState;

    public void Init(WaveManager waveManager)
    {
        this.waveManager = waveManager;
    }

    private void Start()
    {
        for (int i = 0; i < possibleStates.Length; i++)
        {
            possibleStates[i] = Instantiate(possibleStates[i]);
        }

        GetComponent<Health>().OnDeath += Die;

        rb = GetComponent<Rigidbody2D>();

        SwitchState(0);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnterAction(this, collision);
    }

    public void SwitchState(int newStateId)
    {
        currentState = possibleStates[newStateId];
        currentState.EnterState(this);
    }

    private void Die()
    {
        waveManager.EnemiesAlive--;
        Debug.Log(waveManager.EnemiesAlive);
        Destroy(gameObject);
    }
}
