using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject RedDotPrefab;
    GameObject crosshair;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        crosshair = Instantiate(RedDotPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshair.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
    public void loadSceneWinGame()
    {
        Debug.Log("Victory");
        SceneManager.LoadScene("WinGameScene");
    }

}
