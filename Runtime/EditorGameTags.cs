#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Ruvah.GameTags
{
    public static partial class GameTags
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

        // -- FIELDS



        // -- METHODS

        private static void RefreshDatabase()
        {

            if ( Application.isPlaying )
            {
                Debug.LogWarning( "Refreshing GameTags in editor is not possible while playing" );
            }
            else
            {
                ReloadGameTagDatabase();

            }

        }
    }
}

#endif
