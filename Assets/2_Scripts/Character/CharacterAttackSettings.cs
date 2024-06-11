using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAttackSettings", menuName = "Settings/Character/CharacterAttackSettings")]
public class CharacterAttackSettings : ScriptableObject
{
	public float AttackRange;
	public int AttackDamage;
	public float SingRadius;
	public float SingReach;
}
