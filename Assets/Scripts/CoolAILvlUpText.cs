using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoolAILvlUpText : MonoBehaviour
{
    public static CoolAILvlUpText instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            instance = gameObject.GetComponent<CoolAILvlUpText>();
        }
    }

    public void CreateNew()
    {
        GameObject gameOBJ = Instantiate(gameObject);
        gameOBJ.GetComponent<MeshRenderer>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<MeshRenderer>().enabled)
            return;
        transform.Translate(0f, 0f, -Time.deltaTime);
        Color color = GetComponent<TextMeshPro>().color;
        color.a -= Time.deltaTime;
        GetComponent<TextMeshPro>().color = color;
        if (color.a <= 0.01)
        {
            Destroy(gameObject);
        }

    }
}
