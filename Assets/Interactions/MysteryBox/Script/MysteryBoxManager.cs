using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.PackageManager;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEditor.PlayerSettings;
using static UnityEngine.ParticleSystem;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.UIElements;
public class MysteryBoxManager : MonoBehaviour
{
    [Header("Costs")]
    [SerializeField] int baseCost;
    [SerializeField] int firesaleCost;

    [Header("Logistsics")]
    [SerializeField] Transform[] boxLocations;
    [SerializeField] MysteryBox boxPrefab;

    [Header("Events")]
    [SerializeField] protected EventSO onFireSaleStartEvent;
    [SerializeField] protected EventSO onFireSaleEndEvent;

    private MysteryBox[] mysterBoxes;
    private int rolls;
    private int boxLocationIndex;
    private bool fireSaleActive;

    private void Start()
    {
        rolls = 0;
        boxLocationIndex = 0;
        SpawnBoxes();
        RelocateBox();
    }

    private void OnEnable()
    {
        onFireSaleStartEvent.Action += FireSaleStart;
        onFireSaleEndEvent.Action += FireSaleEnd;
    }

    private void OnDisable()
    {
        onFireSaleStartEvent.Action -= FireSaleStart;
        onFireSaleEndEvent.Action -= FireSaleEnd;
    }

    private void SpawnBoxes()
    {
        if (boxPrefab == null)
        {
            Debug.LogWarning("boxprefab is null");
            return;
        }

        mysterBoxes = new MysteryBox[boxLocations.Length];
        for (int i = 0; i < boxLocations.Length; i++)
        {
            MysteryBox newBox = Instantiate<MysteryBox>(boxPrefab, boxLocations[i].position, boxLocations[i].rotation, this.transform);
            newBox.Init(this, baseCost);
            newBox.DeactivateBox();
            mysterBoxes[i] = newBox;
        }
    }

    private void RelocateBox()
    {
        int nextLocatio = FindNextBoxLocationIndex();
        mysterBoxes[boxLocationIndex].DeactivateBox();
        mysterBoxes[nextLocatio].ActivateBox();
        boxLocationIndex = nextLocatio;
        rolls = 0;
    }

    private int FindNextBoxLocationIndex()
    {
        int nextLocation = -1;
        bool foundLocation = false;
        while (foundLocation == false)
        {
            nextLocation = Random.Range(0, boxLocations.Length);
            if(nextLocation != boxLocationIndex)
            {
                foundLocation = true;
                Debug.Log("Next box location: Index " + nextLocation + ", Name" + boxLocations[nextLocation].name);
            }
        }
         
        return nextLocation;
    }

    public bool BoxPurchase() //need better name
    {
        if (fireSaleActive)
            return false;

        rolls++;
        bool shouldChangeLocation = false;
        //if(rolls <= 3)
        //{
        //    //no bear
        //}
        if (rolls >= 4 && rolls <= 7)
        {
            //15% for bear
            shouldChangeLocation = ShouldChangeLocation(15);
        }
        else if (rolls >= 8 && rolls <= 12)
        {
            //30%
            shouldChangeLocation = ShouldChangeLocation(30);
        }
        else if (rolls >= 13)
        {
            //50%
            shouldChangeLocation = ShouldChangeLocation(50);
        }
        if (shouldChangeLocation)
        {
            RelocateBox();
            return true;
        }
        return false;
    }

    public bool ShouldChangeLocation(float percent)
    {
        if (percent < Random.Range(0, 100))
            return true;
        else return false;
    }

    private void FireSaleStart()
    {
        fireSaleActive= true;
        ActivateAllBoxes();
        SetBoxCost(firesaleCost);
    }

    private void FireSaleEnd()
    {
        fireSaleActive= false;
        DeactivateBoxes();
        SetBoxCost(baseCost);
    }

    private void SetBoxCost(int cost)
    {
        for (int i = 0; i < mysterBoxes.Length; i++)
        {
            mysterBoxes[i].SetCost(cost);
        }
    }

    private void ActivateAllBoxes()
    {
        for (int i = 0; i < mysterBoxes.Length; i++)
        {
            if (i == boxLocationIndex) continue;//box already active
            mysterBoxes[i].ActivateBox();
        }
    }

    private void DeactivateBoxes()
    {
        for (int i = 0; i < mysterBoxes.Length; i++)
        {
            if (i == boxLocationIndex) continue;//box should stay active
            mysterBoxes[i].DeactivateBox();
        }
    }

    /*
    The chances of the Teddy Bear appearing in the box increase depending on how many times the 
    box has been used in its current location.The Teddy Bear will never appear on the first 
    three uses of the box.The fourth through seventh uses have a 15% chance of choosing the 
    Teddy Bear. Uses in the range of eighth to twelfth have a 30% chance, and any rolls
    after that have a 50% chance.When the box is in the starting location, however, the 
    Teddy Bear will always appear after eight uses.
    */
}
