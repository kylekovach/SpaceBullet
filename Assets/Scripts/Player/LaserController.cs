using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    Rigidbody2D rb;
    public float forwardSpeed;
    private GameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 forwardMovement = transform.right * forwardSpeed;
        rb.velocity = forwardMovement;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            gm.score += 1;
            Destroy(this.gameObject);
            GetComponentInParent<PlayerController>().DecreaseCooldown();
        }
    }
}
