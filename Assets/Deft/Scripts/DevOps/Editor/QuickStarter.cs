using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Deft.DevOps
{
    public static class QuickStarter
    {
        [RuntimeInitializeOnLoadMethod]
        public static void OnPlay()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            if (SceneManager.GetActiveScene().name != "Startup")
                SceneManager.LoadScene("Startup");
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Startup")
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;

                string routineName = EditorPrefs.GetString("QuickStart");
                if (!string.IsNullOrEmpty(routineName))
                    DeftStartupManager.Get.BeginQuickStart(routineName);
            }
        }

        public static void SetQuickStartRoutine(string name)
            => EditorPrefs.SetString("QuickStart", name);
    }
}

