using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruvah.GameTags
{
    public class GameTagDatabase
    {
        // -- FIELDS

        public IReadOnlyList<GameTag> Tags => rootTags;

        private readonly List<GameTag> rootTags;
        private readonly List<GameTag> allGameTagsList = new();

        // -- METHODS

        public GameTagDatabase( List<GameTag> root_tags )
        {
            rootTags = root_tags;
            UpdateAllGameTags();
        }

        public GameTagDatabase() : this( new List<GameTag>() )
        {

        }

        private void UpdateAllGameTags()
        {
            allGameTagsList.Clear();

            rootTags.DeepForeach( ( game_tag ) => allGameTagsList.Add( game_tag ) );
        }

        public GameTag FindTagByID( int id )
        {
            GameTag result = allGameTagsList.Find( ( tag ) => tag.UniqueID == id );
            return result ?? GameTag.InvalidTag;
        }

        public GameTag FindTagByFullName( string full_name )
        {
            GameTag result = allGameTagsList.Find( ( tag ) => tag.FullNameEquals( full_name ) );
            return result ?? GameTag.InvalidTag;
        }

        public bool TryFindTagByFullName( string gameTagName, out GameTag result )
        {
            result = FindTagByFullName( gameTagName );
            return !GameTag.IsNullOrInvalid( result );
        }

        public bool TryFindTagByID( int id, out GameTag result )
        {
            result = FindTagByID( id );
            return !GameTag.IsNullOrInvalid( result );
        }

        public GameTag FindOrCreateTag( string full_tag_name )
        {
            if ( TryFindTagByFullName( full_tag_name, out var existing_tag ) )
            {
                return existing_tag;
            }

            string[] split_tag_name = full_tag_name.Split( GameTag.SplitCharacter );

            GameTag parent = GameTag.InvalidTag;

            if ( split_tag_name.Length > 1 )
            {
                StringBuilder parent_name = new StringBuilder();

                for ( int i = 0, remaining_levels = split_tag_name.Length - 2;
                     i < split_tag_name.Length - 1;
                     i++, remaining_levels-- )
                {
                    parent_name.Append( split_tag_name[i] );
                    if ( remaining_levels > 0 )
                    {
                        parent_name.Append( GameTag.SplitCharacter );
                    }
                }

                parent = FindOrCreateTag( parent_name.ToString() );
            }

            return new GameTag( split_tag_name.Last(), parent );
        }
    }
}
