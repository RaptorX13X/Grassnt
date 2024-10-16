using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputMenager : MonoBehaviour
{
    public Vector2 input { private set; get; }
    public bool action { private set; get; }
    public Vector3 mousePos;
    public bool dashPressed;
    [SerializeField] private GameUIManager gameUIManager;

    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        action = Input.GetKey(KeyCode.E);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        dashPressed = dashPressed || Input.GetKeyDown(KeyCode.Space);
        if(!gameUIManager.Playing)
        {
            input = Vector2.zero;
            action = false;
            dashPressed = false;
        }
    }

}
