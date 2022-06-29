using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator = default;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
