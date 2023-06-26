using System;
using UnityEngine;

namespace Ruvah.GameTags
{
	public static partial class GameTags
	{
		// -- PROPERTIES

		public static GameTagDatabase Database
		{
			get
			{
				if ( GameTagDatabase == null )
				{
					ReloadGameTagDatabase();
				}

				return GameTagDatabase;
			}
		}

		// -- FIELDS

		public static event Action OnDatabaseRefreshed;
		private static GameTagDatabase GameTagDatabase;
		private static GameTagSettings Settings;

		// -- METHODS

		private static void ReloadGameTagDatabase()
		{
			LoadSettings();

			GameTagDatabase = GameTagUtility.LoadDatabaseFromXML( Settings.DatabaseFile );

			OnDatabaseRefreshed?.Invoke();
		}

		private static void LoadSettings()
		{
			Settings = null;
#if UNITY_EDITOR
			if ( !Application.isPlaying )
			{
				Settings = GameTagUtility.LoadOrCreateSettings();
				return;
			}
#endif

			Settings = Resources.Load<GameTagSettings>( GameTagUtility.ResourcesPath );
			Debug.Assert( Settings, $"Could not find resource {GameTagUtility.ResourcesPath}" );
		}

		public static GameTag FindTagByFullName( string full_name )
		{
			return Database.FindTagByFullName( full_name );
		}

		public static GameTag FindOrCreateTag( string full_name )
		{
			return Database.FindOrCreateTag( full_name );
		}
	}
}
