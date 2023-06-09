using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public bool active;
    public GameObject go;
    public TextMeshProUGUI txt;
    public Vector3 direction;
    public float speed;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        transform.gameObject.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        transform.gameObject.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active)
            return;

        if (Time.time - lastShown > duration)
            Hide();

        transform.position += direction.normalized * speed *Time.deltaTime;
    }
}
