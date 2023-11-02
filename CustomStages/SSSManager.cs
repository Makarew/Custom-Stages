using Harmony;
using IdolShowdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using System.IO;
using static MelonLoader.Modules.MelonModule;
using UnityEngine.UI;
using RINputsL;
using IdolShowdown.Managers;
using TMPro;
using IdolShowdown.UI;
using System.Reflection;
using IdolShowdown.UI.StageSelect;

namespace diorama
{
    internal class SSSManager : MonoBehaviour
    {
        public List<StageInfo> originalStages = new List<StageInfo>();
        public List<StageData> newStages = new List<StageData>();
        public List<String> locations = new List<String>();

        private void Start()
        {
            if (originalStages.Count > 0) return;

            StageSelectController ssc = GetComponent<StageSelectController>();

            System.Type t = ssc.GetType();
            FieldInfo field = t.GetField("StageNavList", BindingFlags.Instance | BindingFlags.NonPublic);

            List<StageInfo> stages = (List<StageInfo>)field.GetValue(ssc);

            originalStages.Add(stages[0]);
            originalStages.Add(stages[1]);
            originalStages.Add(stages[2]);
            originalStages.Add(stages[3]);
            originalStages.Add(stages[4]);
            originalStages.Add(stages[5]);

            string[] allStageDirectories = Directory.GetFiles(Path.Combine(MelonHandler.PluginsDirectory, "Stages"), "mod.json", SearchOption.AllDirectories);

            foreach (string dir in allStageDirectories)
            {
                MetadataBase mb = MetadataBase.Load(dir);
                StageInfo si = CreateStageInfo(mb);
                t = si.GetType();
                //t.GetProperty("Songs").SetMethod.Invoke(si, new object[] { CreateSongData(mb.Location) });
                t.GetProperty("Songs").SetMethod.Invoke(si, new object[] { originalStages[0].Songs });
                StageData sd = new StageData();
                sd.location = Path.Combine(mb.Location, mb.AssetBundle);
                sd.si = si;
                newStages.Add(sd);
                stages.Add(si);
            }

            t = ssc.GetType();
            MethodInfo method = t.GetMethod("UpdateButtonIndexes", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(ssc, new object[] {  });
        }

        private StageInfo GetStageInfo(StageSelectButton stage)
        {
            return stage.StageInfo;
        }

        private StageSelectButton GetStageButton(string stageName)
        {
            return transform.Find("WholeUI/StageButtons/StageButtonsContainer/StageButtonsBG/" + stageName).GetComponent<StageSelectButton>();
        }

        public StageInfo CreateStageInfo(MetadataBase mb)
        {
            StageInfo si = (StageInfo)ScriptableObject.CreateInstance("StageInfo");
            string sn = "NakiriAyameStage";

            si.StageVideoURL = Path.Combine(mb.Location, "Preview.mp4");

            System.Type t = si.GetType();
            t.GetProperty("StageDisplayName").SetMethod.Invoke(si, new object[] { mb.Title });
            t.GetProperty("StageDescription").SetMethod.Invoke(si, new object[] { mb.Description });
            t.GetProperty("StageLocation").SetMethod.Invoke(si, new object[] { mb.StageLocation });
            t.GetProperty("StageVideoClipName").SetMethod.Invoke(si, new object[] { string.Concat("CUSTOMSTAGE<", Path.Combine(mb.Location, mb.AssetBundle)) });

            string[] myColor = mb.Color.Split(';');
            Color color = new Color(float.Parse(myColor[0]), float.Parse(myColor[1]), float.Parse(myColor[2]), 1);

            t.GetProperty("StageColor").SetMethod.Invoke(si, new object[] { color });
            t.GetProperty("StageSceneName").SetMethod.Invoke(si, new object[] { sn });

            Texture2D tex;
            byte[] data;

            data = File.ReadAllBytes(Path.Combine(mb.Location, "Thumb.png"));
            tex = new Texture2D(2, 2);
            tex.LoadImage(data);

            Sprite thumb = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero, 100);

            t.GetProperty("StageImage").SetMethod.Invoke(si, new object[] { thumb });

            return si;
        }

