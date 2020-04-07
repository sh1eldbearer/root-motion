using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIKAdjuster : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The PawnData component for this Pawn."),
        SerializeField] private PawnData _pawnData;
#pragma warning restore CS0649
    #endregion

    #region Public Properties

    #endregion

    // Awake is called before Start
    protected virtual void Awake()
	{
		// Component reference assignments
        if (_pawnData == null)
        {
            _pawnData = this.gameObject.GetComponent<PawnData>();
        }
	}

    private void OnAnimatorIK()
    {
        // Checks the weapon type to see which animations it should use
        // TODO: Could be optimized to not run every IK pass
        if (_pawnData.EquippedWeapon.GetType() == typeof(PistolWeapon))
        {
            UsePistolAnimation();
        }
        else
        {
            UseRifleAnimation();
        }

        // Adjusts the position and rotation of the avatar's hands and elbows based on the provided weights
        if (_pawnData.EquippedWeapon.LHandIKTransform != null)
        {
            _pawnData.PawnAnimator.SetIKPosition(AvatarIKGoal.LeftHand,
                _pawnData.EquippedWeapon.LHandIKTransform.position);
            _pawnData.PawnAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand,
                _pawnData.EquippedWeapon.LHandIKPositionWeight);
            _pawnData.PawnAnimator.SetIKRotation(AvatarIKGoal.LeftHand,
                _pawnData.EquippedWeapon.LHandIKTransform.rotation);
            _pawnData.PawnAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand,
                _pawnData.EquippedWeapon.LHandIKRotationWeight);
        }

        if (_pawnData.EquippedWeapon.LElbowIKTransform != null)
        {

        }

        if (_pawnData.EquippedWeapon.RHandIKTransform != null)
        {
            _pawnData.PawnAnimator.SetIKPosition(AvatarIKGoal.RightHand,
                _pawnData.EquippedWeapon.RHandIKTransform.position);
            _pawnData.PawnAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand,
                _pawnData.EquippedWeapon.RHandIKPositionWeight);

            _pawnData.PawnAnimator.SetIKRotation(AvatarIKGoal.RightHand,
                _pawnData.EquippedWeapon.RHandIKTransform.rotation);
            _pawnData.PawnAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand,
                _pawnData.EquippedWeapon.RHandIKRotationWeight);
        }

        if (_pawnData.EquippedWeapon.RElbowIKTransform != null)
        {

        }
    }

    private void UseRifleAnimation()
    {
        _pawnData.PawnAnimator.SetBool("usingRifle", true);
        _pawnData.PawnAnimator.SetBool("usingPistol", false);
    }

    private void UsePistolAnimation()
    {
        _pawnData.PawnAnimator.SetBool("usingRifle", false);
        _pawnData.PawnAnimator.SetBool("usingPistol", true);
    }
}
