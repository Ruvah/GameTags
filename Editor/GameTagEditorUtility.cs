using UnityEditor;
using UnityEngine;

namespace Ruvah.GameTags
{
    public static class GameTagEditorUtility
    {
        // -- FIELDS

        private static readonly string defaultSettingsPath = $"{Application.dataPath}/GameTags/GameTagSettings";

        // -- METHODS

        public static GameTagSettings LoadOrCreateSettings()
        {
            string[] setting_guids = AssetDatabase.FindAssets( "t:GameTagSettings" );

            if ( setting_guids.Length > 0 )
            {
                if ( setting_guids.Length > 1 )
                {
                    Debug.LogWarning( "Multiple assets for GameTagSettings found!" );
                }

                return AssetDatabase.LoadAssetAtPath<GameTagSettings>(
                    AssetDatabase.GUIDToAssetPath( setting_guids[0] ) );
            }

            Debug.Log( $"Creating Settings asset for GameTags at {defaultSettingsPath}" );
            GameTagSettings new_asset = ScriptableObject.CreateInstance<GameTagSettings>();
            AssetDatabase.CreateAsset( new_asset, defaultSettingsPath );
            return new_asset;
        }
    }
}
