using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject openDoor;
    [SerializeField] private GameObject closeDoor;
    [SerializeField] private GameObject doorCOllider;

    public void OpenDoor()
    {
        openDoor.SetActive(true);
        closeDoor.SetActive(false);
        doorCOllider.SetActive(false);
    }

}
