using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniExplosionController : MonoBehaviour {




    public Animator miniexplosionAnimator;

    public IEnumerator launchAnimationDamage(Vector3 pos) {

        Animator animator = Instantiate(miniexplosionAnimator, pos, Quaternion.identity).GetComponent<Animator>();
        animator.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        Vector3 newScale = animator.gameObject.transform.localScale;

        newScale.x = 0.2f;
        newScale.y = 0.2f;

        animator.gameObject.transform.localScale = newScale;
        animator.enabled = true;

        while (animator.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.8f) {
            yield return new WaitForSeconds(0.1f);

        }

        GameObject.Destroy(animator.gameObject);
    }
}
