// ArcanaUtils.cs
public static class ArcanaUtils
{
    // Get the traditional number associated with the Arcana
    public static int GetArcanaNumber(ArcanaType arcana)
    {
        return arcana switch
        {
            ArcanaType.Fool => 0,
            ArcanaType.Magician => 1,
            ArcanaType.Priestess => 2,
            ArcanaType.Empress => 3,
            ArcanaType.Emperor => 4,
            ArcanaType.Hierophant => 5,
            ArcanaType.Lovers => 6,
            ArcanaType.Chariot => 7,
            ArcanaType.Justice => 8,
            ArcanaType.Hermit => 9,
            ArcanaType.Fortune => 10,
            ArcanaType.Strength => 11,
            ArcanaType.HangedMan => 12,
            ArcanaType.Death => 13,
            ArcanaType.Temperance => 14,
            ArcanaType.Devil => 15,
            ArcanaType.Tower => 16,
            ArcanaType.Star => 17,
            ArcanaType.Moon => 18,
            ArcanaType.Sun => 19,
            ArcanaType.Judgement => 20,
            ArcanaType.World => 21,
            _ => -1
        };
    }

    // Get display name (can be used for UI)
    public static string GetDisplayName(ArcanaType arcana)
    {
        return arcana switch
        {
            ArcanaType.HangedMan => "The Hanged Man",
            ArcanaType.World_Unused => "The World",
            ArcanaType.Faith_Unused => "Faith",
            ArcanaType.Aeon_P3 => "Aeon",
            ArcanaType.Faith_P5R => "Faith",
            _ => $"The {arcana}"
        };
    }

    // Check if Arcana is from base set (0-21)
    public static bool IsBaseArcana(ArcanaType arcana)
    {
        return GetArcanaNumber(arcana) >= 0;
    }

    // Get Arcana affinity level (useful for fusion calculations)
    public static int GetAffinity(ArcanaType arcana1, ArcanaType arcana2)
    {
        // Implementation would contain affinity rules
        // Returns a value indicating how well these Arcana work together
        // This would be used in the Persona fusion system
        return 0; // Placeholder
    }
}