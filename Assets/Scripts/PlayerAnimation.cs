using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    // Special thank you to Leandro for this clean solution.

    [SerializeField]
    private Animator _playerAnimator;

    public void IsMovingLeft()
    {
        _playerAnimator.SetBool("Turn_Left", true);
        _playerAnimator.SetBool("Turn_Right", false);
    }

    public void IsMovingRight()
    {
        _playerAnimator.SetBool("Turn_Right", true);
        _playerAnimator.SetBool("Turn_Left", false);
    }

    public void IsIdle()
    {
        _playerAnimator.SetBool("Turn_Left", false);
        _playerAnimator.SetBool("Turn_Right", false);
    }
}
