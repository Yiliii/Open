using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingToMainMenu : MonoBehaviour
{
    public string animationName = "EndingLoop";
    public string nextScene = "Menu";

    private Animator animator;
    private bool hasTransitioned = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hasTransitioned || animator == null) return;

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        // Check if current animation is the one we want and has completed one full loop
        if (state.normalizedTime >= 1f)
        {
            hasTransitioned = true;
            SceneManager.LoadScene(nextScene);
        }
    }
}