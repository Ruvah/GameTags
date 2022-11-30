using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ruvah.GameTags
{
	[XmlRoot( "GameTag",
		IsNullable = false )]
	public class GameTagData
	{
		[XmlAttribute( "Name" )] public string TagName;

		[XmlArray( "SubTags" ),
		 XmlArrayItem( "GameTag" )]
		public List<GameTagData> SubTags;
	}

	[XmlRoot( "GameTagDatabase",
		IsNullable = false )]
	public class GameTagDataCollection
	{
		[XmlArray( "GameTagArray" ),
		 XmlArrayItem( "GameTag" )]
		public List<GameTagData> GameTagArray;
	}
}

