using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAppear", true);

    }

    void FixedUpdate()
    {
        AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
        if (asi.IsName("hide") && animator.GetBool("isAppear"))
        {
            StartCoroutine(ResetIsAppearAfterDelay(10f));
        }
    }

    private IEnumerator ResetIsAppearAfterDelay(float delay)
    {
        animator.SetBool("isAppear", false);
        yield return new WaitForSeconds(delay); // Chờ 10 giây
        animator.SetBool("isAppear", true);
    }
}
