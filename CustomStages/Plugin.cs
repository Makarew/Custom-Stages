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

        private bool didSongTest;

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
                //AudioLoader.SetupTest();

                GameObject stageCanvas = SceneManager.GetSceneAt(2).GetRootGameObjects()[0];

                stageCanvas.SetActive(true);
                
                //GameObject myBar = GameObject.Instantiate(stageCanvas.transform.Find("WholeUI/BottomMenuBar").gameObject, stageCanvas.transform.Find("WholeUI"));
                //GameObject.Destroy(myBar.transform.Find("ButtonAnchor/Select").gameObject);
                //GameObject.Destroy(myBar.transform.Find("ButtonAnchor/MoreInfo").gameObject);
                //GameObject.Destroy(myBar.transform.Find("ButtonAnchor/Back").gameObject);

                //myBar.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 91);
                //myBar.transform.localPosition = new Vector3(-650, -440, 0);

                //GameObject anchor = myBar.transform.Find("ButtonAnchor").gameObject;
                //anchor.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 91);

                //myBar.transform.Find("ButtonAnchor/RandomStage").gameObject.SetActive(true);

                //anchor.transform.Find("RandomStage/Text (TMP)").GetComponent<TMP_Text>().text = "Refresh";
                //anchor.transform.Find("RandomStage/Text (TMP)").GetComponent<TMP_Text>().fontStyle = FontStyles.Italic;
                //anchor.transform.Find("RandomStage/Text (TMP)").GetComponent<TMP_Text>().font = anchor.transform.Find("NextSong/Text (TMP)").GetComponent<TMP_Text>().font;
                //anchor.transform.Find("RandomStage/Text (TMP)").GetComponent<TMP_Text>().fontMaterial = anchor.transform.Find("NextSong/Text (TMP)").GetComponent<TMP_Text>().fontMaterial;
                //anchor.transform.Find("PrevSong/Text (TMP)").GetComponent<TMP_Text>().text = "Previous Page";
                //anchor.transform.Find("NextSong/Text (TMP)").GetComponent<TMP_Text>().text = "Next Page";

                //GameObject ran = anchor.transform.Find("PrevSong/RandomStageButton").gameObject;
                //ran.GetComponent<InputButtonScript>().enabled = false;
                //ran.transform.Find("Back").gameObject.SetActive(true);
                //ran.transform.Find("ControllerPrompt").gameObject.SetActive(false);
                //ran.transform.Find("KeyPrompt").gameObject.SetActive(true);
                //ran.transform.Find("KeyPrompt").gameObject.GetComponent<Text>().text = "LT";

                //ran = anchor.transform.Find("NextSong/RandomStageButton").gameObject;
                //ran.GetComponent<InputButtonScript>().enabled = false;
                //ran.transform.Find("Back").gameObject.SetActive(true);
                //ran.transform.Find("ControllerPrompt").gameObject.SetActive(false);
                //ran.transform.Find("KeyPrompt").gameObject.SetActive(true);
                //ran.transform.Find("KeyPrompt").gameObject.GetComponent<Text>().text = "RT";

                //GameObject newText = GameObject.Instantiate(stageCanvas.transform.Find("WholeUI/BottomMenuBar(Clone)/ButtonAnchor/Select/Text (TMP)").gameObject, stageCanvas.transform.Find("WholeUI/BottomMenuBar(Clone)/"));

                //string url = "stage";
                //string folderpath = Path.Combine(MelonHandler.PluginsDirectory, "StageBackgrounds", url);

                //if (bundle == null)
                //{
                //    bundle = AssetBundle.LoadFromFile(folderpath);
                //}

                //string[] assets = bundle.GetAllAssetNames();

                //StageInfoSO testSO = null;

                //foreach (string asset in assets)
                //{
                //    if (asset.Contains(".ISSSO"))
                //    {
                //        testSO = (StageInfoSO)bundle.LoadAsset(asset);
                //        break;
                //    }
                //}

                //if (testSO != null)
                //{
                //    StageInfo inf = stageCanvas.transform.Find("WholeUI/StageButtons/StageButtonsContainer/StageButtonsBG/MoonlitGates").GetComponent<StageSelectButton>().StageInfo;
                //    inf.StageColor = testSO.StageColor;

                //}

                //string[] allStageDirectories = Directory.GetFiles(Path.Combine(MelonHandler.PluginsDirectory, "StageBackgrounds"), "mod.json", SearchOption.AllDirectories);

                //MetadataBase mb = MetadataBase.Load(allStageDirectories[0]);

                //StageInfo inf = stageCanvas.transform.Find("WholeUI/StageButtons/StageButtonsContainer/StageButtonsBG/MoonlitGates").GetComponent<StageSelectButton>().StageInfo;
                //inf.StageVideoURL = "C:/Users/makar/Downloads/flashbang.mp4";

                //System.Type t = inf.GetType();
                //t.GetProperty("StageDisplayName").SetMethod.Invoke(inf, new object[] { mb.Title });
                //t.GetProperty("StageDescription").SetMethod.Invoke(inf, new object[] { mb.Description });
                //t.GetProperty("StageLocation").SetMethod.Invoke(inf, new object[] { mb.StageLocation });

                //string[] myColor = mb.Color.Split(';');
                //Color color = new Color(float.Parse(myColor[0]), float.Parse(myColor[1]), float.Parse(myColor[2]), 1);

                //t.GetProperty("StageColor").SetMethod.Invoke(inf, new object[] { color });

                stageCanvas.AddComponent<SSSManager>();

                stageCanvas.SetActive(false);

                GlobalManager.Instance.SaveManager.SaveBool("Final-Frontier-unlocked", true, true);
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
