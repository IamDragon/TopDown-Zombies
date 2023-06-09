using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HelperFunctions
{
    /// <summary>
    /// Adds the parent transform of colliders, 
    /// if multiple transforms have the same parent the parent will only be added once
    /// </summary>
    /// <param name="colliders"></param>
    /// <returns>List of transforms</returns>
    public static List<Transform> GetTransformParentFromColliders(Collider2D[] colliders)
    {
        List<Transform> list = new List<Transform>();

        //always add first collider
        if (colliders.Length > 0)
            list.Add(colliders[0].transform.parent);

        //only want to damage each entity once, get rid of ones with duplicate colliders
        for (int i = 1; i < colliders.Length; i++)
        {
            if (!list.Contains(colliders[i].transform.parent))
                list.Add(colliders[i].transform.parent);
        }
        return list;
    }

    public static List<Collider2D> RemoveDuplicateColliders(RaycastHit2D[] raycasthits)
    {
        if(raycasthits.Length == 0) 
        {
            return new List<Collider2D>();
        }
        List<Collider2D> list = new List<Collider2D>();

        Dictionary<Transform, Collider2D> dict = new Dictionary<Transform, Collider2D>();

        //always add first collider
        if (raycasthits.Length > 0)
        {
            //Debug.Log(raycasthits[0].transform.parent);
            //Debug.Log(raycasthits[0].transform);
            dict.Add(raycasthits[0].transform, raycasthits[0].collider);
        }

        for (int i = 1; i < raycasthits.Length; i++)
        {
            if (!dict.ContainsKey(raycasthits[i].transform))
                dict.Add(raycasthits[i].transform, raycasthits[i].collider);
        }

        foreach (Collider2D collider in dict.Values)
        {
            list.Add(collider);
        }

        return list;
    }

    /// <summary>
    /// Removes hits whos colliders parent is the same
    /// </summary>
    /// <param name="raycasthits"></param>
    /// <returns></returns>
    public static List<RaycastHit2D> RemoveDuplicateHits(RaycastHit2D[] raycasthits)
    {
        if (raycasthits.Length == 0)
        {
            return new List<RaycastHit2D>();
        }
        List<RaycastHit2D> list = new List<RaycastHit2D>();

        Dictionary<Transform, RaycastHit2D> dict = new Dictionary<Transform, RaycastHit2D>();

        //always add first collide1r
        if (raycasthits.Length > 0)
        {
            dict.Add(raycasthits[0].transform, raycasthits[0]);
        }

        for (int i = 1; i < raycasthits.Length; i++)
        {
            if (!dict.ContainsKey(raycasthits[i].transform))
                dict.Add(raycasthits[i].transform, raycasthits[i]);
        }

        foreach (RaycastHit2D collider in dict.Values)
        {
            list.Add(collider);
        }

        return list;
    }


    public static void GetClosestHit(Vector2 position, RaycastHit2D[] colliders, ref RaycastHit2D closest)
    {
        //RaycastHit2D closest;
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < colliders.Length; i++)
        {
            float distance = Vector2.Distance(position, colliders[i].transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = colliders[i];
            }
        }
        //return closest;
    }

    public static Collider2D GetClosestCollider(Vector2 position, Collider2D[] colliders)
    {
        Collider2D closest = null;
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < colliders.Length; i++)
        {
            float distance = Vector2.Distance(position, colliders[i].transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = colliders[i];
            }
        }
        return closest;
    }

    public static Transform GetClosestTransform(Vector2 position, Transform[] transforms)
    {
        Transform transform = null;
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < transforms.Length; i++)
        {
            float distance = Vector2.Distance(position, transforms[i].position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                transform = transforms[i];
            }
        }
        return transform;
    }

    public static Transform GetClosestTransform(Vector2 position, List<Transform> transforms)
    {
        Transform transform = null;
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < transforms.Count; i++)
        {
            float distance = Vector2.Distance(position, transforms[i].position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                transform = transforms[i];
            }
        }
        return transform;
    }

    public static Vector2 GetDirToMouse(Vector2 other)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePos.x - other.x, mousePos.y - other.y).normalized;
    }

    public static Vector2 GetDirToTarget(Vector2 fromPos, Vector2 targetPos)
    {
        return new Vector2(fromPos.x - targetPos.x, fromPos.y - targetPos.y).normalized;
    }

    public static Vector3 GetDirToTarget(Vector3 fromPos, Vector3 targetPos)
    {
        return new Vector3(fromPos.x - targetPos.x, fromPos.y - targetPos.y, fromPos.z - targetPos.z).normalized;
    }

    public static Vector3 GetDirToTarget(Transform fromTransform, Transform targetTransfrom)
    {
        return new Vector3(targetTransfrom.position.x - fromTransform.position.x,
                           targetTransfrom.position.y - fromTransform.position.y,
                           targetTransfrom.position.z - fromTransform.position.z).normalized;
    }

    public static void RotateTowardPosition(Transform toRotate, Vector2 position)
    {
        Vector2 pos = new Vector2(position.x - toRotate.position.x, position.y - toRotate.position.y);
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        toRotate.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    /// <summary>
    /// returns angle in radians
    /// </summary>
    /// <param name="angle"></param>
    public static float DegreeToRad(float angle)
    {
        return angle * Mathf.Deg2Rad;
    }
}
