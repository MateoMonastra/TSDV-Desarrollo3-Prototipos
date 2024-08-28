using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ChallengeManager _challengeManager;
    [SerializeField] private PlayerInterface _playerInterface = null;
    private void Awake()
    {
        _challengeManager = new ChallengeManager();   
    }

    
    
}
