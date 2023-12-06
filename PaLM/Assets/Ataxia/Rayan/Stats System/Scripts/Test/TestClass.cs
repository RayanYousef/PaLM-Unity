using Stats_System;
using System;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    [SerializeField] private Stat<float> floatNumber;
    //[SerializeField] private List<Stat<int>> intNumber = new List<Stat<int>>();
    [SerializeField] private int counter;
    [Header("New Stats Class")]
    [SerializeField] GenericStats stats;
    //[SerializeField] private List<BaseClass> stats = new List<BaseClass>();
    //[SerializeField] IntStat HP;
    //[SerializeField] FloatStat Attack;
    DynamicStructVariableSetterGetter setterGetter
        = new DynamicStructVariableSetterGetter();


    private void Start()
    {
        //statList.Add(floatNumber);
        //statList.Add(intNumber);
        //stats.Add(HP); 
        //stats.Add(Attack);
        //stats.OnAnyStatUpdated += DebugLogStatUpdated;
        floatNumber.OnStatUpdated += DebugLogStatUpdated;

    }

    public void AddNumbers()
    {
        counter++;
        //stats.Level.AddModifier(counter.ToString(), counter);
        Debug.Log(floatNumber);
        var x= stats.GetStatOfType<int>(StatType.lvl);
        // floatNumber.AddModifier(counter.ToString(), counter);


    }
    public void DebugLogStatUpdated(object stat)
    {
        Debug.Log($"Stat Updated{stat}");
    }
}

//[Serializable]
//public class StatBase
//{
//    public enum StatType { FLOAT, INT }

//    [Header("Stat Type")]
//    [SerializeField] private StatType type;
//    [Header("Stat")]
//    [SerializeField] private Stat<float> floatValue = new Stat<float>();

//}


//[System.Serializable]
//public class BaseClass
//{

//}

//[System.Serializable]
//public class IntStat : BaseClass
//{
//    [SerializeField] // Show this field in the Unity Inspector
//    private int intValue = 20;

//    public int IntValue
//    {
//        get { return intValue; }
//        set { intValue = value; }
//    }
//}

//[System.Serializable]
//public class FloatStat : BaseClass
//{
//    [SerializeField] // Show this field in the Unity Inspector
//    private float floatValue = 20.2f;

//    public float FloatValue
//    {
//        get { return floatValue; }
//        set { floatValue = value; }
//    }
//}
