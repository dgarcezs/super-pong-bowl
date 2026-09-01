using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Text score;
    public Text clock;
    public Text quarters;

    private int quarter = 1;
    private float playClock = 15 * 60;

    private bool isClockRunning = false;



    public int homeScore = 0;
    public int awayScore = 0;    
    private readonly int clockSpeed = 10;


    void Update()
    {
        KickOffGame();
        HandleTouchdown();                
        UpdateScore();
        CountdownPlayClock();
    }

    private void HandleTouchdown()
    {
        float screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        if (ball.transform.position.x < screenLeft)
        {
            awayScore += 7;            
            ResetBall();
            HoldGame();
            Invoke(nameof(ResumeGame), 2f);
        }
        if (ball.transform.position.x > screenRight)
        {
            homeScore += 7;     
            ResetBall();       
            HoldGame();
            Invoke(nameof(ResumeGame), 2f);
        }
    }

    private void UpdateScore()
    {
        score.text = homeScore.ToString() + " x " + awayScore.ToString();
    }

    private void ResetBall()
    {
        ball.transform.position = Vector3.zero;
        ball.ResetToInitialSpeed(); 
        ball.InvertDirection();
        ball.StopBall();
    }

    private void CountdownPlayClock()
    {
        if (isClockRunning)
        {
            if (playClock > 0)
            {
                playClock -= Time.deltaTime * clockSpeed;
                playClock = playClock < 0 ? 0 : playClock;
                UpdatePlayClock();
            } else
            {
                HandleEndQuarter();
            }
        }
    }

    private void UpdatePlayClock()
    {
        int minutes = Mathf.FloorToInt(playClock / 60);
        int seconds = Mathf.FloorToInt(playClock % 60);

        clock.text = $"{minutes:00}:{seconds:00}";
    }

    private void HandleEndQuarter()
    {
        if (quarter < 4)
        {
            quarter++;
            ResetQuarter();
        }
        else
        {
            EndGame();
        }        
    }

    private void ResetQuarter()
    {
        ResetBall();
        HoldGame();
        playClock = 15 * 60;        
        UpdatePlayClock();
        UpdatePlayQuarter();
    }

    private void UpdatePlayQuarter()
    {

        quarters.text = quarter switch
        {
            1 => "1st Quarter",
            2 => "2nd Quarter",
            3 => "3rd Quarter",
            4 => "4rd Quarter",
            _ => "1st Quarter",
        };
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
    }    

    private void KickOffGame() 
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            isClockRunning = true;
            ball.StartBall();
        }
    }

    private void ResumeGame()
    {
        isClockRunning = true;
        ball.StartBall();

    }

    private void HoldGame()
    {
        isClockRunning = false;
        ball.StopBall();
    }

}
