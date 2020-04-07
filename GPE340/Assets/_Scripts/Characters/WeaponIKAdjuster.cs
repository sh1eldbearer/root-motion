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
    private void Awake()
	{
		// Component reference assignments
        if (_pawnData == null)
        {
            _pawnData = this.gameObject.GetComponent<PawnData>();
        }
	}

    private void OnAnimatorIK()
    {
        // Adjusts the position and rotation of the avatar's hands based on the provided weights
        _pawnData.PawnAnimator.SetIKPosition(AvatarIKGoal.LeftHand, _pawnData.EquippedWeapon.LeftHandIKTransform.position);
        _pawnData.PawnAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _pawnData.LeftHandIKPositionWeight);

        _pawnData.PawnAnimator.SetIKRotation(AvatarIKGoal.LeftHand, _pawnData.EquippedWeapon.LeftHandIKTransform.rotation);
        _pawnData.PawnAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _pawnData.LeftHandIKRotationWeight);

        _pawnData.PawnAnimator.SetIKPosition(AvatarIKGoal.RightHand, _pawnData.EquippedWeapon.RightHandIKTransform.position);
        _pawnData.PawnAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, _pawnData.RightHandIKPositionWeight);

        _pawnData.PawnAnimator.SetIKRotation(AvatarIKGoal.RightHand, _pawnData.EquippedWeapon.RightHandIKTransform.rotation);
        _pawnData.PawnAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, _pawnData.RightHandIKRotationWeight);

    }
}
