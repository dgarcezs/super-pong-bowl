using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public int homeScore = 0;
    public int awayScore = 0;

    public Transform ball;
    public Text score;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleTouchdown();                
        UpdateScore();
    }

    private void HandleTouchdown()
    {
        float screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        if (ball.position.x < screenLeft)
        {
            awayScore += 7;            
            ResetBall();
        }
        if (ball.position.x > screenRight)
        {
            homeScore += 7;     
            ResetBall();       
        }
    }

    private void UpdateScore()
    {
        score.text = homeScore.ToString() + " x " + awayScore.ToString();
    }

    private void ResetBall()
    {
        ball.position = Vector3.zero;
        Ball ballReference = ball.GetComponent<Ball>();        
        ballReference.ballSpeed = 5; 
        ballReference.InvertDirection();
    }
}
