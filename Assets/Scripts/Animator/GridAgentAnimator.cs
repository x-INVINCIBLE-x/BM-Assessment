using System;
using UnityEngine;

/// <summary>
/// Animates an agent based on their moving status.
/// </summary>
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

    /// <summary>
    /// Updates Boolean based on agent movement events.
    /// </summary>
    /// <param name="isMoving">Agent's current moving status</param>
    private void OnMoving(bool isMoving)
    {
        _animator.SetBool(moveBoolAnim, isMoving);
    }
}