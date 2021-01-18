using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeysTestingStarter : MonoBehaviour
{
    private void Start()
    {
        UIManager.Get.Show<HotkeysPanel>();
    }
}
