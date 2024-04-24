using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health;
    public float damage;
    public float attackDistance;

    public PlayerScript playerScript;

    public Transform player;
    public Rigidbody2D rb;
    public Animator anim;
    public float distance;
    public float moveSpeed;
    public float followDistance;
    public bool isInView = false;
    public float inViewTime = 0f;

    public Transform swordHitBox;
    public LayerMask playerLayer;
    public float hitRadius;

    bool canAttack = true;
    public AudioManager audioManager; // Add this line

    // Spawn a new enemy when this enemy is destroyed
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }

    public void SwordAttack()
    {
        Collider2D[] playerToHit = Physics2D.OverlapCircleAll(swordHitBox.position, hitRadius, playerLayer);
        foreach (Collider2D playerCollider in playerToHit)
        {
            PlayerScript player = playerCollider.GetComponent<PlayerScript>();
            if (player != null && player.health > 0)
            {
                if (PlayerPrefs.GetInt("NormalDifficulty") != 1)
                {
                    audioManager.Hit();
                    player.TakeDamage(damage + 10);
                }
                else
                {
                    audioManager.Hit();
                    player.TakeDamage(damage);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            rb.velocity = new Vector2(0, 0);
            Destroy(gameObject);
            audioManager.Death();
        }
    }

    private void OnDestroy()
    {
        // Spawn a new enemy at a random position
        Vector2 spawnPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        GameObject newEnemy = Instantiate(gameObject, spawnPosition, Quaternion.identity);
    }

    void Start()
    {

    }

    void Update()
    {
        LookAt();
        distance = Vector2.Distance(transform.position, player.position);
        Vector2 targetPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (distance < followDistance && distance > 2.2f)
        {
            anim.SetBool("Walk", true);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (!audioManager.SFXSource.isPlaying)
            {
                int randomIndex = Random.Range(0, audioManager.walking.Length);
                AudioClip randomClip = audioManager.walking[randomIndex];
                audioManager.PlaySFX(randomClip);
            }
        }
        else
        {
            anim.SetBool("Walk", false);
            if (attackDistance >= distance && canAttack)
            {
                anim.SetTrigger("Attack");
                StartCoroutine(AttackCooldown());
            }
        }
    }

    public void LookAt()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
