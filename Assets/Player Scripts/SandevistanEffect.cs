using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandevistanEffect : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> ghosts;
    [SerializeField] private List<SpriteRenderer> usedGhosts;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Color color;
    [SerializeField] private float cooldown;
    [SerializeField] private float duration;
    [SerializeField] private DashController dc;
    private float  timer = 0;
    public void UpdateSDVSTN(Vector2 input)
    {
        if (!dc.dashing) return;
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            timer = 0;
            var ghost = ghosts[ghosts.Count - 1];
            ghosts.Remove(ghost);
            usedGhosts.Add(ghost);
            ghost.transform.position = transform.position;
            ghost.gameObject.SetActive(true);
            float angle = Vector2.SignedAngle(Vector2.up, input);
            if (angle > -45 && angle <= 45) ghost.sprite = sprites[0];
            else if (angle > 45 && angle <= 135) ghost.sprite = sprites[3];
            else if (angle > 135 || angle <= -135) ghost.sprite = sprites[2];
            else ghost.sprite = sprites[1];
            ghost.color = color;
        }

    }
    private void Update()
    {
        List<SpriteRenderer> changed = new List<SpriteRenderer>();
        foreach(var ghost in usedGhosts)
        {
            var alpha = ghost.color.a;
            alpha -= Time.deltaTime / duration;
            if (alpha < 0) changed.Add(ghost);
            else ghost.color = new Color(ghost.color.r, ghost.color.g, ghost.color.b, alpha);
        }

        foreach(var ghost in changed)
        {
            ghost.gameObject.SetActive(false);
            ghosts.Add(ghost);
            usedGhosts.Remove(ghost);
        }
    }
}
