using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private PlayerStateManager playerStateManager;

    private const string ISIDLE = "IsIdle";
    private const string ISWALKING = "IsWalking";


    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetBool(ISIDLE, true);
    }

    private void Update()
    {
        if(playerStateManager.IsIdle())
        {
            animator.SetBool(ISIDLE, true);
        } else if (playerStateManager.IsWalking()) 
        {
            animator.SetTrigger(ISWALKING);
            animator.SetBool(ISIDLE,false);
        }

    }


}
