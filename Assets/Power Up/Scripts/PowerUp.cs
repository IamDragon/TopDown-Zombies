using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PowerUp : MonoBehaviour
{
    [SerializeField] private string playerTag;
    bool activated;

    private void Start()
    {
        //GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    protected virtual void ActivatePowerUp()
    {
        Debug.Log(this + " activate");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag) && !activated)
        {
            ActivatePowerUp();
            transform.gameObject.SetActive(false);
        }
    }
}
