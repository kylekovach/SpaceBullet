using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;
using UnityEngine.UI;


public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;
    public float forwardSpeed;
    public float turnMultiplier;
    private int kill = 0;
    public Text score;
    public Image scoreImg;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 forwardMovement = new Vector2(forwardSpeed, 0);
        rb.velocity = forwardMovement;
        score = GameObject.FindGameObjectsWithTag("Combo")[0].GetComponent<Text>();
        scoreImg = GameObject.FindGameObjectsWithTag("ScoreImg")[0].GetComponent<Image>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.angularVelocity = -move * turnMultiplier;
        rb.velocity = transform.right * forwardSpeed;
        if (kill > 2)
        {
            score.text = kill.ToString();
            score.enabled = true;
            scoreImg.enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            score.enabled = false;
            scoreImg.enabled = false;
            gm.score += kill;
            Destroy(this.gameObject);
            GetComponentInParent<PlayerController>().playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
            GetComponentInParent<PlayerController>().controlBase = true;
        }
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            kill += 1;
            forwardSpeed += 1;
        }
        if (other.gameObject.tag == "Player")
        {
            score.enabled = false;
            scoreImg.enabled = false;
            gm.score += kill;
            Destroy(this.gameObject);
            other.gameObject.GetComponent<PlayerController>().playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
            other.gameObject.GetComponent<PlayerController>().controlBase = true;
        }
    }

    Vector2 rotate(Vector2 localCoords, float degrees)
    {
        Vector2 result = new Vector2();
        float x = localCoords.x;
        float y = localCoords.y;
        result.x = x * Cos(degrees) - y * Sin(degrees);
        result.y = x * Sin(degrees) + y * Cos(degrees);
        return result;
    }
}