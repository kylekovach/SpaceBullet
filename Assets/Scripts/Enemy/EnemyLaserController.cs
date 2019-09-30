using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("how much damage the laser does")]
    private float damage = 0.5f;


    Rigidbody2D rb;
    public float forwardSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 forwardMovement = transform.right * forwardSpeed;
        rb.velocity = forwardMovement;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().DecreaseHealth(damage);
            Destroy(this.gameObject);
        }
    }
}
