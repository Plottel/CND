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
                SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public static void SetQuickStartScene(string name)
            => EditorPrefs.SetString("QuickStartScene", name);
    }
}

