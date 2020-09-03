using System;
using System.Collections.Generic;

[Serializable]
public class UserScore : User
{
    public int score;
    public string time; 

    public UserScore(string name, int score, string time) : base (name)
    {
        this.score = score;
        this.time = time;
    }

    public override Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["name"] = name;
        result["score"] = score;
        result["time"] = time;

        return result;
    }
}
