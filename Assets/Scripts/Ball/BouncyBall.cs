using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = 10f;
    float initialSpeed = 6;

    Rigidbody2D rb;

    int score = 0;
    int lives = 3;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalscoreGOText;
    public TextMeshProUGUI finalscoreWINText;
    public TextMeshProUGUI countdownText;

    public GameObject[] livesImage;
    public GameObject gameOverPanel;
    public GameObject winPanel; // new panel for winning

    bool isRespawning = false;
    bool gameEnded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        finalscoreGOText.gameObject.SetActive(false);
        finalscoreWINText.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);

        rb.AddForce(new Vector2(initialSpeed, initialSpeed), ForceMode2D.Impulse);
    }

    void Update()
    {
        if (gameEnded) return;

        if (!isRespawning && transform.position.y < minY)
        {
            if (lives <= 0)
            {
                GameOver();
            }
            else
            {
                lives--;
                livesImage[lives].SetActive(false);
                StartCoroutine(RespawnDelay());
            }
        }

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }

        // Check if all bricks are destroyed
        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0)
        {
            WinGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            score += 10;
            scoreText.text = "Score: " + score.ToString("0000");
        }
    }

    IEnumerator RespawnDelay()
    {
        isRespawning = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = Vector3.zero;

        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = "Respawning in " + i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.gameObject.SetActive(false);
        rb.isKinematic = false;
        isRespawning = false;
        rb.AddForce(new Vector2(initialSpeed, initialSpeed), ForceMode2D.Impulse);
    }

    void GameOver()
    {
        gameEnded = true;
        gameOverPanel.SetActive(true);
        finalscoreGOText.gameObject.SetActive(true);
        finalscoreGOText.text = "Final Score  " + score.ToString("0000");
        scoreText.gameObject.SetActive(false);

        Time.timeScale = 0f;
        Destroy(gameObject);
    }

    void WinGame()
    {
        gameEnded = true;
        winPanel.SetActive(true);
        finalscoreWINText.gameObject.SetActive(true);
        finalscoreWINText.text = "Final Score  " + score.ToString("0000");
        scoreText.gameObject.SetActive(false);
        
        Time.timeScale = 0f;
        Destroy(gameObject);
    }
}
