using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class UserScore 
{
    public string name;
    public int score;
    public string time; 

    public UserScore(string name, int score, string time)
    {
        this.name = name;
        this.score = score;
        this.time = time;
    }


    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["name"] = name;
        result["score"] = score;
        result["time"] = time;

        return result;
    }
}
