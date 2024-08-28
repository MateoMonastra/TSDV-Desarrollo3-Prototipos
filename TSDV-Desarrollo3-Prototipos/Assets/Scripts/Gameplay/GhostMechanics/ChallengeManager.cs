using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager
{
    private List<Challenge> _challengeList;
    public ChallengeManager()
    {
        _challengeList = new List<Challenge>();
        _challengeList.Add(new StruggleChallenge(8,20,0.4f));
        _challengeList.Add(new StruggleChallenge());
    }
    
    public Challenge GetRandomChallenge()
    {
        return _challengeList[Random.Range(0,_challengeList.Count)];
    }

    public Challenge GetChallenge(int index)
    { 
        return _challengeList[index]; 
    }
}