        private List<StageInfo.StageSongData> CreateSongData(string path)
        {
            if (!File.Exists(Path.Combine(path, "music.json"))) return null;

            MetadataMusic mm = MetadataMusic.Load(Path.Combine(path, "music.json"));

            if (mm.SoundBankID == "N/A" || mm.SongCount == 0) return null;

            WwiseObjectReference wo = GameObject.Find("Wwise Global").GetComponent<AkBank>().data.WwiseObjectReference;
            System.Type t = wo.GetType().BaseType;
            FieldInfo field = t.GetField("objectName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            field.SetValue(wo, mm.SoundBankID, BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.NonPublic, null, null);
            GameObject.Find("Wwise Global").GetComponent<AkBank>().data.Load();

            List<StageInfo.StageSongData> sds = new List<StageInfo.StageSongData>();

            for (int i = 0; i < mm.SongCount; i++)
            {
                sds.Add(new StageInfo.StageSongData());
                StageInfo.StageSongData sd = sds[i];

                t = sd.GetType();

                string play = "Play_";
                string stop = "Stop_";
                string pause = "Pause_";
                string resume = "Resume_";

                string displayName = "";

                Color color = Color.white;

                string[] colorString = new string[3];

                switch (i)
                {
                    case 0:
                        play += mm.SongOneStateName;
                        stop += mm.SongOneStateName;
                        pause += mm.SongOneStateName;
                        resume += mm.SongOneStateName;

                        displayName = mm.SongOneTitle;

                        colorString = mm.SongOneColor.Split(';');
                        color = new Color(float.Parse(colorString[0]), float.Parse(colorString[1]), float.Parse(colorString[2]));
                        break;
                    case 1:
                        play += mm.SongTwoStateName;
                        stop += mm.SongTwoStateName;
                        pause += mm.SongTwoStateName;
                        resume += mm.SongTwoStateName;

                        displayName = mm.SongTwoTitle;

                        colorString = mm.SongTwoColor.Split(';');
                        color = new Color(float.Parse(colorString[0]), float.Parse(colorString[1]), float.Parse(colorString[2]));
                        break;
                    case 2:
                        play += mm.SongThreeStateName;
                        stop += mm.SongThreeStateName;
                        pause += mm.SongThreeStateName;
                        resume += mm.SongThreeStateName;

                        displayName = mm.SongThreeTitle;

                        colorString = mm.SongThreeColor.Split(';');
                        color = new Color(float.Parse(colorString[0]), float.Parse(colorString[1]), float.Parse(colorString[2]));
                        break;
                    case 3:
                        play += mm.SongFourStateName;
                        stop += mm.SongFourStateName;
                        pause += mm.SongFourStateName;
                        resume += mm.SongFourStateName;

                        displayName = mm.SongFourTitle;

                        colorString = mm.SongFourColor.Split(';');
                        color = new Color(float.Parse(colorString[0]), float.Parse(colorString[1]), float.Parse(colorString[2]));
                        break;
                }

                string playPreview = play + "_Preview";
                string stopPreview = stop + "_Preview";

                t.GetProperty("DisplayName").SetMethod.Invoke(sd, new object[] { displayName });
                t.GetProperty("PauseState").SetMethod.Invoke(sd, new object[] { pause });
                t.GetProperty("PlayState").SetMethod.Invoke(sd, new object[] { play });
                t.GetProperty("StopState").SetMethod.Invoke(sd, new object[] { stop });
                t.GetProperty("ResumeState").SetMethod.Invoke(sd, new object[] { resume });
                t.GetProperty("PreviewPlayState").SetMethod.Invoke(sd, new object[] { playPreview });
                t.GetProperty("PreviewStopState").SetMethod.Invoke(sd, new object[] { stopPreview });
                t.GetProperty("SongColor").SetMethod.Invoke(sd, new object[] { color });
            }

            return sds;
        }

        public struct StageData
        {
            public StageInfo si;
            public string location;
        }
    }
}
