using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    private Player _player;
    
    public static CharacterManager Instance
    {
        get
        {
            return _instance;
        }
    }
    public Player Player
    {
        get { return _player;}
        set { _player = value; }
    }
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
