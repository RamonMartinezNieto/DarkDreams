using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class UserScore 
{
    public string name;
    public int score;

    public UserScore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }


    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["name"] = name;
        result["score"] = score;

        return result;
    }
}
