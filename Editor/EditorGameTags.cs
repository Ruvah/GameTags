using System;
using UnityEditor;
using UnityEngine;

namespace Ruvah.GameTags
{
    public static class EditorGameTags
    {
        // -- TYPES

        private class GameTagsPostProcessor : AssetPostprocessor
        {
            private static void OnPostprocessAllAssets( string[] importedAssets, string[] deletedAssets,
                string[] movedAssets,
                string[] movedFromAssetPaths )
            {
                RefreshDatabase();
            }
        }

        // -- PROPERTIES

        private static GameTagSettings GameTagSettings
        {
            get
            {
                if ( gameTagSettings == null )
                {
                    ReloadSettings();
                }

                return gameTagSettings;
            }
        }

        public static GameTagDatabase GameTagDatabase
        {
            get
            {
                if ( gameTagDatabase == null )
                {
                    ReloadDatabase();
                }

                return gameTagDatabase;
            }
        }

        // -- FIELDS

        public static event Action OnDatabaseRefreshed;
        private static GameTagSettings gameTagSettings;
        private static GameTagDatabase gameTagDatabase;


        // -- METHODS

        private static void ReloadSettings()
        {
            gameTagSettings = GameTagEditorUtility.LoadOrCreateSettings();
        }

        private static void ReloadDatabase()
        {
            gameTagDatabase = GameTagUtility.LoadDatabaseFromXML( GameTagSettings.DatabaseFile );
        }

        private static void RefreshDatabase()
        {

            if ( Application.isPlaying )
            {
                Debug.LogWarning( "Refreshing GameTags in editor is not possible while playing" );
            }
            else
            {
                ReloadSettings();
                ReloadDatabase();
                OnDatabaseRefreshed?.Invoke();
            }

        }
    }
}
