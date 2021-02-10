using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Deft;
using Deft.UI;

public class AbilityButton : UIButton
{
    public Image border;

    protected override void Awake()
    {
        base.Awake();
        border = transform.Find<Image>("Border");
        border.enabled = false;
    }
}
