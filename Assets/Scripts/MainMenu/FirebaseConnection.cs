using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseConnection : MonoBehaviour
{
    public static FirebaseConnection Instance = null;

    //Use this bool to know when the list is already
    public static bool finish;

    private DatabaseReference dataReference;
    private FirebaseDatabase instance;
   
    private List<UserScore> usersList = new List<UserScore>();
    private List<UserScore> sortedList = new List<UserScore>();


    void Awake()
    {
        //Singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Config Firabase Instance
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pruebasapirest-13c28.firebaseio.com/");
        //Catch reference
        dataReference = FirebaseDatabase.DefaultInstance.RootReference;

        //Fill array
        StartCoroutine(GetAllScoresUsersDesktop());
    }

    public List<UserScore> GetListUsers()
    {
        return GetFirstTenUsers(usersList);
    }

    public void WriteScoreInBBDD(string user, int score, string time)
    {
        List<UserScore> tenBestScores = GetListUsers();

        try
        {
            if (tenBestScores.Count != 0)
            {
                //Write Score, only one time, be carefull with the writeBD variable
                if (tenBestScores.Count < 10)
                {
                    PostUserScore(new UserScore(user, score, time));
                    GameManager.Instance.IsNewScore = true;
                }
                else if (tenBestScores[tenBestScores.Count - 1].score <= score)
                {
                    if (tenBestScores[tenBestScores.Count - 1].score < score)
                    {
                        PostUserScore(new UserScore(user, score, time));
                        GameManager.Instance.IsNewScore = true;
                    }
                    else
                    {
                        //Check is which user have the best time
                        int theBest = CheckTheBest(tenBestScores[tenBestScores.Count - 1].score, score, tenBestScores[tenBestScores.Count - 1].time, time.ToString());
                        if (theBest == 2) 
                        { 
                            PostUserScore(new UserScore(user, score, time));
                            GameManager.Instance.IsNewScore = true;
                        }
                    }
                }
            }
            else
            {
                PostUserScore(new UserScore(user, score, time));
                GameManager.Instance.IsNewScore = true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("DataBase don't run currently" + ex.Message);
        }
    }

    //1 best score is score1, 2 best score is score2
    private int CheckTheBest(int score1, int score2, string time1, string time2) 
    {
        int valueWinner = -2;
        if (score1 > score2) valueWinner = 1;
        else if (score2 > score1) valueWinner = 2;
        else if (score2 == score1) { 
            int winner = CheckTime(time1, time2);
            if (winner == 1) valueWinner = 1;
            else if (winner == 2 || winner == -1) valueWinner = 2; 
        }

        return valueWinner;
    }

    //Method to check how user have the best time
    //-1 same time, 1 time1 is the best, 2 time2 is the best
    private int CheckTime(string time1, string time2)
    {
        int value = -2;

        string [] firstTime = time1.Split(':');
        string[] secondtTime = time2.Split(':');

        //Check minutes
        if (Convert.ToInt32(firstTime[0]) < Convert.ToInt32(secondtTime[0]))
            value = 1; 
        else if (Convert.ToInt32(firstTime[0]) > Convert.ToInt32(secondtTime[0]))
            value = 2; 
        //check seconds if both are in the same minute
        else if (Convert.ToInt32(firstTime[0]) == Convert.ToInt32(secondtTime[0]))
        {
            if (Convert.ToInt32(firstTime[1]) < Convert.ToInt32(secondtTime[1]))
                value = 1;
            else if (Convert.ToInt32(firstTime[1]) > Convert.ToInt32(secondtTime[1]))
                value = 2;
            else
                value = -1;
        }
        return value; 
    }

    public void WriteNewUser(string user) => PostUser(new User(user));

    public void UpdateListLaderBoardBackground() => StartCoroutine(GetAllScoresUsersDesktop());  //StartCoroutine(GetFirstTenUsersMobile());

    //Only use this when export to Mobile 
    // in other way use GetFirstTenUsersDesktop(); 
    private IEnumerator GetFirstTenUsersMobile()
    {
        usersList.Clear(); 

        //Instance to get users
        instance = FirebaseDatabase.DefaultInstance;

        //Get References to add users
        dataReference.Child("UsersScore");

        //Query 10 people whit the better score
        instance.GetReference("UsersScore").OrderByChild("score").LimitToLast(10).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted) Debug.LogError("Error to get users");
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    usersList.Add(new UserScore(user.Child("name").Value.ToString(), Convert.ToInt32(user.Child("score").Value), user.Child("time").Value.ToString()));
                }
                finish = true;
            }
        });
        yield return null;
    }

    private IEnumerator GetAllScoresUsersDesktop() 
    {
        usersList.Clear();

        string uri = $"https://pruebasapirest-13c28.firebaseio.com/UsersScore.json?orderBy=\"score\"";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                //Split all string to individual strings to get user and score. 
                string[] spliJson = webRequest.downloadHandler.text.Split('{', '}');

                foreach (string s in spliJson)
                {
                    
                    if (s.StartsWith("\"name\""))
                    {
                        string[] userName = s.Split(',');
                        string name = "";
                        string score = "";
                        string time = "";

                        foreach (string u in userName)
                        {
                            if (u.StartsWith("\"name\""))
                            {
                                char[] user = new char[16];
                                u.CopyTo(8, user, 0, u.Length - 9);
                                name = new string(user);
                            }
                            else if (u.StartsWith("\"score\""))
                            {
                                char[] score2 = new char[16];
                                u.CopyTo(8, score2, 0, u.Length - 8);
                                score = new string(score2);
                            }
                            else if (u.StartsWith("\"time\""))
                            {
                                char[] time2 = new char[5];
                                u.CopyTo(8, time2, 0, u.Length - 9);
                                time = new string(time2);
                            }
                        }
                        usersList.Add(new UserScore(name, Convert.ToInt32(score), time));
                    }
                }
            }
            sortedList = GetFirstTenUsers(usersList);
            finish = true;
        }
    }


    //Sort list comparing time of the final users if they have te same score
    private List<UserScore> GetFirstTenUsers(List<UserScore> users)
    {
        //Sort initial list
        List<UserScore> sortedList = users.OrderByDescending(o => o.score).ToList<UserScore>();
        List<UserScore> scoresRepeat = new List<UserScore>();

        UserScore userTen;

        if (users.Count > 10) 
        {
            //user nº 10 to delete rest of the users in the original list
            userTen = sortedList[9];

            //Get all repeat users with the same score of the user nº 10
            scoresRepeat = CheckMoreScoresInTheList(userTen.score, users);

            //Delete all UsersScores with the same score and minus score
            sortedList.RemoveAll(delegate (UserScore us)
            {
                return us.score <= userTen.score;
            });


            //Bubble Sort using CheckTiem to sort the secondary list
            for (int i = 0; i < scoresRepeat.Count; i++ ) 
            {
                for (int a = 0; a < scoresRepeat.Count - 1; a++)
                {
                    float bestTime = CheckTime(scoresRepeat[a].time, scoresRepeat[a+1].time);
                    if (bestTime == 2 || bestTime == -1)
                    {
                        UserScore temp = scoresRepeat[a];
                        scoresRepeat[a] = scoresRepeat[a + 1];
                        scoresRepeat[a + 1] = temp;
                    }
                }
            }
        }

        //Add secondary list sorted in the original list
        foreach (UserScore us in scoresRepeat) 
        {
            sortedList.Add(us);
        }
        
        //Take only ten users of the orginal list
        List<UserScore> finalTenBestUsers = sortedList.Take(10).ToList<UserScore>();

        return finalTenBestUsers;
    }

    private List<UserScore> CheckMoreScoresInTheList(int score, List<UserScore> users)
    {
        List<UserScore> listWithTheSameScore = users.FindAll
            (
                delegate (UserScore us)
                {
                    return us.score == score;
                }
            );
        return listWithTheSameScore; 
    }

    //Only use this when export to Mobile 
    // in other way use GetFirstTenUsersDesktop(); 
    private IEnumerator writeNewScore(string name, int score, string time)
    {
        string key = dataReference.Child("UsersScore").Push().Key;
        UserScore user = new UserScore(name, score,time);
        Dictionary<string, object> entryValues = user.ToDictionary();

        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/UsersScore/" + key] = entryValues;

        dataReference.UpdateChildrenAsync(childUpdates);

        yield return null;
    }


    private void PostUserScore(UserScore user)
    {
        RestClient.Post<UserScore>($"https://pruebasapirest-13c28.firebaseio.com/UsersScore.json", user);
    }

    private void PostUser(User user)
    {
        RestClient.Post<User>($"https://pruebasapirest-13c28.firebaseio.com/NewUsers.json", user);
    }
}