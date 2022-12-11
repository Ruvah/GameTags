# GameTags
Serializable comparable tag system with parent-child relationships for Unity.

This package is maintained by Ruben Vandermeersch (https://github.com/Ruvah)

## Features

### XML setup

Tags can be easily setup through XML following a simple structure.

![xml_setup_example.png](https://i.imgur.com/rTGUo22.pngimg.)

All that remains is referencing the XML file in the GameTagSettings Asset in the GameTags Prefab and drop it in your scene.

![prefab_view0.1](https://i.imgur.com/VBM7nLy.png)![settings_view_0.1](https://i.imgur.com/tBcrujm.png)

Tags can be added by code at runtime through `GameTags.Instance.FindOrCreateTag("ExampleTag.Child")`

### Child-parent relations

Tags have child tags and their relationship can be checked upon.

### Inspector Support

Add the GameTagContainer class to your Monobehaviours or ScriptableObjects to have inspector support for tags defined in the xml database.
The GameTagContainer is serializable.

![game_tag_container_inspector_0.1](https://i.imgur.com/TOnbiHe.gif)

### Comparisons

Described below are the multiple ways to compare game tags against each other. 

#### MatchesExact

This compares tags unique ID (int) against each other and will only return true if these match.

#### MatchesAnyExact

Returns true if an tag matches this tag exactly.

#### MatchesAllExact

Returns true if all tags matches this tag exactly.

#### HasTag

Returns true if the parameter tag, value, `MatchesExact(value)` or is a child or grand-child of this tag. 

#### HasAny

Returns true if any of the parameters matches this tag exactly or is a child or grand-child of this tag.

#### HasAll

Returns true if all of the parameters matches this tag exactly or is a child or grand-child of this tag.

