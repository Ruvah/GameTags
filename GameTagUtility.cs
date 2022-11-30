﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Ruvah.GameTags
{
	public static class GameTagUtility
	{
		public static void DeepForeach( this IEnumerable<GameTag> tags, Action<GameTag> action )
		{
			foreach ( var game_tag in tags )
			{
				action.Invoke( game_tag );
				DeepForeach( game_tag.ChildTags, action );
			}
		}

		private static GameTagDataCollection ParseXmlFileToDataCollection( TextAsset xml_asset )
		{
			XmlSerializer serializer = new XmlSerializer( typeof(GameTagDataCollection) );

			using var reader = new StringReader( xml_asset.text );
			var data_collection = serializer.Deserialize( reader ) as GameTagDataCollection;

			return data_collection;
		}

		private static GameTagDatabase LoadGameTagDatabase( GameTagDataCollection data_collection )
		{
			List<GameTag> game_tag_list = new List<GameTag>( data_collection.GameTagArray.Count );
			foreach ( var game_tag_data in data_collection.GameTagArray )
			{
				game_tag_list.Add( LoadGameTag( game_tag_data, GameTag.InvalidTag ) );
			}

			return new GameTagDatabase( game_tag_list );
		}

		public static GameTagDatabase LoadDatabaseFromXML( TextAsset xml_asset )
		{
			if ( !xml_asset )
			{
				Debug.LogWarning( "No xml database is set for GameTags in settings using empty database" );
				return new GameTagDatabase();
			}

			return LoadGameTagDatabase( ParseXmlFileToDataCollection( xml_asset ) );
		}

		private static GameTag LoadGameTag( GameTagData tag_data, GameTag parent )
		{
			GameTag tag = new GameTag( tag_data.TagName, parent );

			foreach ( var child_tag_data in tag_data.SubTags )
			{
				LoadGameTag( child_tag_data, tag );
			}

			return tag;
		}
	}
}
