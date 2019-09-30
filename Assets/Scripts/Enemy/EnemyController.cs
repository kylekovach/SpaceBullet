using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("How much health this enemy has")]
    private int m_MaxHealth;

    [SerializeField]
    [Tooltip("How fast the enemy gonna reload")]
    private float m_TimeToReload;

    [SerializeField]
    [Tooltip("How fast the enemy can move")]
    private float m_Speed;

    [SerializeField]
    [Tooltip("The area where the enemy spawns on X")]
    private float m_BoundX;

    [SerializeField]
    [Tooltip("The area where the enemy spawns on Y")]
    private float m_BoundY;

    [SerializeField]
    [Tooltip("The fire mode sprite")]
    private Sprite m_FireMode;

    [SerializeField]
    [Tooltip("the static sprite")]
    private Sprite m_StaticMode;

    [SerializeField]
    [Tooltip("Game object of the enemy bullet")]
    private EnemyLaserController m_EnemyLaser;

    [SerializeField]
    [Tooltip("How long for enemy to walk at a random direction")]
    private float reset_time;

    [SerializeField]
    [Tooltip("Sprite Left")]
    private Sprite left;

    [SerializeField]
    [Tooltip("Sprite Right")]
    private Sprite right;

    [SerializeField]
    private int m_Score;
    #endregion

    #region Private Variables
    private float p_curhealth;
    private float p_reloadTimer;
    private Vector2 random_dir;
    private float current_reset_time = 0;
    #endregion

    #region Public Variables
    public bool to_player;
    public bool shoot;
    #endregion

    #region Cached Component
    private Rigidbody2D cc_Rb;
    private SpriteRenderer cc_Sr;
    #endregion

    #region Cached References
    private Transform cr_Player;
    #endregion

    #region Initialization
    private void Awake()
    {
        p_curhealth = m_MaxHealth;

        cc_Rb = GetComponent<Rigidbody2D>();
        cc_Sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.position = new Vector2(Random.Range(-m_BoundX, m_BoundX), Random.Range(-m_BoundY, m_BoundY));
        cr_Player = FindObjectOfType<PlayerController>().transform;
    }
    #endregion

    #region Main Updates
    private void Update()
    {
        if (shoot)
        {
            cc_Sr.sprite = m_FireMode;
        } else
        {
            cc_Sr.sprite = m_StaticMode;
        }

        // If no player near, move randomly and shoot at player direction. Otherwise, move towards player 
        if (to_player)
        {
            Vector2 dir = cr_Player.position - transform.position;
            dir.Normalize();
            cc_Rb.MovePosition(cc_Rb.position + dir * m_Speed * Time.fixedDeltaTime);
            if (dir.x > 0 && !shoot)
            {
                cc_Sr.sprite = right;
            }
            else if (!shoot)
            {
                cc_Sr.sprite = left;
            }
        }
        else
        {
            if (current_reset_time <= 0)
            {
                random_dir = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                current_reset_time = reset_time;
            }
            else
            {
                current_reset_time -= Time.deltaTime;
            }
            random_dir.Normalize();
            cc_Rb.velocity = random_dir * m_Speed;

            if (random_dir.x > 0)
            {
                cc_Sr.sprite = right;
            }
            else
            {
                cc_Sr.sprite = left;
            }
        }
    }
    #endregion


    #region Fire
    private void FixedUpdate()
    {
        if (p_reloadTimer > m_TimeToReload)
        {
            shoot = false;
            cc_Rb.bodyType = RigidbodyType2D.Dynamic;

            float x = cr_Player.transform.position.x - transform.position.x;
            float y = cr_Player.transform.position.y - transform.position.y;
            
            to_player = true;
            p_reloadTimer = 0; 
            float angle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
            Quaternion laserRotatioin = Quaternion.Euler(new Vector3(0, 0, angle));
            Instantiate<EnemyLaserController>(m_EnemyLaser, transform.position, laserRotatioin, transform);
        }
        else if (p_reloadTimer >= m_TimeToReload * 6 /10 && p_reloadTimer <= m_TimeToReload)
        {
            shoot = true;
            p_reloadTimer += Time.fixedDeltaTime;
            cc_Rb.bodyType = RigidbodyType2D.Static;
        } else
        {
            p_reloadTimer += Time.fixedDeltaTime;
        }
    }
    #endregion

    #region Health Methods
    public void DecreaseHealth(float amount)
    {
        p_curhealth -= amount;
        if (p_curhealth <= 0)
        {
       
            Destroy(gameObject);
        }
    }
    #endregion
}
