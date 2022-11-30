using UnityEngine;

namespace Ruvah.GameTags
{
	public class GameTags : MonoBehaviour
	{
		// -- PROPERTIES

		public static GameTags Instance
		{
			get
			{
				Debug.Assert( instance, "No GameObject with instance of {nameof(GameTags)} alive" );

				return instance;
			}
		}

		// -- FIELDS

		private static GameTags instance;

		private GameTagDatabase gameTagDatabase;

		[SerializeField] private GameTagSettings settings;

		// -- METHODS

		private void Awake()
		{
			DontDestroyOnLoad( gameObject );
			instance = this;
			gameTagDatabase = GameTagUtility.LoadDatabaseFromXML( settings.DatabaseFile );
		}

		public GameTag FindTagByFullName( string full_name )
		{
			return gameTagDatabase.FindTagByFullName( full_name );
		}

		public GameTag FindOrCreateTag( string full_name )
		{
			return gameTagDatabase.FindOrCreateTag( full_name );
		}
	}
}
