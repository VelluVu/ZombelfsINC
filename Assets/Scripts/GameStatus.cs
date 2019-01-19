using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStatus : MonoBehaviour {

    public static string currentLevel;
    public static GameStatus status;
    public static bool theGameIsOn;
    public static int gore = 3;
    public static bool Level1;
    public static bool Level2;
    public static bool Level3;
    public static bool Level4;
    public static bool Level5;
    public static bool inLevel;
    public static bool winStatus;
    public static bool gameCompleted;
    public static bool gameBegin;

    void Awake () {

        if (Level1 == true && Level2 == true && Level3
             == true && Level4 == true && Level5 == true)
        {
            gameCompleted = true;
        } else
        {
            gameCompleted = false;
        }

        theGameIsOn = true;

        if (status == null)
        {
            DontDestroyOnLoad(gameObject);
            status = this;
        } else if (status != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Debug.Log("CurrentLevel: " + currentLevel);

        if(Input.GetKey(KeyCode.Escape))
        {
            if (inLevel)
            {            
                ScoreTable.SaveScores();
                Save();
            }
            SceneManager.LoadScene("MainMenu");
        }

        if(!theGameIsOn)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }
    
    public static void Save()
    {
        CharacterStats charSt = FindObjectOfType<CharacterStats>();
        Debug.Log("Save");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + ScoreTable.currentPlayer + "savegame.dat");
        CharacterData data = new CharacterData(charSt);
        //TODO : Tallenna arvot
        bf.Serialize(file, data);
        file.Close();

    }

    public static void Load(string playerName)
    {
        string path = playerName + "/savegame.dat";
        if(File.Exists(Application.persistentDataPath + path))
        {
            
            CharacterStats charSt = FindObjectOfType<CharacterStats>();
            
            Debug.Log("Load");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + playerName + "savegame.dat", FileMode.Open);
            CharacterData data = (CharacterData)bf.Deserialize(file);
            file.Close();
            //TODO : Lataa arvot 
            ScoreTable.currentPlayer = data.name;
            ScoreTable.SetScore(ScoreTable.currentPlayer, "score", data.score);

            CharacterStats.currentCharacterLevel = data.cCharLevel;          
            CharacterStats.levelPojo = data.lPojo;
            CharacterStats.strength = data.str;
            CharacterStats.dexterity = data.dex;
            CharacterStats.vitality = data.vit;
            CharacterStats.energy = data.ene;  
            charSt.maxHealth = data.mHealth;
            charSt.maxMana = data.mMana;
            charSt.replenishH = data.rHealth;
            charSt.replenishM = data.rMana;
            charSt.moveSpeed = data.fMoveSpeed;
            charSt.rotationSpeed = data.rotSpeed;
            charSt.BackwardsMoveSpeed = data.bMoveSpeed;
            charSt.jumpForce = data.jForce;
            Level1 = data._Level1;
            Level2 = data._Level2;
            Level3 = data._Level3;
            Level4 = data._Level4;
            Level5 = data._Level5;
            WeaponSwitch wep = FindObjectOfType<WeaponSwitch>();
            wep.SwitchToThisWep(0);
            FindObjectOfType<Axe>().LoadAxeStats();
            wep.SwitchToThisWep(1);
            FindObjectOfType<Sword>().LoadSwordStats();
            wep.SwitchToThisWep(2);
            FindObjectOfType<Spell>().LoadSpellStats();
        }
        
    }
}

[System.Serializable]
public class CharacterData
{
    
    public string name;
    public int score;
    public float mHealth;
    public float fMoveSpeed;
    public float rotSpeed;
    public float mMana;
    public float rMana;
    public float rHealth;
    public float bMoveSpeed;
    public float jForce;
    public int str;
    public int dex;
    public int vit;
    public int ene;
    public int lPojo;
    public int cCharXP;
    public int cCharLevel;
    public bool _Level1;
    public bool _Level2;
    public bool _Level3;
    public bool _Level4;
    public bool _Level5;

    //konstruktori ettei tarvi erikseen asettaa arvoja...
    public CharacterData(CharacterStats stats)
    {
        name = ScoreTable.currentPlayer;
        score = ScoreTable.GetScore(ScoreTable.currentPlayer, "score");
        cCharLevel = CharacterStats.currentCharacterLevel;
        cCharXP = stats.currentCharacterXP;
        lPojo = CharacterStats.levelPojo + CharacterStats.currentCharacterLevel * 2 - 2;
        str = CharacterStats.strength;
        dex = CharacterStats.dexterity;
        vit = CharacterStats.vitality;
        ene = CharacterStats.energy;      
        mHealth = stats.maxHealth;
        mMana = stats.maxMana;
        rHealth = stats.replenishH;
        rMana = stats.replenishM;
        fMoveSpeed = stats.moveSpeed;
        rotSpeed = stats.rotationSpeed;
        bMoveSpeed = stats.BackwardsMoveSpeed;
        jForce = stats.jumpForce;
        _Level1 = GameStatus.Level1;
        _Level2 = GameStatus.Level2;
        _Level3 = GameStatus.Level3;
        _Level4 = GameStatus.Level4;
        _Level5 = GameStatus.Level5;

    }
}