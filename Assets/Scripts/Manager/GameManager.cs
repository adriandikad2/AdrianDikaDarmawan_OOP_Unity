using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }

        Instance = this;

        LevelManager = GetComponentInChildren<LevelManager>();

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Camera"));
    }
    
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}