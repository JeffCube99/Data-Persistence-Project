using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformationManager : MonoBehaviour
{
    public static PlayerInformationManager Instance;

    private string _name;
    public string name
    {
        get
        {
            return _name;
        }

        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _name = value;
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
