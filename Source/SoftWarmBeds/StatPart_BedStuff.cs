using System.Text;
using RimWorld;
using Verse;

namespace SoftWarmBeds;

public class StatPart_BedStuff : StatPart
{
    private readonly StatDef additiveStat = null;

    private readonly StatDef multiplierStat = null;
    private float Addend;

    private float Factor;

    public override string ExplanationPart(StatRequest req)
    {
        if (!req.HasThing || req.Thing is not Building_Bed)
        {
            return null;
        }

        var stringBuilder = new StringBuilder();
        var bedComp = req.Thing.TryGetComp<CompMakeableBed>();
        string material = null;
        if (bedComp != null)
        {
            if (bedComp.Loaded)
            {
                var bedStuff = bedComp.blanketStuff;
                material = bedStuff.label;
            }
            else
            {
                material = "NoBeddings".Translate();
            }
        }
        else if (req.StuffDef != null)
        {
            material = req.StuffDef.label;
        }

        if (material == null)
        {
            return null;
        }

        var number = Addend.ToStringByStyle(parentStat.ToStringStyleUnfinalized);
        if (additiveStat != null)
        {
            stringBuilder.AppendLine("StatsReport_Material".Translate() + " (" + material + "): +" + number);
        }

        if (multiplierStat != null)
        {
            stringBuilder.AppendLine("StatsReport_StuffEffectMultiplier".Translate() + ": x" +
                                     Factor.ToStringPercent("F0"));
        }

        return stringBuilder.ToString().TrimEndNewlines();
    }

    public override void TransformValue(StatRequest req, ref float val)
    {
        if (!req.HasThing || req.Thing is not Building_Bed)
        {
            return;
        }

        var addend = additiveStat != null ? selectValue(req, additiveStat) : 0f;
        var factor = multiplierStat != null ? selectValue(req, multiplierStat) : 0f;
        if (multiplierStat != null)
        {
            if (additiveStat != null)
            {
                val += factor * addend;
            }
            else
            {
                val *= factor;
            }
        }

        val += addend;
        Factor = factor;
        Addend = addend;
    }

    private float selectValue(StatRequest req, StatDef stat)
    {
        if (stat == null)
        {
            return 0f;
        }

        var bedComp = req.Thing.TryGetComp<CompMakeableBed>();
        ThingDef stuff = null;
        if (bedComp == null)
        {
            if (req.StuffDef != null)
            {
                stuff = req.StuffDef; // no comp (Bedroll)
            }
            else if (stat == additiveStat)
            {
                return 0f; // no comp, no stuff (SleepingSpot)
            }
        }
        else
        {
            if (bedComp.Loaded)
            {
                stuff = bedComp.blanketStuff; // comp = stuff from bedding
            }
            else if (stat == additiveStat)
            {
                return 0f; // unmade bed = additive zero
            }
        }

        var both = additiveStat != null && multiplierStat != null;
        if (both)
        {
            return stat == additiveStat
                ? stuff.GetStatValueAbstract(stat)
                : // if additive = get it from bed/bedding stuff
                //return req.Def.GetStatValueAbstract(stat, null); // Changed on 1.1: method transfered to a Def child:
                req.BuildableDef.GetStatValueAbstract(stat); // if multiplier = get it from bed
        }

        return stat == additiveStat
            ? stuff.GetStatValueAbstract(stat)
            : // just additive = get it from bed/bedding stuff
            //return req.Def.GetStatValueAbstract(stat, null); // Changed on 1.1: method transfered to a Def child:
            req.BuildableDef.GetStatValueAbstract(stat); // just multiplier (just in case) = get from bed
    }
}