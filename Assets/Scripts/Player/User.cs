/**
 * Department: Game Developer
 * File: User.cs
 * Objective: Create a basic User only with the name
 * Employee: Ramón Martínez Nieto 
 */
using System;
using System.Collections.Generic;

/**
 * Serializable class with the name
 * 
 * @author Ramón Martínez Nieto
 */
[Serializable]
public class User 
{
    /**
     * User's name.  
     */
    public string name;

    /**
     * User's constructor 
     */
    public User(string name)
    {
        this.name = name;
    }

    /**
     * Virtual method ToDcitionary(). To set name in the other fields.
     */
    public virtual Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["name"] = name;

        return result;
    }
}
