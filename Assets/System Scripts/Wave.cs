using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "SO/Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] private Unit[] units;
    [SerializeField] private float delayInSeconds = 0.5f;

    public void StartWave(WaveManager waveManager)
    {
        waveManager.StartCoroutine(SpawnWave(waveManager));
    }

    private IEnumerator SpawnWave(WaveManager waveManager)
    {
        foreach (var unit in units)
        {
            Unit instantiated = Instantiate(unit, waveManager.EnemiesSpawnPoint, Quaternion.identity, waveManager.EnemiesParent);
            instantiated.Init(waveManager);

            waveManager.EnemiesAlive++;
            yield return new WaitForSeconds(delayInSeconds);
        }
    }
}
