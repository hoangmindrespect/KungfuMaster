using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb2D;
    private Transform player;
    private bool isRight = true;
    private int state;
    public LayerMask layerMask;
    public GameObject fireBall;
    public GameObject wingBall;
    private int totalFireballs = 8;
    private float fireBallSpeed = 5f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float minDistanceToFlip = 0.5f;

    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radiusAttack;
    [SerializeField] private int damageCaused;
    private float x;
    private float y;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(StateHandler());
    }

    private void Update()
    {
        CheckPlayerDirection();
    }

    private void FixedUpdate()
    {
        if (state == 1)
        {
            FollowPlayerAndUseMeele();
        }
    }

    private void CheckPlayerDirection()
    {
        float directionToPlayer = player.position.x - transform.position.x;

        if (Mathf.Abs(directionToPlayer) > minDistanceToFlip)
        {
            if ((directionToPlayer > 0 && !isRight) || (directionToPlayer < 0 && isRight))
            {
                Flip();
            }
        }
    }

    private void FollowPlayerAndUseMeele()
    {
        Vector2 targetPosition = new Vector2(player.position.x, rb2D.position.y);
        float step = moveSpeed * Time.fixedDeltaTime;
        rb2D.position = Vector2.MoveTowards(rb2D.position, targetPosition, step);
    }

    private void Flip()
    {
        isRight = !isRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private IEnumerator StateHandler()
    {
        while (true)
        {
            state = Random.Range(1, 3);
            animator.SetInteger("State", state);

            if (state == 1)
            {
                yield return new WaitForSeconds(4f);
            }
            else if (state == 2)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void IdleAndUseSpecialSkill()
    {
        int r = Random.Range(1, 3);
        if (r == 1)
        {
            UseSpectialSkill1();
        }
        else
        {
            UseSpectialSkill2();
        }
    }

    public void UseSpectialSkill2()
    {
        if (isRight)
        {
            x = transform.position.x + 6.0f;
        }
        else
        {
            x = transform.position.x - 6.0f;
        }
        y = transform.position.y + 2.5f;
        float angleStep = 360f / totalFireballs;
        float radius = 0.5f;

        for (int i = 0; i < totalFireballs; i++)
        {
            float angle = i * angleStep;
            float radian = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
            Vector2 spawnPosition = new Vector2(x, y) + direction * radius;

            GameObject newFireBall = Instantiate(fireBall, spawnPosition, Quaternion.identity);

            FireBallMovement fireBallScript = newFireBall.GetComponent<FireBallMovement>();
            if (fireBallScript != null)
            {
                fireBallScript.SetDirection(direction);
            }
        }
    }
    public void UseSpectialSkill1()
    {
        if (isRight)
        {
            x = transform.position.x + 6.0f;
        }
        else
        {
            x = transform.position.x - 6.0f;
        }
        y = transform.position.y + 2.5f;
        int totalElectricBalls = 1;
        float radius = 1.5f;

        for (int i = 0; i < totalElectricBalls; i++)
        {
            float angle = i * Mathf.PI;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            Vector2 spawnPosition = new Vector2(x, y) + offset;

            GameObject newElectricBall = Instantiate(wingBall, spawnPosition, Quaternion.identity);

            WingBallController electricBallScript = newElectricBall.GetComponent<WingBallController>();
            if (electricBallScript == null)
            {
                Debug.LogError("ElectricBall script is missing on the prefab!");
            }
        }
    }

    public void OnAttack()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(attackPoint.position, radiusAttack, layerMask);
        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<Player>().TakeDamage(damageCaused);
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, radiusAttack);
    }
}
