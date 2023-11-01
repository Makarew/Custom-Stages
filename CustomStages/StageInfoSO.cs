using UnityEngine;

namespace diorama
{
    [CreateAssetMenu(fileName = "StageInfo", menuName = "Stage Info", order = 0)]
    public class StageInfoSO : ScriptableObject
    {
        public Color StageColor;
        public string StageDescription;
        public string StageDisplayName;

        public Sprite StageImage;
        public string StageLocation;
    }
}
