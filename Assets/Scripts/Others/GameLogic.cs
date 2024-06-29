using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject RedDotPrefab;
    GameObject crosshair;


    // Start is called before the first frame update
    void Start()
    {
        crosshair = Instantiate(RedDotPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshair.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }


}
