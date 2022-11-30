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

#if UNITY_EDITOR
				if ( !Application.isPlaying )
				{
					return GameTag.InvalidTag;
				}
#endif

				if ( gameTag == null || gameTag == GameTag.InvalidTag )
				{
					gameTag = GameTags.Instance.FindTagByFullName( FullTagName );
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
