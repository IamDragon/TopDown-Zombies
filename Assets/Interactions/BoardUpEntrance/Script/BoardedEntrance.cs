using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardedEntrance : MonoBehaviour
{
    private int totalBoards;
    [SerializeField] private int currentBoards;
    [SerializeField] private float timeToRepair;
    [SerializeField] private float timeToBreak;
    private bool repairing;
    public bool breaking;
    [SerializeField] private GameObject[] boards;
    Interaction interaction;

    [Header("Event")]
    [SerializeField] private EventSO onInteractionTriggerdEvent;
    [SerializeField] private StringEventSO onInteractionEnterEvent;
    [SerializeField] private EventSO onInteractionExitEvent;
    [SerializeField] private IntEventSO onCarpenterEvent;

    private void Start()
    {
        currentBoards = boards.Length;
        totalBoards = boards.Length;
        interaction = GetComponent<Interaction>();
        interaction.TextVisible = false;
        SetInteractionText();
    }

    private void OnEnable()
    {
        onInteractionTriggerdEvent.Action += RepairEntrance;
        onCarpenterEvent.Action += FullRepairEntrance;
    }

    private void OnDisable()
    {
        onInteractionTriggerdEvent.Action -= RepairEntrance;
        onCarpenterEvent.Action -= FullRepairEntrance;
    }

    public bool CanBreak
    {
        get { return !breaking && currentBoards > 0; }
    }

    public bool CanRepair
    {
        get { return !repairing && currentBoards < boards.Length; }
    }

    public bool IsBroken
    {
        get { return currentBoards == 0; }
    }

    private void FullRepairEntrance(int j)
    {
        for (int i = currentBoards; i < boards.Length; i++)
        {
            ActivateBoard();
            currentBoards++;
        }
    }

    public void RepairEntrance()
    {
        if (repairing || !interaction.PlayerInRange) return;
        //start animation
        StartCoroutine(AddBoard());
    }

    public IEnumerator AddBoard()
    {
        repairing = true;
        yield return new WaitForSeconds(timeToRepair);
        currentBoards++;
        ActivateBoard();
        if (currentBoards >= totalBoards)
        {
            interaction.TextVisible = false;
            currentBoards = totalBoards;
            if (interaction.PlayerInRange)
                onInteractionExitEvent.Invoke();
        }
        repairing = false;
    }

    private void ActivateBoard()
    {
        for (int i = 0; i < boards.Length; i++)
        {
            if (!boards[i].activeSelf)
            {
                boards[i].SetActive(true);
                return;
            }
        }
    }

    public void BreakBoard()
    {
        //animation - handled externally (in enemy class) - needs a way if it is already being broken
        //unless multiple enemies can break at the same time
        if (!CanBreak) return;
        StartCoroutine(RemoveBoard());
    }

    public IEnumerator RemoveBoard()
    {
        breaking = true;
        yield return new WaitForSeconds(timeToBreak);
        currentBoards--;
        InactiveBoard();
        if (currentBoards <= 0)
        {
            currentBoards = 0;
        }
        breaking = false;
        interaction.TextVisible = true;
        if (interaction.PlayerInRange)
            onInteractionEnterEvent.Invoke(interaction.interactionText);
    }

    private void InactiveBoard()
    {
        for (int i = 0; i < boards.Length; i++)
        {
            if (boards[i].activeSelf)
            {
                boards[i].SetActive(false);
                return;
            }
        }
    }

    private void SetInteractionText()
    {
        interaction.SetInteractionText("to repair barrier");
    }
}