using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialBallSpeed = 5f;
    private float ballSpeed;
    
    public Transform paddleLeft;
    public Transform paddleRight;
    
    
    private Vector2 direction = Vector2.one;
    private void Start()
    {
        ballSpeed = initialBallSpeed;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = Time.deltaTime * ballSpeed * direction;
        transform.Translate(movement);                
        HandleVerticalFieldColision();
        HandlePaddleColision();
    }

    private void HandleVerticalFieldColision()
    {
        Vector3 position = transform.position;
        float screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float screenBotton = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        if (direction.y > 0 && position.y >= (screenTop - 0.25f))
        {
            direction.y = -AngleDirection();
            ballSpeed += 0.10f;
        }

        if (direction.y < 0 && position.y <= (screenBotton + 0.25f))
        {
            direction.y = AngleDirection();
            ballSpeed += 0.10f;
        }
    }

    private void HandlePaddleColision()
    {
        if (direction.x > 0)
        {
            // Handle Paddle Right Colision
            if ((transform.position.x + (transform.localScale.x / 2f)) > (paddleRight.position.x - (paddleLeft.localScale.x / 2f))
                && (transform.position.x + (transform.localScale.x / 2f)) < (paddleRight.position.x + (paddleLeft.localScale.x / 2f))
                && (transform.position.y > paddleRight.position.y - paddleRight.localScale.y / 2f)
                && (transform.position.y < paddleRight.position.y + paddleRight.localScale.y / 2f))
            {
                InvertDirection();                            
            }

        } else if (direction.x < 0)
        {
            // Handle PAddle Left Colision            
            if ((transform.position.x - (transform.localScale.x / 2f)) < (paddleLeft.position.x + (paddleLeft.localScale.x / 2f))
                && (transform.position.x - (transform.localScale.x / 2f)) > (paddleLeft.position.x - (paddleLeft.localScale.x / 2f))
                && (transform.position.y > paddleLeft.position.y - paddleLeft.localScale.y / 2f)
                && (transform.position.y < paddleLeft.position.y + paddleLeft.localScale.y / 2f))
            {
                InvertDirection();
            }
        }
    }

    public void ResetToInitialSpeed()
    {
        ballSpeed = initialBallSpeed;
    }

    public void InvertDirection()
    {
        direction.x = -direction.x;        
    }

    private float AngleDirection()
    {
        return Random.Range(0.5f, 1.5f);
    }



}
