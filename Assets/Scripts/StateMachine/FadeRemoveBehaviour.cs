using UnityEngine;


public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0f;
    private float fadeDelayElapsed = 0f;
    public float fadeDelay = 0f;

    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color startColour;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        objToRemove = animator.gameObject;
        startColour = spriteRenderer.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fadeDelay > fadeDelayElapsed)
        {
            fadeDelayElapsed += Time.deltaTime;
        }

        else
        {
            timeElapsed += Time.deltaTime;
            float newAlpha = startColour.a * (1 - timeElapsed / fadeTime); // whatever % the time elapsed is over fade time, subtract from full alpha value of 1 which sets the new alpha
            spriteRenderer.color = new Color(startColour.r, startColour.g, startColour.b, newAlpha);

            if (timeElapsed > fadeTime)
            {
                Destroy(objToRemove);
            }
        }
    }
}
