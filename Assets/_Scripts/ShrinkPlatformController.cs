using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * File: 
 *      ShrinkPlatformController.cs
 * Name: 
 *      Russell Brabers
 * StudentID: 
 *      101192571
 * Date Modified:
 *      December 18, 2020
 * Description:
 *      A script to control a platform that hovers, shrinks, and grows
 * Revision History:
 *      - Initial creation
 *      - Added animator modifying functions
 *      - Added collision logic
 *      - Added hover variables and functionality to the Update method
 *      - Added AudioSource and sound effect
 */

public class ShrinkPlatformController : MonoBehaviour
{
    private Animator anim;
    private AudioSource sfx;
    public bool canShrink; // Boolean to check if the animation can start

    [Header("Hover Details")]
    public Transform startPos;
    public Transform endPos;
    private Vector3 distance;
    public float hoverTimer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
        canShrink = true;

        distance = endPos.position - startPos.position;
    }

    // Use Mathf.PingPong to create a hovering effect
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
        if (canShrink)
        {
            anim.SetTrigger("isShrinking");
            sfx.Play();
            canShrink = false;
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
        canShrink = true;
    }
}
