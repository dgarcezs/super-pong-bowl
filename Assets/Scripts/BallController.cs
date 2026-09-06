using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialBallSpeed = 5f;
    private float ballSpeed;

    public bool IsMoving { get; private set; } = false;
    
    public Transform paddleLeft;
    public Transform paddleRight;
    
    
    public Vector2 Direction {get; private set;} = Vector2.one;
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
        if (IsMoving) 
        {
            Vector3 movement = Time.deltaTime * ballSpeed * Direction;
            transform.Translate(movement);                
            HandleVerticalFieldColision();
            HandlePaddleColision();
        }
    }

    private void HandleVerticalFieldColision()
    {
        Vector3 position = transform.position;
        float screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float screenBotton = Camera.main.ScreenToWorldPoint(new Vector3(0, 60, 0)).y;

        if (Direction.y > 0 && position.y >= (screenTop - 0.25f))
        {
            Direction = new Vector2(Direction.x, -AngleDirection());            
        }

        if (Direction.y < 0 && position.y <= (screenBotton + 0.25f))
        {
            Direction = new Vector2(Direction.x, AngleDirection());            
        }
    }

    private void HandlePaddleColision()
    {
        if (Direction.x > 0)
        {
            // Handle Paddle Right Colision
            if ((transform.position.x + (transform.localScale.x / 2f)) > (paddleRight.position.x - (paddleLeft.localScale.x / 2f))
                && (transform.position.x + (transform.localScale.x / 2f)) < (paddleRight.position.x + (paddleLeft.localScale.x / 2f))
                && (transform.position.y > paddleRight.position.y - paddleRight.localScale.y / 2f)
                && (transform.position.y < paddleRight.position.y + paddleRight.localScale.y / 2f))
            {
                InvertDirection();
                ballSpeed += 0.10f;                            
            }

        } else if (Direction.x < 0)
        {
            // Handle PAddle Left Colision            
            if ((transform.position.x - (transform.localScale.x / 2f)) < (paddleLeft.position.x + (paddleLeft.localScale.x / 2f))
                && (transform.position.x - (transform.localScale.x / 2f)) > (paddleLeft.position.x - (paddleLeft.localScale.x / 2f))
                && (transform.position.y > paddleLeft.position.y - paddleLeft.localScale.y / 2f)
                && (transform.position.y < paddleLeft.position.y + paddleLeft.localScale.y / 2f))
            {
                InvertDirection();
                ballSpeed += 0.10f;
            }
        }
    }

    public void ResetToInitialSpeed()
    {
        ballSpeed = initialBallSpeed;
    }

    public void InvertDirection()
    {
        Direction = new Vector2(-Direction.x, Direction.y);        
    }
    public void StartBall()
    {
        IsMoving = true;
    }

    public void StopBall()
    {
        IsMoving = false;
    }

    private float AngleDirection()
    {
        return Random.Range(0.5f, 1.5f);
    }

}