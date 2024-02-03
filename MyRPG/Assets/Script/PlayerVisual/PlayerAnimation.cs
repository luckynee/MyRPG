using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private PlayerMovement playerMovement;

    private const string ISIDLE = "IsIdle";
    private const string ISWALKING = "IsWalking";
    private const string INPUTMAGNITUDE = "Input Magnitude";


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
        animator.SetFloat(INPUTMAGNITUDE, playerMovement.InputMagnitude, 0.35f, Time.deltaTime);

       /* if(playerStateManager.IsIdle())
        {
            animator.SetBool(ISIDLE, true);
            animator.SetBool(ISWALKING, false);
        } else if (playerStateManager.IsWalking()) 
        {
            animator.SetBool(ISWALKING,true);
            animator.SetBool(ISIDLE,false);
        }
       */
    }


}
