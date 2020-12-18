using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPlatformController : MonoBehaviour
{
    private Animator anim;
    public bool canGrow;

    [Header("Hover Details")]
    public Transform startPos;
    public Transform endPos;
    private Vector3 distance;
    public float hoverTimer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canGrow = true;

        distance = endPos.position - startPos.position;
    }

    private void Update()
    {
        hoverTimer += Time.deltaTime * 0.5f;

        var distanceY = (distance.y > 0) ? startPos.position.y + Mathf.PingPong(hoverTimer, distance.y) : startPos.position.y;

        transform.position = new Vector3(transform.position.x, distanceY, 0.0f);
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
