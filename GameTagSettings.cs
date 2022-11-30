using UnityEngine;

namespace Ruvah.GameTags
{
    [CreateAssetMenu( menuName = "GameTags/Settings" )]
    public class GameTagSettings : ScriptableObject
    {
        public TextAsset DatabaseFile;
    }
}
