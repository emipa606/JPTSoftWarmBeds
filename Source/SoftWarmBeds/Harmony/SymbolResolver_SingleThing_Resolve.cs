using HarmonyLib;
using RimWorld;
using RimWorld.BaseGen;

namespace SoftWarmBeds;

//Generates beddings for beds on generated bases.
[HarmonyPatch(typeof(SymbolResolver_SingleThing), nameof(SymbolResolver_SingleThing.Resolve))]
public static class SymbolResolver_SingleThing_Resolve
{
    public static Faction cachedFaction;

    public static void Prefix(ResolveParams rp)
    {
        cachedFaction = rp.faction;
        ThingMaker_MakeThing.Act = cachedFaction != null;
    }
}