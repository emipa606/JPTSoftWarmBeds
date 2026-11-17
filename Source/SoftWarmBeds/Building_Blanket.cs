using UnityEngine;
using Verse;

namespace SoftWarmBeds;

public class Building_Blanket : ThingWithComps
{
    public Color colorTwo;

    private static float DeltaFactor => Mathf.Round(SoftWarmBedsSettings.ColorWash * 10) / 10;

    public override Color DrawColorTwo
    {
        get => Color.Lerp(colorTwo, field, DeltaFactor);
    } = new(1f, 1f, 1f);
}