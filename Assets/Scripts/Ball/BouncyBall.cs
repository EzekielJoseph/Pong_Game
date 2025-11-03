using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = -15f;

    Rigidbody2D rb;

    int score = 0;  
    int lives = 3;

    public TextMeshProUGUI scoreText;
    public GameObject[] livesImage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
            if (lives <= 0)
            {
                GameOver();
                return;
            }
            else 
            {
                transform.position = Vector3.zero;
                rb.velocity = Vector3.zero;
                lives--;
                livesImage[lives].SetActive(false);
            }
        }

        if(rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            score += 10;
            scoreText.text = "Score: " + score.ToString("0000");
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
