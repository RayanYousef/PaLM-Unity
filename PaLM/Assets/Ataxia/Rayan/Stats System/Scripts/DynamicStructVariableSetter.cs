using Stats_System;
using System.Reflection;

public class DynamicStructVariableSetterGetter
{
    public CharacterStats SetPropertyValueByName(CharacterStatsType propertyName, object propertyValue, CharacterStats playerData)
    {
        PropertyInfo propertyInfo = typeof(CharacterStats).GetProperty(propertyName.ToString());
        object playerDataBoxed = playerData;
        propertyInfo.SetValue(playerDataBoxed, propertyValue, null);
        playerData = (CharacterStats)playerDataBoxed;

        return playerData;
    }

    public object GetPropertyValueByName(StatType propertyName, GenericStats playerData)
    {
        PropertyInfo property = typeof(GenericStats).GetProperty(propertyName.ToString());

        return property.GetValue(playerData, null);
    }   
}
