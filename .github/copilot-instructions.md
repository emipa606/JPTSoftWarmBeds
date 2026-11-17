.github/copilot-instructions.md

# [JPT] Soft Warm Beds (Continued) GitHub Copilot Instructions

## Mod Overview and Purpose

The [JPT] Soft Warm Beds (Continued) mod for RimWorld updates and enhances the vanilla bed system by introducing softness and insulation properties. This mod allows players to create more comfortable and effective beds by using various bedding materials, such as mattresses, pillows, and blankets. It also includes a hammock alternative for tropical climates.

## Key Features and Systems

1. **Softness Stat**: A new stat that measures how much a material can contribute to a bed's comfort and rest effectiveness.
2. **Bedding Materials**: Players can make mattresses, pillows, and blankets at tailor benches or crafting spots after researching complex furniture.
3. **Insulation Properties**: The material insulation properties carry over to the bed, helping keep pawns warm and preventing negative thoughts when sleeping in cold.
4. **Hammocks**: A tropical alternative to beds that helps cooling off, making it a useful travel bed as well.
5. **XML Integration**: Simple XML patches can be used to incorporate the basic features into other beds.
6. **Compatibility**: The mod is compatible with a variety of other mods, including Baby and Children, Children and Pregnancy, GloomyFurniture, Gloomy Vanilla, Nano Repair Tech, Orion's Hospitality, Polyamory Beds, Rice Cultivating Civilization, RIMkea, Sparkling Worlds, Vanilla Furniture Expanded, and more.

## Coding Patterns and Conventions

1. **C# Files**: The mod uses C# for its core functionality, including Harmony patches for modifying RimWorld's behavior.
2. **XML Files**: XML patches are used to define new stats, job definitions, thing categories, thing definitions, and work giver definitions.
3. **Harmony Patching**: Harmony is used for patching the game code to add or modify functionality.
4. **Legacy Code**: Some legacy code is preserved for compatibility with older versions of the mod.

## XML Integration

The mod uses XML files to define new game elements such as stats, jobs, thing categories, things, and work givers. These files are located in the `Defs` directory.

Example XML file: `Defs/Stats.xml`

xml
<?xml version="1.0" encoding="utf-8"?>
<Defs>
  <StatDef>
    <defName>Softness</defName>
    <label>Softness</label>
    <description>A measure of how much a material can contribute to a bed's comfort and rest effectiveness.</description>
    <calculator>
      <type>BaseValue</type>
      <baseValue>0</baseValue>
    </calculator>
  </StatDef>
</Defs>


## Harmony Patching

Harmony is used to patch the game code to add or modify functionality. The mod includes several Harmony patches for various game systems, such as drawing, hospitalities, and pawn rendering.

Example Harmony patch: `PatchOperationCopy.cs`

csharp
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace JPTSoftWarmBeds
{
    public class PatchOperationCopy : PatchOperation
    {
        public List<CodeInstruction> instructions;

        public override IEnumerable<CodeInstruction> Transpile(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            List<CodeInstruction> newInstructions = new List<CodeInstruction>(instructions);
            // Modify the instructions as needed
            return newInstructions;
        }
    }
}


## Suggestions for Copilot

1. **XML Patches**: When writing XML patches, Copilot can help generate new stat definitions, job definitions, thing categories, thing definitions, and work giver definitions.
2. **Harmony Patches**: When writing Harmony patches, Copilot can help generate new patch operations, such as `PatchOperationCopy`, `PatchOperationFindModById`, and others.
3. **C# Code**: Copilot can help write new C# code for the mod, such as new classes, methods, and properties.
4. **Integration**: Copilot can help integrate new features into existing code, such as adding new bedding materials or modifying existing game systems.

## Contributing

If you are interested in contributing to the mod, please follow these guidelines:

1. **Fork the Repository**: Fork the [JPT] Soft Warm Beds (Continued) repository on GitHub.
2. **Clone the Repository**: Clone the forked repository to your local machine.
3. **Create a New Branch**: Create a new branch for your changes.
4. **Make Changes**: Make your changes to the mod.
5. **Test Changes**: Test your changes to ensure they work as expected.
6. **Commit Changes**: Commit your changes with a descriptive commit message.
7. **Push Changes**: Push your changes to your forked repository.
8. **Create a Pull Request**: Create a pull request to the main repository.

Thank you for your contribution!

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

