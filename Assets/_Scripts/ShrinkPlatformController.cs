using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPlatformController : MonoBehaviour
{
    private Animator anim;
    public bool canGrow;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canGrow = true;
    }

    private void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerGrow();
        }
    }

    public void TriggerShrink()
    {
        if (canGrow)
        {
            anim.SetTrigger("isShrinking");
            canGrow = false;
        }
    }

    public void TriggerIdleSmall()
    {
        anim.SetTrigger("isIdleSmall");
    }

    public void TriggerGrow()
    {
        StartCoroutine(GrowRoutine());
    }
    IEnumerator GrowRoutine()
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("isGrowing");
    }

    public void TriggerIdle()
    {
        anim.SetTrigger("isIdle");
        canGrow = true;
    }
}
