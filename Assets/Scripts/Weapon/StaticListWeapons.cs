/**
 * Department: Game Developer
 * File: StaticListWeapons.cs
 * Objective: Have a static list with all weapons. 
 * Employee: Ramón Martínez Nieto
 */
using System.Collections.Generic;

/**
 * 
 * Class to have a static list with all weapons.
 * Use this list to add all of the weapons when the character catch a new weapon.
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public static class StaticListWeapons
{
    private static List<Weapons> ListAllWeapons = new List<Weapons>();

    /**
     * Add a new weapon
     */
    public static void AddWeapon(Weapons weapon) => ListAllWeapons.Add(weapon);

    /**
     * Get a list with all weapons catched.
     * 
     * @return List<Weapon> 
     */
    public static List<Weapons> GetListAllWeapons() { return ListAllWeapons; }

    /**
     * Reset the list in a new game 
     */
    public static void ResetListWeapons()
    {
        Weapons.TotalWeapons = 0;
        ListAllWeapons.Clear();
    }
}