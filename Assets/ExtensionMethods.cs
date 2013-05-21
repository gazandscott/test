using System;

public static class ExtensionMethods
{
    public static string GetName(this Species species)
    {
        if (species == Species.CLOVER)
		{
			return "Clover";
		}
		else if (species == Species.MARIGOLD)
		{
			return "Marigold";
		}
		else if (species == Species.TOMATO)
		{
			return "Tomato";
		}
		
		return null;
    }
}
