using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using static ak.wwise.core;
using IdolShowdown;
using MelonLoader;
using UnityEngine.SceneManagement;

namespace diorama
{
    internal class AudioLoader
    {
        internal static void SetupTest()
        {
            Melon<Plugin>.Logger.Msg("Start Test");
            WwiseObjectReference wo = GameObject.Find("Wwise Global").GetComponent<AkBank>().data.WwiseObjectReference;

            Melon<Plugin>.Logger.Msg("Got Wwise: " + wo.name);

            System.Type t = wo.GetType().BaseType;
            FieldInfo field = t.GetField("objectName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            Melon<Plugin>.Logger.Msg("Got Type");

            field.SetValue(wo, "TestSB", BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.NonPublic, null, null);

            Melon<Plugin>.Logger.Msg("Set Value");

            GameObject.Find("Wwise Global").GetComponent<AkBank>().data.Load();

            Melon<Plugin>.Logger.Msg("Loaded Bank");

            SceneManager.GetSceneAt(2).GetRootGameObjects()[0].gameObject.SetActive(true);

            StageInfo.StageSongData sd = GameObject.Find("Canvas/WholeUI/StageButtons/StageButtonsContainer/StageButtonsBG/MoonlitGates").GetComponent<StageSelectButton>().StageInfo.Songs[0];

            Melon<Plugin>.Logger.Msg("Got Song");

            t = sd.GetType();
            //field = t.GetField(sd.PlayState); //t.GetField("PlayState", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            Melon<Plugin>.Logger.Msg("Got Type");

            //field.SetValue(sd, "Play_XB");
            t.GetProperty("PlayState").SetMethod.Invoke(sd, new object[] { "Play_XB" });
            t.GetProperty("DisplayName").SetMethod.Invoke(sd, new object[] { "Remnants of Memories" });
            t.GetProperty("SongColor").SetMethod.Invoke(sd, new object[] { Color.red });

            Melon<Plugin>.Logger.Msg("Set Song");

            SceneManager.GetSceneAt(2).GetRootGameObjects()[0].gameObject.SetActive(false);
        }
    }
}
