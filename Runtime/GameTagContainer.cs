using System;
using UnityEngine;

namespace Ruvah.GameTags
{
	[Serializable]
	public class GameTagContainer
	{
		// -- PROPERTIES

		public GameTag GameTag
		{
			get
			{

				if ( !Application.isPlaying
				     || gameTag == null
				     || Equals( gameTag, GameTag.InvalidTag ) )
				{
					gameTag = GameTags.FindTagByFullName( FullTagName );
				}

				return gameTag;
			}
		}

		// -- FIELDS

		private GameTag gameTag;
		public string FullTagName;

		// -- METHODS

		public static implicit operator GameTag( GameTagContainer game_tag_property )
		{
			return game_tag_property.GameTag;
		}
	}
}
