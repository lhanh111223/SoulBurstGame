using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
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

        Health = 100;
        Mana = 200;
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
