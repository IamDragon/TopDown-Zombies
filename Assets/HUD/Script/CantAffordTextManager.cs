using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantAffordTextManager : TextManager
{
    //ONCantAfford -> fade in & fade out after x seconds
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeoutTime;
    [SerializeField] private float timeBeforeFadingOut;
    [SerializeField] private bool startFadedOut;

    [Header("Events")]
    [SerializeField] private EventSO onCantAffordEvent;

    protected override void Start()
    {
        base.Start();
        if (startFadedOut)
            textElement.CrossFadeAlpha(0, 0, true);
    }

    private void OnEnable()
    {
        onCantAffordEvent.Action += FadeIn;
    }

    private void OnDisable()
    {
        onCantAffordEvent.Action -= FadeIn;

    }

    private void FadeIn()
    {
        textElement.CrossFadeAlpha(1, fadeInTime, true);
        Invoke(nameof(FadeOut), timeBeforeFadingOut);
    }

    private void FadeOut()
    {
        textElement.CrossFadeAlpha(0, fadeoutTime, true);
    }
}
