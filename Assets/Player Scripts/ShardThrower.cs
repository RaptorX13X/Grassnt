using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardThrower : MonoBehaviour
{
    [SerializeField] private GameObject shardPrefab;
    [SerializeField]private float cooldown;
    [SerializeField] private float vel;
    [SerializeField] private Rigidbody2D playerBody;
    private float timer;
    [SerializeField] private AudioClip yeetSound;

    public void ShardThrow(Vector3 mousePos)
    {
        if (timer >= 0)
            return;

        GameObject missile = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        missile.GetComponent<Rigidbody2D>().rotation = playerBody.rotation;
        timer = cooldown;
        var dir = (mousePos - transform.position).normalized;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, dir.y) * vel;
        missile.transform.up = dir;
        AudioManager.instance.PlaySound(yeetSound);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }
}
