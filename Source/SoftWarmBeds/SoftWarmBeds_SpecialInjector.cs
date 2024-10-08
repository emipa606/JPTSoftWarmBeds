﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace SoftWarmBeds;

public sealed class SoftWarmBeds_SpecialInjector
{
    public readonly double adjust = 0.25;

    public float CalculateSoftness(ThingDef def)
    {
        return 1 - (ArmorGrade(def) / ((1 + (FurFactor(def) * 2) + (ValueFactor(def) / 2)) / 2)) - (float)adjust;
    }

    public void Inject()
    {
        var texDefs = DefDatabase<ThingDef>.AllDefsListForReading.Where(x =>
            x.stuffProps != null && (x.stuffProps.categories.Contains(StuffCategoryDefOf.Leathery) ||
                                     x.stuffProps.categories.Contains(StuffCategoryDefOf.Fabric)));
        InjectStatBase(texDefs);
    }

    private float ArmorGrade(ThingDef def)
    {
        var blunt = Math.Min(1, def.statBases.GetStatValueFromList(StatDefOf.StuffPower_Armor_Blunt, 0f));
        var sharp = Math.Min(1, def.statBases.GetStatValueFromList(StatDefOf.StuffPower_Armor_Sharp, 0f));
        return (blunt + sharp) / 2;
    }

    private float FurFactor(ThingDef def)
    {
        var heat = def.statBases.GetStatValueFromList(StatDefOf.StuffPower_Insulation_Heat, 0f);
        var cold = def.statBases.GetStatValueFromList(StatDefOf.StuffPower_Insulation_Cold, 0f);
        var delta = Math.Abs(heat - cold);
        var reach = ((heat + cold) / 2) - 12;
        return Math.Max(delta, reach) / 10;
    }

    private float ValueFactor(ThingDef def)
    {
        var val = def.statBases.GetStatValueFromList(StatDefOf.MarketValue, 0f);
        return (float)Math.Pow(val, 1.0 / 3.0);
    }

    private void InjectStatBase(IEnumerable<ThingDef> list)
    {
        var stringBuilder = new StringBuilder("[SoftWarmBeds] Added softness stat to: ");
        foreach (var thingDef in list)
        {
            var statModifier = new StatModifier
            {
                stat = BedStatDefOf.Textile_Softness,
                value = CalculateSoftness(thingDef)
            };
            thingDef.statBases.Add(statModifier);
            stringBuilder.Append($"{thingDef.defName} ({statModifier.value.ToStringPercent()}), ");
        }

        Log.Message(stringBuilder.ToString().TrimEnd(' ', ','));
    }
}