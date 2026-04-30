using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator anim;
    public Transform spriteTransform;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void UpdateOrientation(float velocityX)
    {

        if (velocityX >= 0.01f)
        {
            spriteTransform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (velocityX <= -0.01f)
        {
            spriteTransform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void SetMoving(bool isMoving)
    {
        if (anim != null) anim.SetBool("isMoving", isMoving);
    }

    public void TriggerAttack()
    {
        if (anim != null) anim.SetTrigger("DoAttack");
    }

    public void TriggerUltimateAttack()
    {
        if (anim != null) anim.SetTrigger("DoUltimate");
    }

    public void TriggerDeath()
    {
        if (anim != null) anim.SetTrigger("DoDeath");
    }

    public void TriggerWin()
    {
        if (anim != null) anim.SetTrigger("DoWin");
    }
}