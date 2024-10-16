using UnityEngine;

public class SpriteLayerer : MonoBehaviour
{
    [SerializeField] private float wiggleRoom;
    private Transform playerTransform;
    private Transform tr;
    [SerializeField] private SpriteRenderer sR;

    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMenager>().transform;
        tr = transform;
    }

    void Update()
    {
        if(Mathf.Abs(playerTransform.position.y - tr.position.y) > wiggleRoom)
        {
            sR.sortingOrder = playerTransform.position.y > tr.position.y ? 6 : 4;
        }
    }
}
