# Grassn't

### About the project
Grassn't was a submission to June 2023 local gamejam completed in 48 hours of work. The topic was "pokój" (or peace / room in english), we approached the topic by designing a game about a teenager being forced out of his room by friends, family and grass trying to disturb his peace. 
As a beginner programmer I was supporting two more experienced programmers. I was responsible for all weapon scripts and some other minor scripts. 
Due to a silly friendly atmosphere some scripts may contain major spelling errors.

### How does it work
```csharp
public class Pillow : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private ParticleSystem feathers;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMenager playerMenager))
            return;

        RaycastHit2D[] pillowHits = Physics2D.CircleCastAll(transform.position, radius, Vector3.forward);
        foreach (RaycastHit2D hit in pillowHits)
        {
            if (hit.collider.gameObject.TryGetComponent(out PlayerMenager menager)) continue;
            if (hit.collider.gameObject.TryGetComponent(out Health health)) health.Damage();
        }
        ExplodePillow();
    }

    private void ExplodePillow()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        feathers.Play();
        Invoke("DestroyMe", 1f);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
```

This weapon is a projectile that explodes on hit and deals AoE damage. 
- When the pillow hits an enemy or an obstacle it creates an array of all objects in its hit radius.
- The foreach look checks each object for two components, firstly if it has a component exclusive to the player it ignores the object, otherwise it checks for the health component and deals damage.
- After checking all the object the method ExplodePillow is called. The renderer and collider get disabled, the particle effect of feathers starts playing and after a second the game object gets destroyed.
