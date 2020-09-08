using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticListWeapons
{
    private static List<Weapons> ListAllWeapons = new List<Weapons>();

    public static void AddWeapon(Weapons weapon) => ListAllWeapons.Add(weapon);

    public static List<Weapons> GetListAllWeapons() { return ListAllWeapons;  }

}
