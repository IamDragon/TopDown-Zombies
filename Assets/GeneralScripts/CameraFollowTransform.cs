using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollowTransform : MonoBehaviour
{
    [SerializeField] private Transform toFollow;
    //[SerializeField] private Vector3 offset;

    [Header("Bounds")]
    [SerializeField] private float boundX = 0.15f;
    [SerializeField] private float boundY = 0.05f;


    private void LateUpdate()
    {
        if (toFollow == null) return;

        Vector3 newPosition = CalculateBounds();

        transform.position += new Vector3(newPosition.x, newPosition.y, 0);
    }

    private Vector3 CalculateBounds()
    {
        Vector3 delta = Vector3.zero;
        //checks if we are inside the bounds on the x axis
        float deltaX = toFollow.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < toFollow.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        //checks if we are inside the bounds on the y axis
        float deltaY = toFollow.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < toFollow.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        return delta;
    }
}
