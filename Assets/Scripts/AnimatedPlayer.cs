using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedPlayer : APlayer
{
    private const string SpeedParamName = "Speed";
    private const string DirectionParamName = "Direction";
    private const string JumpParamName = "Jump";
    private const string LeftFootWeight = "LeftFootWeight";
    private const string RightFootWeight = "RightFootWeight";

    private const float ParamChangeSpeed = 5f;

    private Animator animator;

    private Transform leftFoot;
    private Transform rightFoot;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();

        leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
    }

    protected override void Update()
    {
        base.Update();

        float resultDirection = Mathf.MoveTowards(animator.GetFloat(DirectionParamName), direction.x, Time.deltaTime * ParamChangeSpeed);
        animator.SetFloat(DirectionParamName, resultDirection);

        float targetSpeed = direction.y;
        if (targetSpeed > 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            targetSpeed *= 2;
        }
        float resultSpeed = Mathf.MoveTowards(animator.GetFloat(SpeedParamName), targetSpeed, Time.deltaTime * ParamChangeSpeed);
        animator.SetFloat(SpeedParamName, resultSpeed);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetTrigger(JumpParamName);
        }




        if (Physics.Raycast(leftFoot.position + Vector3.up * 0.3f, Vector3.down, out RaycastHit lHit))
        {
            lfPos = lHit.point + Vector3.up * 0.1f;
            lRot = Quaternion.FromToRotation(Vector3.up, lHit.normal) * transform.rotation;
        }

        if (Physics.Raycast(rightFoot.position + Vector3.up * 0.3f, Vector3.down, out RaycastHit rHit))
        {
            rfPos = rHit.point + Vector3.up * 0.1f;
            rRot = Quaternion.FromToRotation(Vector3.up, rHit.normal) * transform.rotation;
        }
    }

    private float lfWeight;
    private float rfWeight;

    private Vector3 lfPos;
    private Vector3 rfPos;
    private Quaternion lRot;
    private Quaternion rRot;

    private void OnAnimatorIK(int layerIndex)
    {
        lfWeight = animator.GetFloat(LeftFootWeight);
        rfWeight = animator.GetFloat(RightFootWeight);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, lfWeight);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rfWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, lfWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rfWeight);


        animator.SetIKPosition(AvatarIKGoal.LeftFoot, lfPos);
        animator.SetIKPosition(AvatarIKGoal.RightFoot, rfPos);

        animator.SetIKRotation(AvatarIKGoal.LeftFoot, lRot);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, rRot);
    }

    protected override void MovePlayer()
    {
        throw new System.NotImplementedException();
    }

    protected override void RotatePlayer()
    {
        throw new System.NotImplementedException();
    }
}
