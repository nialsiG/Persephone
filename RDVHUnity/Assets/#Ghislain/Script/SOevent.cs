using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "Event", menuName = "Scriptable object/Event")]
public class SOevent : ScriptableObject
{
    [Header("Description")]
    [SerializeField] string _titre;
    [SerializeField] string _texte;
    [SerializeField] Sprite _image;
    [SerializeField] SOSoundPool _sound;

    [Header("Choix 1")]
    [SerializeField] string _texteChoix1;
    [SerializeField] int _changeToMoney1;
    [SerializeField] int _changeToReputation1;

    [Header("Choix 2")]
    [SerializeField] string _texteChoix2;
    [SerializeField] int _changeToMoney2;
    [SerializeField] int _changeToReputation2;


    public string Title => _titre;
    public string Texte => _texte;
    public Sprite Image => _image;
    public SOSoundPool Sound => _sound;
    public string TexteChoix1 => _texteChoix1;
    public int ChangeToMoney1 => _changeToMoney1;
    public int ChangeToReputation1 => _changeToReputation1;    
    public string TexteChoix2 => _texteChoix2;
    public int ChangeToMoney2 => _changeToMoney2;
    public int ChangeToReputation2 => _changeToReputation2;

}