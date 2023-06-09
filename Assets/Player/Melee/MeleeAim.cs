using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAim : MonoBehaviour
{
    [SerializeField] private float distanceFromParent;

    private void Update()
    {
        RotateTowardMouse();
    }
    private void RotateTowardMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HelperFunctions.RotateTowardPosition(transform, mousePos);

        Vector2 pos = new Vector2(mousePos.x - transform.parent.position.x, mousePos.y - transform.parent.position.y);
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //if (HelperFunctions.GetDirToMouse(transform.parent.position).x < 0)
            //transform.localScale = new Vector2(transform.localScale.x, -1 * Mathf.Abs(transform.localScale.y));
        //else
            //transform.localScale = new Vector2(transform.localScale.x, Mathf.Abs(transform.localScale.y));

        Vector3 dirtoMouse = distanceFromParent * HelperFunctions.GetDirToMouse(transform.parent.position);
        //Vector3 dirtoMouse = HelperFunctions.GetDirToMouse(transform.parent.position);
        transform.position = transform.parent.position + dirtoMouse;
    }
}
