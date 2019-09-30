using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Player Components
    public Rigidbody2D playerRigidbody;
    public SpriteRenderer spriteRenderer;
    public bool controlBase = true;
    public BulletController original;
    public float speedMultiplier;
    private float currHealth = 0;
    private float energy = 0;
    public LaserController laser;
    public Text score;
    public Image scoreImg;
    private int cooldown = 0;
    private GameManager gm;

    [SerializeField]
    [Tooltip("maximum health")]
    private float health = 10;

    [SerializeField]
    [Tooltip("distance from player the bullet spawns")]
    private float bulletDistance;

    [SerializeField]
    [Tooltip("health bar")]
    private Slider slide;

    [SerializeField]
    [Tooltip("how much health player takes from bumping into enemy")]
    private int hurt;

    [SerializeField]
    [Tooltip("how many hits the player needs to recharge bullet")]
    private float energyRequired = 10;

    [SerializeField]
    [Tooltip("energy bar")]
    private Slider energySlide;

    [SerializeField]
    [Tooltip("cooldown timer for lasers")]
    private int cooldownRequired;

    #endregion

    #region Player Sprites
    [SerializeField]
    [Tooltip("front")]
    private Sprite front;
    [SerializeField]
    [Tooltip("back")]
    private Sprite back;
    [SerializeField]
    [Tooltip("left")]
    private Sprite left;
    [SerializeField]
    [Tooltip("right")]
    private Sprite right;
    [SerializeField]
    [Tooltip("attack")]
    private Sprite attack;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        currHealth = health;
        score = GameObject.FindGameObjectsWithTag("Combo")[0].GetComponent<Text>();
        scoreImg = GameObject.FindGameObjectsWithTag("ScoreImg")[0].GetComponent<Image>();
        score.enabled = false;
        scoreImg.enabled = false;
        energy = energyRequired;
        cooldown = cooldownRequired;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlBase)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            playerRigidbody.velocity = movement * speedMultiplier;

            if (moveHorizontal > 0)
            {
                spriteRenderer.sprite = right;
            }
            else if (moveHorizontal < 0)
            {
                spriteRenderer.sprite = left;
            }
            else if (moveVertical < 0)
            {
                spriteRenderer.sprite = front;
            }
            else if (moveVertical > 0)
            {
                spriteRenderer.sprite = back;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && energy == energyRequired)
        {
            if (controlBase)
            {
                controlBase = false;
                Vector3 offset;
                Quaternion bulletRotation = new Quaternion();
                if (spriteRenderer.sprite == right)
                {
                    offset = transform.position + transform.right * bulletDistance;
                    bulletRotation = new Quaternion();
                }
                else if (spriteRenderer.sprite == left)
                {
                    offset = transform.position - transform.right * bulletDistance;
                    bulletRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                }
                else if (spriteRenderer.sprite == back)
                {
                    offset = transform.position + transform.up * bulletDistance;
                    bulletRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                else
                {
                    offset = transform.position - transform.up * bulletDistance;
                    bulletRotation = Quaternion.Euler(new Vector3(0, 0, 270));
                }
                Instantiate<BulletController>(original, offset, bulletRotation, transform);
                spriteRenderer.sprite = attack;
                playerRigidbody.bodyType = RigidbodyType2D.Static;
                energy = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && cooldown == cooldownRequired)
        {
            if (controlBase)
            {
                Vector3 offset;
                Quaternion laserRotation = new Quaternion();
                if (spriteRenderer.sprite == right)
                {
                    offset = transform.position + transform.right * bulletDistance;
                    laserRotation = new Quaternion();
                }
                else if (spriteRenderer.sprite == left)
                {
                    offset = transform.position - transform.right * bulletDistance;
                    laserRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                }
                else if (spriteRenderer.sprite == back)
                {
                    offset = transform.position + transform.up * bulletDistance;
                    laserRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                else
                {
                    offset = transform.position - transform.up * bulletDistance;
                    laserRotation = Quaternion.Euler(new Vector3(0, 0, 270));
                }
                Instantiate<LaserController>(laser, offset, laserRotation, transform);
                cooldown = 0;
            }
        }

        // Update health bar
        slide.value = currHealth / health;
        energySlide.value = energy / energyRequired;
        cooldown += 1;
        cooldown = Mathf.Min(cooldown, cooldownRequired);
        
        // End Game
        if (slide.value <= 0)
        {
            gm.LoseGame();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            playerRigidbody.velocity = Vector2.zero;
        }
        if (other.gameObject.tag == "Enemy")
        {
            DecreaseHealth(1);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Bullet")
        {
            energy = energyRequired;
        }
    }

    public void DecreaseHealth(float value)
    {
        currHealth -= value;
    }

    public void DecreaseCooldown()
    {
        energy += 1;
        energy = Mathf.Min(energy, energyRequired);
    }

}
