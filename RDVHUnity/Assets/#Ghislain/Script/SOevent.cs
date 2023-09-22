using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "Event", menuName ="Scriptable object/Event")]
public class SOevent : ScriptableObject
{
    [SerializeField] string _titre;
    [SerializeField] string _texte;
    [SerializeField] Sprite _image;

    public string GetTitle()
    {
        return (_titre);
    }

    public string GetTexte()
    {
        return (_texte);
    }

    public Sprite GetImage()
    {
        return (_image);
    }
}
