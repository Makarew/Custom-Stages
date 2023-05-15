using IdolShowdown;
using JetBrains.Annotations;
using MelonLoader;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CustomStages
{
    public class Plugin : MelonMod
    {
        internal AssetBundle bundle;

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasLoaded(buildIndex, sceneName);

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

                string url = "stage";
                string folderpath = Path.Combine(MelonHandler.PluginsDirectory, "StageBackgrounds", url);

                if (bundle == null)
                {
                    bundle = AssetBundle.LoadFromFile(folderpath);
                }

                GameObject customStage = GameObject.Instantiate((GameObject)bundle.LoadAsset("CustomStage"));

                //Texture2D tex;
                //tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
                //WWW www = new WWW(folderpath);

                //www.LoadImageIntoTexture(tex);

                //GameObject background = new GameObject();
                //background.transform.SetParent(ground);

                //background.AddComponent<SpriteRenderer>();
                //background.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);

                //GameObject ground = GameObject.Instantiate(new GameObject());
                //ground.name = "Ground";
                //ground.transform.position = new Vector3(0, -2.65f, 0);

                //ground.AddComponent<BoxCollider2D>();
                //ground.GetComponent<BoxCollider2D>().bounds.center.Set(0, -4.7f, 0);
                //ground.GetComponent<BoxCollider2D>().bounds.center.Set(5, 0.5f, 0);
                //ground.GetComponent<BoxCollider2D>().size = new Vector2(10, 0.9444f);
                //ground.GetComponent<BoxCollider2D>().offset = new Vector2(0, -2.0278f);

                //ground.layer = 8;
                
                //ground.AddComponent<FixedBoxCollider>();
                //ground.AddComponent<FixedRigidBody>();
                //ground.AddComponent<Wall>();
            }
        }
    }
}
