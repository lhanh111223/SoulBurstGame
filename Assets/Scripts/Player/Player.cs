using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Parameter;

public class Player : MonoBehaviour
{
    static GameParameterPlayer _param = new();

    public CharacterDatabase characterDB;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;

    // Mana - HP
    public int Health;
    public int Mana;

    public CinemachineVirtualCamera virtualCamera;

    GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        //Instantiate(characterDB.GetCharacter(selectedOption).playerPrefab, transform.position, Quaternion.identity);
        Health = _param.MAX_HEALTH;
        Mana = _param.MAX_MANA;
        _player = Instantiate(characterDB.GetCharacter(selectedOption).playerPrefab, transform.position, Quaternion.identity);
        virtualCamera.Follow = _player.transform;

    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    public void DecreaseMana(int value)
    {
        Mana -= value;
    }

    // Get player
    public GameObject GetPlayer()
    {
        return _player;
    }

}
