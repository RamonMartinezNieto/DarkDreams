/**
 * Department: Game Developer
 * File: UserScore.cs
 * Objective: UserScore to save and show the score in the laderboard.
 * Employee: Ramón Martínez Nieto
 */
using System; 
using System.Collections.Generic;

/**
 * Serializable class  to save and show the users scores
 * 
 * @author Ramón Martínez Nieto
 * @see User
 */
[Serializable]
public class UserScore : User
{
    /**
     * Score of the user 
     */
    public int score;
    /**
     * Time of the round 
     */
    public string time; 

    /**
     * Constructor 
     * 
     * @param string name 
     * @param string score
     * @param string time 
     * @see User#User
     */
    public UserScore(string name, int score, string time) : base (name)
    {
        this.score = score;
        this.time = time;
    }

    /**
     * Override Dictionary, important to show results in the laderboard
     * 
     * @return Dictionary<string, object> with the results
     * @see User#ToDictionary
     */
    public override Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["name"] = name;
        result["score"] = score;
        result["time"] = time;

        return result;
    }
}
