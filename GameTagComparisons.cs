using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruvah.GameTags
{
	public partial class GameTag
	{
		// -- METHODS

		public override bool Equals( object obj )
		{
			return MatchesExact( obj as GameTag );
		}

		public bool MatchesID( GameTag other )
		{
			return other != null && other.UniqueID == UniqueID;
		}

		public bool MatchesExact( GameTag other )
		{
			return MatchesID( other );
		}

		public bool HasTag( GameTag other )
		{
			if ( IsNullOrInvalid( other ) )
			{
				return false;
			}

			return MatchesExact( other )
			       || childTags.Any( child_tag => child_tag.HasTag( other ) );
		}

		public bool HasAnyTag( IReadOnlyCollection<GameTag> game_tags )
		{
			if ( game_tags == null )
			{
				throw new ArgumentNullException( nameof(game_tags) );
			}

			return game_tags.Any( HasTag );
		}

		public bool HasAll( IReadOnlyCollection<GameTag> game_tags )
		{
			if ( game_tags == null )
			{
				throw new ArgumentNullException( nameof(game_tags) );
			}

			return game_tags.All( HasTag );
		}

		public bool MatchesAnyExact( IReadOnlyCollection<GameTag> game_tags )
		{
			if ( game_tags == null )
			{
				throw new ArgumentNullException( nameof(game_tags) );
			}

			return game_tags.Any( MatchesExact );
		}

		public bool MatchesAllExact( IReadOnlyCollection<GameTag> game_tags )
		{
			if ( game_tags == null )
			{
				throw new ArgumentNullException( nameof(game_tags) );
			}

			return game_tags.All( MatchesExact );
		}

		public bool NameEquals( string name )
		{
			return TagName.Equals( name, StringComparison.InvariantCulture );
		}

		public bool FullNameEquals( string full_name )
		{
			return fullName.Equals( full_name, StringComparison.InvariantCulture );
		}
	}
}
