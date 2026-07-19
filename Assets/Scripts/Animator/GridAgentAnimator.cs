using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GridAgentAnimator : MonoBehaviour
{
    [SerializeField] private GridAgent agent;
    [SerializeField] private string moveBoolAnim = "IsMoving";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        agent.OnMove += OnMoving;
    }

    private void OnDestroy()
    {
        agent.OnMove -= OnMoving;
    }

    private void OnMoving(bool isMoving)
    {
        _animator.SetBool(moveBoolAnim, isMoving);
    }
}