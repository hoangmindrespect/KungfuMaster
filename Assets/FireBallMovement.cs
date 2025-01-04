using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    [Header("FireBall Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 3f;
    private Animator animator;
    private Vector2 direction;

    private void Start()
    {
        Destroy(gameObject, lifetime);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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
