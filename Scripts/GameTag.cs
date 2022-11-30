using System;
using System.Collections.Generic;
using System.Text;

namespace Ruvah.GameTags
{
	public partial class GameTag
	{
		// -- PROPERTIES

		public IReadOnlyList<GameTag> ChildTags => childTags;
		public int UniqueID => uniqueID;
		public string FullName => fullName;

		// -- FIELDS

		public const int InvalidID = -1;
		public const char SplitCharacter = '.';

		public static readonly GameTag InvalidTag = new();
		private static int idCounter;

		public readonly string TagName;

		private readonly int uniqueID;
		private readonly List<GameTag> childTags = new();
		private string fullName;
		private List<string> parentNameArray = new();
		private GameTag parentTag = InvalidTag;

		// -- METHODS

		public GameTag( string tag_name, GameTag parent_tag )
		{
			uniqueID = idCounter++;
			TagName = tag_name;
			SetParentTag( parent_tag );
		}

		private GameTag()
		{
			uniqueID = InvalidID;
			TagName = "Invalid";
			SetParentTag( null );
		}

		public override int GetHashCode()
		{
			return HashCode.Combine( FullName, uniqueID );
		}

		public static bool IsNullOrInvalid( GameTag tag )
		{
			return tag == null || tag.UniqueID == InvalidID;
		}

		public override string ToString()
		{
			return FullName;
		}

		private void SetParentTag( GameTag parent )
		{
			parentTag = parent;

			if ( IsNullOrInvalid( parent ) )
			{
				parentTag = InvalidTag;
				parentNameArray.Clear();
				fullName = TagName;
			}
			else
			{
				parentNameArray = new List<string>( parent.parentNameArray ) { parent.TagName };

				StringBuilder full_name_builder = new StringBuilder();
				foreach ( var name in parentNameArray )
				{
					full_name_builder.Append( name );
					full_name_builder.Append( SplitCharacter );
				}

				full_name_builder.Append( TagName );
				fullName = full_name_builder.ToString();

				parentTag.AddAsChild( this );
			}
		}

		private void AddAsChild( GameTag child )
		{
			childTags.Add( child );
		}
	}
}
