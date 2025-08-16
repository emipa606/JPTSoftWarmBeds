# GitHub Copilot Instructions for [JPT] Soft Warm Beds (Continued)

## Mod Overview and Purpose
**[JPT] Soft Warm Beds (Continued)** is a RimWorld mod designed to enhance the realism and comfort of beds in the game. The mod removes the unrealistic use of only stone or metal for comfortable resting and introduces the need for mattresses, pillows, and blankets to improve bed comfort and warmth.

## Key Features and Systems
- **Enhanced Bed Comfort**: Beds provide full comfort only when equipped with bedding, affecting rest effectiveness and warmth.
- **Temperature Consideration**: Materials affect the insulation properties of beds, helping pawns stay warm in cold environments.
- **Hammocks**: Introduces hammocks for tropical climates, providing a cooling effect.
- **Softness Stat**: A new stat measures the comfort and rest effectiveness of bed materials by considering their innate properties like armor, temperature offsets, and luxury.
- **Compatibility**: Highly compatible with many popular mods, ensuring seamless integration with existing game installations.

## Coding Patterns and Conventions
- **Namespace Usage**: Follow standard C# conventions, such as maintaining clear and descriptive namespaces.
- **Comments and Documentation**: Use inline commenting for complex logic and provide XML documentation for public classes and methods.
- **Coding Style**: Adopt consistent brace style, indentation, and naming conventions for variables and methods.

## XML Integration
The mod uses XML patches to integrate its features with existing RimWorld objects:
- XML patches modify bed definitions to add the new `CompMakeableBed` component.
- Existing beds in a player's saved game will transition to the new system upon loading using XML patch operations.

## Harmony Patching
Harmony is extensively used for patching game methods without modifying the original code:
- **Example Patches**: 
  - `Hospitality_Patch`: Enhances compatibility with the Orion's Hospitality mod.
  - `Draw_Patch`: Adjusts rendering for the new bed components.
  - Patches to track and manage memory for insomniac colonists, ensuring they receive proper rest bonuses.

## Suggestions for Copilot
- **Code Completion**: Utilize Copilot to autocomplete repetitive code patterns, especially for creating similar classes or patching methods.
- **Auto-Documentation**: Use Copilot to generate method summaries and parameter descriptions for comprehensive documentation.
- **XML Patch Generation**: Leverage Copilot to automate the creation of XML patches for adding or modifying bed properties.
- **Performance Improvements**: Suggest optimizations for intensive calculations like the `calculateSoftness` method, which evaluates bed softness based on complex logic.

By adhering to these instructions, developers working on the [JPT] Soft Warm Beds (Continued) mod can maintain consistency, ensure compatibility, and efficiently expand the mod's functionalities.

This `.github/copilot-instructions.md` file provides guidance and best practices for using GitHub Copilot effectively in the context of the [JPT] Soft Warm Beds (Continued) mod project, focusing on maintaining the mod's high-quality standards and compatibility with the RimWorld community.
