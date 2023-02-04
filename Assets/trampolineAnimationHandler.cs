using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolineAnimationHandler : MonoBehaviour
{
    public Animator mushroomAnim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && ((collision.transform.position.y - gameObject.transform.position.y) > 0))
        {
            mushroomAnim.SetTrigger("Bounce");
        }
    }
}
