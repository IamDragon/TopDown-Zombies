using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    /// <summary>
    /// performs atta ck by checking overlapbox according to transform.right
    /// transform.right is considered where the object is looking
    /// </summary>
    [Header("Gizmo")]
    [SerializeField] private bool drawGizmo;

    [Header("Attack")]
    [SerializeField] protected float damage;
    [SerializeField] protected DamageInfo damageInfo;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float delay;
    protected bool isAttacking;
    public bool IsAttacking { get { return isAttacking; } }

    [Header("Attack position")]
    [SerializeField] protected Vector2 bounds;
    [SerializeField] protected float range;
    


    protected bool DrawGizmo { get { return drawGizmo; } }

    public virtual void PerformAttack(Vector3 dir)
    {
        if(!isAttacking)
        {
            isAttacking= true;
            StartCoroutine(Attack(dir));
            Invoke(nameof(ResetAttack), cooldown);
        }
    }

    protected virtual IEnumerator Attack(Vector3 dir)
    {
        yield return new WaitForSeconds(delay);
        //Collider2D[] hits = Physics2D.OverlapBoxAll(range * transform.right + transform.position, bounds, 0, damageInfo.maskToDamage);
        Collider2D[] hits = HitCheck(dir);
        List<Transform> transformsToDamage = HelperFunctions.GetTransformParentFromColliders(hits);
        Transform closestEnemy = HelperFunctions.GetClosestTransform(transform.position, transformsToDamage);

        if (closestEnemy != null)
        {
            DealDamage(closestEnemy);
            Debug.Log(transform.name + "melee attacked " + closestEnemy);
        }
        else
            Debug.Log(transform.name + " did not have anything to hit");
    }

    protected virtual void ResetAttack()
    {
        isAttacking= false;
    }

    protected virtual Collider2D[] HitCheck(Vector3 dir)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(range * dir + transform.position, bounds, 0, damageInfo.maskToDamage);
        return hits;
        //return Physics2D.OverlapBoxAll(range * transform.right + transform.position, bounds, 0, damageInfo.maskToDamage);
    }

    protected virtual void DealDamage(Transform objectTodamage)
    {
        objectTodamage.GetComponent<HitHandler>().GetHit(damage);
    }

    protected virtual void OnDrawGizmos()
    {
        if (drawGizmo)
        {
            //Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0,0, transform.rotation.eulerAngles.z), Vector3.one);
            //Gizmos.matrix = rotationMatrix;

            //Gizmos.DrawCube(new Vector3(offset.x, offset.y, 0) + transform.position, bounds);
            Gizmos.DrawWireCube(range * transform.right + transform.position, new Vector3(bounds.x, bounds.y, 0));

        }
    }
}
