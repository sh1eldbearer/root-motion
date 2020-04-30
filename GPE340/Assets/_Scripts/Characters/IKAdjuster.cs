using System.Collections;
using System.Collections.Generic;
using Utility.Enums;
using UnityEngine;

public class IKAdjuster : MonoBehaviour
{
    #region Private Properties

#pragma warning disable CS0649

    [Header("Game Components")]
    [Tooltip("The PawnData component for this pawn."),
        SerializeField] private PawnData _pawnData;

    [Tooltip("The Transform component of this pawn's model's head."),
        Space, SerializeField] private Transform _headTransform;

    [Header("IK Weight Settings")]
    [Tooltip("The weight to apply to the position of the avatar's left hand for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _lHandPositionWeight = 1.0f;
    [Tooltip("The weight to apply to the rotation of the avatar's left hand for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _lHandRotationWeight = 1.0f;
    [Tooltip("The weight to apply to the position of the avatar's left elbow for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _lElbowPositionWeight = 1.0f;

    [Tooltip("The weight to apply to the position of the avatar's right hand for inverse kinematics."),
        Space, SerializeField, Range(0.0f, 1.0f)] private float _rHandPositionWeight = 1.0f;
    [Tooltip("The weight to apply to the rotation of the avatar's right hand for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _rHandRotationWeight = 1.0f;
    [Tooltip("The weight to apply to the position of the avatar's right elbow for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _rElbowPositionWeight = 1.0f;

    [Tooltip("The weight to apply to the position of the avatar's head for inverse kinematics."),
        Space, SerializeField, Range(0.0f, 1.0f)] private float _headPositionWeight = 1.0f;
#pragma warning restore CS0649

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
        if (_pawnData.PawnAnimator.GetBool("isDead"))
        {
            DisableAllIKWeights();
        }
        else
        {
            // Checks the weapon type to see which animations it should use
            // TODO: Could be optimized to not run every IK pass
            if (_pawnData.InventoryMgr.EquippedWeaponType == WeaponType.Pistol)
            {
                UsePistolAnimation();
            }
            else
            {
                UseRifleAnimation();
            }

            // Adjusts the position and rotation of the avatar's hands and elbows based on the provided weights
            if (_pawnData.InventoryMgr.EquippedWeaponModelData.LHandIKTransform != null) // Left hand
            {
                SetIKTransforms(AvatarIKGoal.LeftHand,
                    _pawnData.InventoryMgr.EquippedWeaponModelData.LHandIKTransform.position,
                    _lHandPositionWeight,
                    _pawnData.InventoryMgr.EquippedWeaponModelData.LHandIKTransform.rotation,
                    _lHandRotationWeight);
            }

            if (_pawnData.InventoryMgr.EquippedWeaponModelData.LElbowIKTransform != null) // Left elbow
            {
                SetIKHintTransforms(AvatarIKHint.LeftElbow,
                    _pawnData.InventoryMgr.EquippedWeaponModelData.LElbowIKTransform.position,
                    _lElbowPositionWeight);
            }

            if (_pawnData.InventoryMgr.EquippedWeaponModelData.RHandIKTransform != null) // Right hand
            {
                SetIKTransforms(AvatarIKGoal.RightHand,
                    _pawnData.InventoryMgr.EquippedWeaponModelData.RHandIKTransform.position,
                    _rHandPositionWeight,
                    _pawnData.InventoryMgr.EquippedWeaponModelData.RHandIKTransform.rotation,
                    _rHandRotationWeight);
            }

            if (_pawnData.InventoryMgr.EquippedWeaponModelData.RElbowIKTransform != null) // Right elbow
            {
                SetIKHintTransforms(AvatarIKHint.RightElbow,
                    _pawnData.InventoryMgr.EquippedWeaponModelData.RElbowIKTransform.position,
                    _rElbowPositionWeight);
            }

            // Adjusts the rotation of the character's head so they always look where the mouse is pointing
            if (_headTransform != null)
            {
                // Creates an imaginary plane at the position of the model's head
                Plane headPlane = new Plane(_headTransform.up, _headTransform.position);

                // Gets a ray from the mouse's position through the camera's view direction
                Ray mouseRay = GameManager.gm.GameCamera.ScreenPointToRay(Input.mousePosition);

                float intersectDistance;

                // If the mouse ray collides with the plane, have the avatar's head look at the mouse's position
                if (headPlane.Raycast(mouseRay, out intersectDistance))
                {
                    Vector3 intersectPoint = mouseRay.GetPoint(intersectDistance);

                    _pawnData.PawnAnimator.SetLookAtPosition(intersectPoint);
                    _pawnData.PawnAnimator.SetLookAtWeight(_headPositionWeight);
                }
            }
        }
    }

    /// <summary>
    /// Sets the animator's parameters so the avatar will use the rifle aiming animations.
    /// </summary>
    private void UseRifleAnimation()
    {
        _pawnData.PawnAnimator.SetBool("usingRifle", true);
        _pawnData.PawnAnimator.SetBool("usingPistol", false);
    }

    /// <summary>
    /// Sets the animator's parameters so the avatar will use the pistol aiming animations.
    /// </summary>
    private void UsePistolAnimation()
    {
        _pawnData.PawnAnimator.SetBool("usingRifle", false);
        _pawnData.PawnAnimator.SetBool("usingPistol", true);
    }


    /// <summary>
    /// Set the target position and rotation (and weights) for a body part of this avatar.
    /// </summary>
    /// <param name="ikGoal">The body part of the avatar that will be moved.</param>
    /// <param name="goalPosition">The goal position to move the body part to.</param>
    /// <param name="positionWeight">The weight to apply to the goal position.</param>
    /// <param name="goalRotation">The goal rotation to move the body part to.</param>
    /// <param name="rotationWeight">The weight to apply to the goal rotation.</param>
    private void SetIKTransforms(AvatarIKGoal ikGoal, Vector3 goalPosition, float positionWeight,
        Quaternion goalRotation, float rotationWeight)
    {
        _pawnData.PawnAnimator.SetIKPosition(ikGoal, goalPosition);
        _pawnData.PawnAnimator.SetIKPositionWeight(ikGoal, positionWeight);
        _pawnData.PawnAnimator.SetIKRotation(ikGoal, goalRotation);
        _pawnData.PawnAnimator.SetIKRotationWeight(ikGoal, rotationWeight);
    }

    /// <summary>
    /// Sets the target hint position (and weight) for a body part of this avatar.
    /// </summary>
    /// <param name="ikHint">The body part to assign the hint position to.</param>
    /// <param name="hintPosition">The hint position to move the body part to.</param>
    /// <param name="positionWeight">The weight to apply to the hint position.</param>
    private void SetIKHintTransforms(AvatarIKHint ikHint, Vector3 hintPosition, float positionWeight)
    {
        _pawnData.PawnAnimator.SetIKHintPosition(ikHint, hintPosition);
        _pawnData.PawnAnimator.SetIKHintPositionWeight(ikHint, positionWeight);
    }

    /// <summary>
    /// Sets all IK weights to zero.
    /// </summary>
    public void DisableAllIKWeights()
    {
        _pawnData.PawnAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
        _pawnData.PawnAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        _pawnData.PawnAnimator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0);
        _pawnData.PawnAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        _pawnData.PawnAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
        _pawnData.PawnAnimator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
        _pawnData.PawnAnimator.SetLookAtWeight(0);
    }
}