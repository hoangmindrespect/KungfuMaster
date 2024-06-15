using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerAttackControl : MonoBehaviour
{
    [SerializeField] private int damageCaused;
    [SerializeField] private Vector2 dimensionesBox;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float tiempoDeVida;
    public LayerMask layerMask;

    private void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    public void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(attackPoint.position, dimensionesBox, 0f, layerMask);
        foreach (Collider2D colisiones in objetos)
        {
            if (colisiones.CompareTag("Player"))
            {
                colisiones.GetComponent<Player>().TakeDamage(damageCaused);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackPoint.position, dimensionesBox);
    }
}
