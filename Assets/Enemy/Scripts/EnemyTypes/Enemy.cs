using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Transform target;
    [SerializeField] protected DamageInfo damageInfo;

    protected EnemyAttackHandler attackHandler;
    protected Movement movement;
    public List<Node> path;

    private HealthSystem healthSystem;

    public HealthSystem HealthSystem { get { return healthSystem; } }

    protected virtual void Awake()
    {
        attackHandler = GetComponent<EnemyAttackHandler>();
        movement = GetComponent<Movement>();
        healthSystem = GetComponent<HealthSystem>();
    }

    protected virtual void Start()
    {
        healthSystem.OnDeath += Inactivate;
    }

    protected virtual void Update()
    {
        if(target == null)
        {
            //stand still don't do jack shit
            movement.SetRBVel0();
            return;
        }

        if (attackHandler.TargetInAttackRange(target))
        {
            movement.SetRBVel0();
            attackHandler.PerformAttack(HelperFunctions.GetDirToTarget(transform, target));
            //Attack
        }
        else// if(!EntranceInRange())
        {
            MoveToNextNode();
        }

    }

    public void Init(float health)
    {
        HealthSystem.Init(health);
        HealthSystem.ResetHealth();
    }

    protected Vector3 DirectionToNode(Node node)
    {
        Vector2 dirToNode = node.worldPosition - transform.position;
        return dirToNode.normalized;
    }

    public void SetPath(List<Node> path)
    {
        this.path = null;
        this.path = path;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    protected virtual void MoveToNextNode()
    {
        if (path == null || path.Count <= 0) return;
        if (AtNode(path[0]) && path.Count > 1)
            path.RemoveAt(0);
        Node nextNode = path[0];
        Vector3 dirToNode= DirectionToNode(nextNode);
        movement.SetVelocity(dirToNode, moveSpeed);
    }

    protected bool AtNode(Node node)
    {
        return Vector3.Distance(transform.position, node.worldPosition) < 0.1f;
    }

    public void Inactivate()
    {
        healthSystem.IsAlive = false;
        transform.gameObject.SetActive(false);
    }

    public void Activate()
    {
        //Debug.Log(this + " activated");
        transform.gameObject.SetActive(true);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ResetSelf()
    {
        healthSystem.ResetHealth();
        Activate();
    }

    public void ResetSelf(Vector3 position)
    {
        healthSystem.ResetHealth();
        Activate();
        SetPosition(position);
    }

    public void Kill()
    {
        healthSystem.TakeDamage(healthSystem.MaxHealth);
    }


}
