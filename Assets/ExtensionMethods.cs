using System;

public static class ExtensionMethods
{
    public static string GetName(this Species species)
    {
        if (species == Species.CLOVER)
		{
			return "Clover";
		}
		else if (species == Species.FLOWER)
		{
			return "Flower";
		}
		else if (species == Species.VEGETABLE)
		{
			return "Vegetables";
		}
		
		return null;
    }
}
