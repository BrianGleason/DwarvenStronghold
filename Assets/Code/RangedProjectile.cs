using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    public GameObject attackPrefab;

    public void Fire(Vector3 attackerPos, Vector3 targetPos, float attackOffsetScalar)
    {
        Vector2 selfToTargetVector = targetPos - attackerPos;
        float selfToTargetAngle = Mathf.Atan2(selfToTargetVector.y, selfToTargetVector.x) * Mathf.Rad2Deg;
        Quaternion selfToTargetRotation = Quaternion.Euler(new Vector3(0, 0, selfToTargetAngle));

        Vector2 attackOffset = selfToTargetVector.normalized * attackOffsetScalar;
        Vector3 attackOffsetV3 = attackOffset;

        instantiateAttack(attackPrefab, attackerPos + attackOffsetV3, selfToTargetRotation, attackerPos);
    }

    void instantiateAttack(GameObject attackPrefab, Vector2 attackPosn, Quaternion selfToTargetRotation, Vector2 attackerPos)
    {
        GameObject attack = Instantiate(attackPrefab, attackPosn, selfToTargetRotation);
        Rigidbody2D attackRigidBody = attack.GetComponent<Rigidbody2D>();
        attackRigidBody.AddForce((attackPosn - attackerPos) * 100);
        RangedAttackDecay attackScript = attack.GetComponent<RangedAttackDecay>();
        if (attackScript != null)
        {
            attackScript.InitializeConstants(10, 5, 1, attackerPos);
        }
    }


}
