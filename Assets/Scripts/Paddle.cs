using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public float speed = 5f;
    public Key inputKeyUp = Key.W;
    public Key inputKeyDown = Key.S;

    private void Update()
    {
        Move();        
    }

    private void Move()
    {
        transform.Translate(0f, GetMovementFromInput(), 0f);    
        transform.position = ProcessMovementLimit(transform.position);
    }


    private float GetMovementFromInput()
    {
        float movement = 0;
        if (Keyboard.current[inputKeyUp].isPressed)
        {
            movement = 1;
        }

        if (Keyboard.current[inputKeyDown].isPressed)
        {
            movement = -1;
        }
        return movement * speed *  Time.deltaTime;
    }

    private Vector3 ProcessMovementLimit(Vector3 actualPosition)
    {
        float maxPositionY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float minPositionY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        float height = transform.localScale.y / 2;

        actualPosition.y = Mathf.Clamp(actualPosition.y, minPositionY + height, maxPositionY - height);
        return actualPosition;
    }

}
