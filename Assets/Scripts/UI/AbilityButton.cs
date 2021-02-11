using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Deft;
using Deft.UI;
using Deft.Input;

public class AbilityButton : UIButton
{
    public Image border;
    public Image highlight;
    public Image hotkey;

    public Color32 cooldownHighlight = new Color32(190, 87, 87, 87);

    bool isOnCooldown;
    float cooldownStart;
    float cooldown;

    protected override void Awake()
    {
        base.Awake();
        border = transform.Find<Image>("Border");
        highlight = transform.Find<Image>("Highlight");
        hotkey = transform.Find<Image>("Hotkey");

        border.enabled = false;
        highlight.enabled = false;
    }

    private void Update()
    {
        if (isOnCooldown)
        {
            if (Time.time - cooldownStart > cooldown)
                EndCooldown();
            else
            {
                float elapsedCooldown = Time.time - cooldownStart;
                float percentCooldown = elapsedCooldown / cooldown;
                highlight.fillAmount = 1 - percentCooldown;
            }
        }
    }

    public void BeginCooldown(float duration)
    {
        isOnCooldown = true;
        cooldownStart = Time.time;
        cooldown = duration;
        highlight.enabled = true;
        highlight.color = cooldownHighlight;
        highlight.fillAmount = 1f;
    }

    public void EndCooldown()
    {
        isOnCooldown = true;
        cooldownStart = 0f;
        cooldown = 0f;
        highlight.enabled = false;
        highlight.fillAmount = 1f;
    }
}
