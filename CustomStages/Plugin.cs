using IdolShowdown;
using JetBrains.Annotations;
using MelonLoader;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using IdolShowdown.Managers;
using TMPro;
using System.Reflection;
using HarmonyLib;

namespace diorama
{
    public class Plugin : MelonMod
    {
        internal AssetBundle bundle;

        public StageInfo stageInfoToLoad;

        public string stageToLoad;

        private bool stageLoaded;
        private GameObject customStage;
        public static new HarmonyLib.Harmony Harmony { get; private set; }

        public override void OnLateInitializeMelon()
        {
            base.OnLateInitializeMelon();

            Harmony = new HarmonyLib.Harmony("diorama");

            MusicPatches.Initialize();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasLoaded(buildIndex, sceneName);

            if (stageLoaded)
            {
                GameObject.Destroy(customStage);
                customStage = null;
                stageLoaded = false;
            }

            if (sceneName == "Stage Select")
            {
                GameObject stageCanvas = SceneManager.GetSceneAt(2).GetRootGameObjects()[0];

                stageCanvas.SetActive(true);

                stageCanvas.AddComponent<SSSManager>();

                stageCanvas.SetActive(false);
            }

            if (sceneName != "NakiriAyameStage" || stageToLoad == "") return;

            GameObject[] rootList = SceneManager.GetSceneAt(1).GetRootGameObjects();
            bool isScene = false;
            GameObject scene = null;

            for (int i = 0; i < rootList.Length; i++)
            {
                if (rootList[i].name == "Scene")
                {
                    isScene = true;
                    scene = rootList[i];
                    break;
                }
            }

            if (isScene)
            {
                Transform ground = scene.transform.Find("Background/Ground");

                ground.SetParent(null);
                ground.GetComponent<SpriteRenderer>().enabled = false;
                
                foreach(Transform child in ground)
                {
                    child.gameObject.SetActive(false);
                }

                scene.SetActive(false);

                if (bundle == null)
                {
                    bundle = AssetBundle.LoadFromFile(stageToLoad);
                }

                customStage = GameObject.Instantiate((GameObject)bundle.LoadAsset("CustomStage"));
                stageLoaded = true;
            }
        }
    }
}
