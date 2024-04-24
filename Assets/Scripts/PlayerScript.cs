using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    float knockbackForce = 16f;
    bool startHeartBeatSound = true;
    bool StoptHeartBeatSound = false;
    private PauseScript pauseScript;
    [Header("--------------Audio Manager--------------")]
    AudioManager audioManager;

    [Header("--------------Health--------------")]
    public Slider healthBar;
    public float healthSpeed;
    public Slider loosingHealthBar;
    public float loosingHealthSpeed;

    public Image fillHealthBar;
    public Image fillLostHealthBar;
    public float health;
    public float damage;

    [Header("--------------RigidBody--------------")]
    public Rigidbody2D rb;
    private Transform m_transform;

    [Header("--------------Animation--------------")]
    public Animator anim;
    public Animator healthAnim;

    [Header("--------------Food Manager--------------")]
    public FoodManager foodManager;
    public RandomSpawner randomSpawner;
    public Window_QuestPointer windowQuestPointer;

    [Header("--------------Move Speed--------------")]
    public float moveSpeed;

    [Header("--------------Sword Hit Box--------------")]
    public Transform swordHitBox;
    public LayerMask enemyLayer;
    public Collider2D[] enemiesToHit;
    public Collider2D enemyToHitNormal;
    public Collider2D enemyToHitTank;
    public float hitRadius;

    private Vector2 moveDirection;

    public bool canPlaySound = true;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }



    public void SwordAttack()
    {
        enemiesToHit = Physics2D.OverlapCircleAll(swordHitBox.position, hitRadius, enemyLayer);
        foreach (Collider2D enemyCollider in enemiesToHit)
        {
            EnemyScript enemy = enemyCollider.GetComponent<EnemyScript>();
            if (enemy != null && enemy.health > 0)
            {
                enemy.TakeDamage(damage);
                audioManager.Hit();
                StartCoroutine(EnemyKnockBack());
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    IEnumerator EnemyKnockBack()
    {
        enemyToHitNormal.GetComponent<Rigidbody2D>().AddForce(transform.right * 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        enemyToHitNormal.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.2f);

        if (enemyToHitNormal.GetComponent<EnemyScript>().health > 0)
        {
            enemyToHitNormal.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
    void Start()
    {
        m_transform = transform;
    }

    //IEnumerator FadeIn()
    //{
    //    audioManager.heartBeatSource.volume = 0;
    //    if (audioManager.heartBeatSource.volume <= 1)
    //    {
    //        audioManager.heartBeatSource.volume += Time.deltaTime*10;
    //    }
    //    yield return new WaitForSeconds(1f);
    //    startHeartBeatSound = false;
    //    StoptHeartBeatSound = false;
    //}

    void Update()
    {
        LAMouse();
        ProcessInput();
        Attack();

        if (health > 30 && health <= 50)
        {
            //if (startHeartBeatSound)
            //{
            //    audioManager.PlayHeartBeat();
            //}

            if (startHeartBeatSound)
            {
                audioManager.PlayHeartBeat();
                startHeartBeatSound = false;
                StoptHeartBeatSound = false;
            }

            healthAnim.SetBool("Half", true);
            healthAnim.SetBool("Low", false);
        }
        else if (health > 0 && health <= 30)
        {
            healthAnim.SetBool("Low", true);
            healthAnim.SetBool("Half", false);
        }
        else
        {
            StoptHeartBeatSound = true;
            healthAnim.SetBool("Half", false);
            healthAnim.SetBool("Low", false);
            if (StoptHeartBeatSound)
            {
                audioManager.StopHeartBeat();
                startHeartBeatSound = true;
            }
        }

        windowQuestPointer.targetPosition = windowQuestPointer.objectPosition.transform.position;
        windowQuestPointer.targetDistance = Vector2.Distance(transform.position, windowQuestPointer.objectPosition.position);

        if (windowQuestPointer.targetDistance <= windowQuestPointer.distance)
        {
            windowQuestPointer.Hide(true);
        }
        else
        {
            windowQuestPointer.Hide(false);
        }

        healthBar.value = Mathf.Lerp(healthBar.value, health, healthSpeed);
        loosingHealthBar.value = Mathf.Lerp(loosingHealthBar.value, health, loosingHealthSpeed);
        if (health > 75)
        {
            fillHealthBar.color = new Color32(21, 221, 0, 255);
            fillLostHealthBar.color = new Color32(236, 209, 0, 255);
        }
        else if (health <= 75 && health > 50)
        {
            fillHealthBar.color = new Color32(248, 245, 0, 255);
            fillLostHealthBar.color = new Color32(255, 120, 0, 255);
        }
        else if (health <= 50 && health > 25)
        {
            fillHealthBar.color = new Color32(255, 120, 0, 255);
            fillLostHealthBar.color = new Color32(200, 0, 0, 255);
        }
        else if (health <= 25 && health >= 0)
        {
            fillHealthBar.color = new Color32(200, 0, 0, 255);
            fillLostHealthBar.color = new Color32(90, 0, 0, 255);
        }
    }

    public void Attack()
    {
        bool isAttacking = Input.GetMouseButtonDown(0);

        if (isAttacking)
        {
            anim.SetTrigger("Attack");
        }
    }

    private void LAMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        m_transform.rotation = rotation;
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveX != 0 || moveY != 0)
        {
            if (!audioManager.SFXSource.isPlaying)
            {
                int randomIndex = Random.Range(0, audioManager.walking.Length);
                AudioClip randomClip = audioManager.walking[randomIndex];
                audioManager.PlaySFX(randomClip);
            }
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }



    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            if (foodManager.foodCount != foodManager.maxFoodCount)
            {
                foodManager.foodCount++;
                Destroy(collision.gameObject);
                randomSpawner.spawnCounter--;
                audioManager.PickUpSource();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(swordHitBox.transform.position, hitRadius);
    }
}
