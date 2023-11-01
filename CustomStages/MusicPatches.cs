using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IdolShowdown;
using IdolShowdown.UI;
using IdolShowdown.UI.StageSelect;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using HarmonyLib;

namespace diorama
{
    public class MusicPatches
    {
        public static void Initialize()
        {
            MethodInfo method = typeof(StageSelectSetStage).GetMethod("SwitchToState", BindingFlags.Instance | BindingFlags.Public);
            HarmonyMethod harmonyPost = new HarmonyMethod(typeof(MusicPatches).GetMethod("SwitchToState"));

            //MethodInfo methodTwo = typeof(MusicHelper).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic);
            //HarmonyMethod harmonyPre = new HarmonyMethod(typeof(MusicPatches).GetMethod("Start"));

            Plugin.Harmony.Patch(method, harmonyPost);
            //Plugin.Harmony.Patch(methodTwo, harmonyPre);
        }

        public static void SwitchToState(ref string stageName)
        {
            Melon<Plugin>.Logger.Msg("Switch To State Post Fix");

            StageInfo si = new StageInfo();
            string location = "";
            if (SceneManager.GetSceneAt(1).name == "Stage Select")
            {
                StageSelectController ssc = SceneManager.GetSceneAt(1).GetRootGameObjects()[0].GetComponent<StageSelectController>();
                System.Type t = ssc.GetType();
                FieldInfo field = t.GetField("selectedStageInfo", BindingFlags.Instance | BindingFlags.NonPublic);

                si = (StageInfo)field.GetValue(ssc);
                string[] loc = si.StageVideoClipName.Split('<');
                if (loc[0] == "CUSTOMSTAGE")
                {
                    location = loc[1];
                }
            }
            else if (SceneManager.GetSceneAt(2).name == "Stage Select")
            {
                si = SceneManager.GetSceneAt(2).GetRootGameObjects()[0].GetComponent<ISUIElementsHandler>().CurrentSelectedElement.GetComponent<StageSelectButton>().StageInfo;
                location = SceneManager.GetSceneAt(1).GetRootGameObjects()[0].GetComponent<ISUIElementsHandler>().CurrentSelectedElement.GetComponent<StageTag>().location;
            }

            if (location != "")
            {
                stageName = "NakiriAyameStage";
                if (Melon<Plugin>.Instance.bundle != null)
                {
                    Melon<Plugin>.Instance.bundle.Unload(true);
                    Melon<Plugin>.Instance.bundle = null;
                }
            } 
            Melon<Plugin>.Instance.stageToLoad = location;

            if (stageName != "NakiriAyameStage")
            {
                Melon<Plugin>.Instance.stageInfoToLoad = null;
                return;
            }

            Melon<Plugin>.Instance.stageInfoToLoad = si;
        }

        public static void Start()
        {
            Melon<Plugin>.Logger.Msg("Music Helper Pre Fix");

            if (Melon<Plugin>.Instance.stageInfoToLoad == null || SceneManager.GetSceneAt(1).name != "NakiriAyameStage") return;

            GameObject[] sceneObjects = SceneManager.GetSceneAt(1).GetRootGameObjects();

            foreach (GameObject sceneObject in sceneObjects)
            {
                if (sceneObject.name == "Music + sound bank")
                {
                    //System.Type t = sceneObject.GetComponent<MusicHelper>().GetType();

                    //FieldInfo field = t.GetField("stageInfo", BindingFlags.Instance | BindingFlags.NonPublic);

                    //field.SetValue(sceneObject.GetComponent<MusicHelper>(), Melon<Plugin>.Instance.stageInfoToLoad);

                    return;
                }
            }
        }
    }
}
