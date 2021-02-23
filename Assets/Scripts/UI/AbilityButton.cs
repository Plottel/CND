using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using Deft;
using Deft.UI;
using Deft.Input;

public class AbilityButton : UIButton
{
    public Ability ability { get; private set; }

    public Image border;
    public Image highlight;
    public Image hotkey;

    public Color32 cooldownHighlight = new Color32(190, 87, 87, 87);

    bool isOnCooldown;

    protected override void Awake()
    {
        base.Awake();
        border = transform.Find<Image>("Border");
        highlight = transform.Find<Image>("Highlight");
        hotkey = transform.Find<Image>("Hotkey");

        border.enabled = false;
        highlight.enabled = false;
    }

    public void SetAbility(Ability newAbility)
    {
        if (ability != null)
        {
            ability.eventUse -= OnAbilityUse;
            ability.eventEndUse -= OnAbilityEndUse;
        }

        ability = newAbility;
        newAbility.eventUse += OnAbilityUse;
        newAbility.eventEndUse += OnAbilityEndUse;
    }    

    private void Update()
    {
        if (isOnCooldown)
        {
            if (ability.RemainingCooldown > 0f)
                UpdateCooldown();
            else
                EndCooldown();
        }
    }

    void OnAbilityUse()
    {
        border.enabled = true;
    }

    void OnAbilityEndUse()
    {
        border.enabled = false;
        BeginCooldown();
    }

    void BeginCooldown()
    {
        isOnCooldown = true;
        highlight.enabled = true;
        highlight.color = cooldownHighlight;
        highlight.fillAmount = 1f;
    }

    void EndCooldown()
    {
        isOnCooldown = false;
        highlight.enabled = false;
        highlight.fillAmount = 1f;
    }

    void UpdateCooldown()
    {
        float remainingPcnt = ability.RemainingCooldown / ability.Cooldown;
        highlight.fillAmount = remainingPcnt;
    }
}
