using System;
using System.Collections.Generic;

[Serializable]
public class User 
{
    public string name;
    public User(string name)
    {
        this.name = name;
    }

    public virtual Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["name"] = name;

        return result;
    }
}
