using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerMovement : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb2D;
    public Transform player;
    private bool detectedPlayer = true;
    public LayerMask layerMask;

    [Header("Health")]
    [SerializeField] private float HP;
    //[SerializeField] private BarraDeVida barraDeVida;
    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radiusAttack;
    [SerializeField] private int damageCaused;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        //barraDeVida.InicializarBarraDeVida(vida);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("attackPlayer", distanceToPlayer);
    }
    public void damagedByPlayer(float dps)
    {
        HP -= dps;
        //barraDeVida.CambiarVidaActual(vida);
        if (HP <= 0)
        {
            animator.SetTrigger("Muerte");
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }

    public void DetectPlayer()
    {
        if ((player.position.x > transform.position.x && !detectedPlayer) || (player.position.x < transform.position.x && detectedPlayer))
        {
            detectedPlayer = !detectedPlayer;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180.0f, 0);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, radiusAttack);
    }
}

