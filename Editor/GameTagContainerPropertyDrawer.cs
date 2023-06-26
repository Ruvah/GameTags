using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Ruvah.GameTags
{
	public class GameTagDropdown : AdvancedDropdown
	{
		// -- PROPERTIES

		public int SelectedItemID
		{
			get => selectedItemID;
			set => selectedItemID = value;
		}

		// -- FIELDS

		private int selectedItemID = GameTag.InvalidID;

		// -- METHODS

		public GameTagDropdown( AdvancedDropdownState state ) : base( state )
		{
		}

		protected override void ItemSelected( AdvancedDropdownItem item )
		{
			base.ItemSelected( item );

			selectedItemID = item.id;
		}

		private void AddTagAndChildrenToItem( GameTag tag, AdvancedDropdownItem root_item )
		{
			var item = new AdvancedDropdownItem( tag.TagName )
			{
				id = tag.UniqueID
			};

			if ( tag.ChildTags.Count > 0 )
			{
				var parent_item = new AdvancedDropdownItem( tag.TagName )
				{
					id = tag.UniqueID
				};
				item.AddChild( parent_item );
				item.AddSeparator();

				foreach ( var child_tag in tag.ChildTags )
				{
					AddTagAndChildrenToItem( child_tag, item );
				}
			}

			root_item.AddChild( item );
		}

		protected override AdvancedDropdownItem BuildRoot()
		{
			IReadOnlyList<GameTag> tags = GameTags.Database.Tags;
			var root = new AdvancedDropdownItem( "Tags" );
			foreach ( var game_tag in tags )
			{
				AddTagAndChildrenToItem( game_tag, root );
			}

			return root;
		}
	}

	[CustomPropertyDrawer( typeof(GameTagContainer) )]
	public class GameTagContainerPropertyDrawer : PropertyDrawer
	{
		// -- FIELDS

		private GameTagDropdown tagDropDown;
		private bool shouldRefreshDropdown;


		// -- METHODS

		public GameTagContainerPropertyDrawer()
		{
			GameTags.OnDatabaseRefreshed += EditorGameTags_OnDatabaseRefreshed;
		}

		private void EditorGameTags_OnDatabaseRefreshed()
		{
			shouldRefreshDropdown = true;
		}

		~GameTagContainerPropertyDrawer()
		{
			GameTags.OnDatabaseRefreshed -= EditorGameTags_OnDatabaseRefreshed;
		}

		private void RefreshDropdown( SerializedProperty full_tag_name_property )
		{
			tagDropDown = new GameTagDropdown( new AdvancedDropdownState() )
			{
				SelectedItemID = GameTags.Database
					.FindTagByFullName( full_tag_name_property.stringValue ).UniqueID
			};
		}

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			SerializedProperty full_tag_name_property = property.FindPropertyRelative( "FullTagName" );

			if ( tagDropDown == null || shouldRefreshDropdown )
			{
				RefreshDropdown( full_tag_name_property );
				shouldRefreshDropdown = false;
			}

			EditorGUI.BeginProperty( position, label, property );

			var property_position =
				EditorGUI.PrefixLabel( position, GUIUtility.GetControlID( FocusType.Keyboard ), label );
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			var drop_down_button_rect = new Rect( property_position.x, property_position.y, property_position.width,
				position.height );
			var drop_down_rect = new Rect( property_position.x, property_position.y, property_position.width,
				property_position.height );

			if ( EditorGUI.DropdownButton( drop_down_button_rect, new GUIContent( full_tag_name_property.stringValue ),
				    FocusType.Keyboard ) )
			{
				tagDropDown.Show( drop_down_rect );
			}

			full_tag_name_property.stringValue =
				GameTags.Database.FindTagByID( tagDropDown.SelectedItemID ).FullName;

			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty();
			property.serializedObject.ApplyModifiedProperties();
		}
	}
}
