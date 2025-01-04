using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingBallController : MonoBehaviour
{
    [Header("Wing Ball Settings")]
    [SerializeField] private float speed = 8f; // Tốc độ di chuyển
    [SerializeField] private float rotationSpeed = 100f; // Tốc độ quay
    [SerializeField] private float lifetime = 5f; // Thời gian tồn tại
    private Animator animator;
    private Transform player; // Tham chiếu tới Player
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            Vector2 perpendicular = Vector2.Perpendicular(direction) * Mathf.Sin(Time.time * rotationSpeed);

            Vector2 combinedMovement = direction + perpendicular;

            rb.velocity = combinedMovement.normalized * speed;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ElectricBall hit the Player!");
            other.GetComponent<Player>().TakeDamage(10);
            animator.SetBool("isBreak", true);
            StartCoroutine(DestroyAfterDelay(0.1f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Chờ 0.1 giây
        Destroy(gameObject); // Hủy đối tượng sau delay
    }
}
