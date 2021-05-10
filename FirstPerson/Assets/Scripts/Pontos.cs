using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontos : MonoBehaviour
{
    Text textComp;

    void Start()
    {
        textComp = GetComponent<Text>();
    }
    
    void Update()
    {
        textComp.text = $"{PlayerController.pontos}/8 BAÚS";
    }
}