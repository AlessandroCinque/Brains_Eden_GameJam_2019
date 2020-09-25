using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tutorial : MonoBehaviour
{
    public float time = 0.0f;
    public List<GameObject> compImage = new List<GameObject>();
    public List<Image> image = new List<Image>();

    public List<GameObject> MeshComponent = new List<GameObject>();
    public List<TextMeshProUGUI> textMesh = new List<TextMeshProUGUI>();

    public List<GameObject> textComponent = new List<GameObject>();
    public List<Text> ourComponent = new List<Text>();
    

    // Use this for initialization
    void Start ()
    {
        compImage.Add(GameObject.Find("ButtonA"));
        compImage.Add(GameObject.Find("ButtonD"));
        compImage.Add(GameObject.Find("ButtonW"));
        compImage.Add(GameObject.Find("ButtonSpace"));

        //=========================
        MeshComponent.Add(GameObject.Find("MeshA"));
        MeshComponent.Add(GameObject.Find("MeshD"));
        MeshComponent.Add(GameObject.Find("MeshW"));
        MeshComponent.Add(GameObject.Find("MeshSpace"));

        //=========================
        textComponent.Add(GameObject.Find("TextA"));
        textComponent.Add(GameObject.Find("TextD"));
        textComponent.Add(GameObject.Find("TextW"));
        textComponent.Add(GameObject.Find("TextSpace"));

        for (int i = 0; i < 4; i++)
        {
            image.Add(compImage[i].GetComponent<Image>());
            textMesh.Add(MeshComponent[i].GetComponent<TextMeshProUGUI>());
            ourComponent.Add(textComponent[i].GetComponent<Text>());
        }
    }

    // Update is called once per frame
    void Update ()
    {
        time += Time.deltaTime;
        if (time >= 15.0f)
        {
            for (int i = 0; i < 4; i++)
            {
                image[i].enabled = false;
                textMesh[i].enabled = false;
                ourComponent[i].enabled = false;
            }
        }
    }
}
