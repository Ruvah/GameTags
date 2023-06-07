using System.IO;
using UnityEditor;
using UnityEngine;

namespace Ruvah.GameTags
{
    public static class GameTagEditorUtility
    {
        // -- FIELDS

        private static readonly string settingsFolderName = "GameTags";
        private static readonly string absoluteSettingsFolderPath = $"{Application.dataPath}/{settingsFolderName}";
        private static readonly string settingsAssetPath = $"Assets/{settingsFolderName}/GameTagSettings.asset";

        
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

            Debug.Log( $"Creating Settings asset for GameTags at {absoluteSettingsFolderPath}" );
            GameTagSettings new_asset = ScriptableObject.CreateInstance<GameTagSettings>();
            if ( !Directory.Exists( absoluteSettingsFolderPath ) )
            {
                Directory.CreateDirectory( absoluteSettingsFolderPath );
            }

            AssetDatabase.CreateAsset( new_asset, settingsAssetPath );
            return new_asset;
        }
    }
}
