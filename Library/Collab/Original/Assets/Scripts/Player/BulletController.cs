using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;
    public float forwardSpeed;
    public float turnMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 forwardMovement = new Vector2(forwardSpeed, 0);
        rb.velocity = forwardMovement;
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.angularVelocity = -move * turnMultiplier;
        rb.velocity = transform.right * forwardSpeed;
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
            GetComponentInParent<PlayerController>().playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
            GetComponentInParent<PlayerController>().controlBase = true;
        }
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            forwardSpeed += 1;
        }
        if (other.gameObject.tag == "Player")
        {
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

    private void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
