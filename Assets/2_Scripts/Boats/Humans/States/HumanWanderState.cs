using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWanderState : BaseState<Human>
{
	private Vector3 initialTargetPos;
	private Vector3 initialTargetPosDifference;
	private Vector3 updatedTargetPos;

	public override void Enter()
	{
		initialTargetPos = bb.GenerateRandomHumanPos();
		initialTargetPosDifference = bb.platform.gameObject.transform.position - initialTargetPos;
	}

	public override void Update()
	{
		UpdatePosition();
		UpdateRotation();

		if (Vector3.Distance(updatedTargetPos, bb.GameObject.transform.position) < 1.0f)
		{
			owner.SwitchState(typeof(HumanIdleState));
		}
	}

	private void UpdatePosition()
	{
		updatedTargetPos = initialTargetPos + (bb.platform.gameObject.transform.position - initialTargetPos) + initialTargetPosDifference;
		updatedTargetPos.y = 10.0f;

		bb.GameObject.transform.position = Vector3.Slerp(bb.GameObject.transform.position, updatedTargetPos, Time.deltaTime * bb.humansSettings.WalkSpeed);
	}

	private void UpdateRotation()
	{
		Quaternion targetRotation = Quaternion.LookRotation(updatedTargetPos - bb.GameObject.transform.position);
		bb.GameObject.transform.rotation = Quaternion.Lerp(bb.GameObject.transform.rotation, targetRotation, Time.deltaTime * bb.humansSettings.RotationSpeed);
	}
}