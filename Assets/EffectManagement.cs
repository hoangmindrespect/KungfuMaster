using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManagement : MonoBehaviour
{

    [Header("----------Coins----------")]
    public GameObject redCoin;
    public GameObject blueCoin;

    public void GenerateCoinDestroyCD(float x, float y)
    {
        GameObject B = Instantiate(redCoin, new Vector3(x, y, 0), Quaternion.identity);
        GameObject C = Instantiate(redCoin, new Vector3(x, y, 0), Quaternion.identity);

        Rigidbody2D rbB = B.GetComponent<Rigidbody2D>();
        Rigidbody2D rbC = C.GetComponent<Rigidbody2D>();

        if (rbB != null)
        {
            rbB.gravityScale = 1;
            rbB.velocity = new Vector2(-3, -3);
        }

        if (rbC != null)
        {
            rbC.gravityScale = 1;
            rbC.velocity = new Vector2(3, -3);
        }
    }

    public void GenerateCoinDestroySkeleton(float x, float y)
    {
        GameObject A = Instantiate(blueCoin, new Vector3(x, y, 0), Quaternion.identity);
        GameObject D = Instantiate(blueCoin, new Vector3(x, y, 0), Quaternion.identity);
        GameObject B = Instantiate(redCoin, new Vector3(x, y, 0), Quaternion.identity);
        GameObject C = Instantiate(redCoin, new Vector3(x, y, 0), Quaternion.identity);

        Rigidbody2D rbA = A.GetComponent<Rigidbody2D>();
        Rigidbody2D rbB = B.GetComponent<Rigidbody2D>();
        Rigidbody2D rbC = C.GetComponent<Rigidbody2D>();
        Rigidbody2D rbD = D.GetComponent<Rigidbody2D>();

        if (rbA != null)
        {
            rbA.gravityScale = 1;
            rbA.velocity = new Vector2(-3, -3);
        }
        if (rbD != null)
        {
            rbD.gravityScale = 1;
            rbD.velocity = new Vector2(3, -3);
        }

        if (rbB != null)
        {
            rbB.gravityScale = 1;
            rbB.velocity = new Vector2(-5, -3);
        }

        if (rbC != null)
        {
            rbC.gravityScale = 1;
            rbC.velocity = new Vector2(5, -3);
        }
    }
}
