using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataObject", menuName = "ScriptableObjects/CharacterData")]

public class CharacterData : ScriptableObject
{
    [Header("Character Information")]
    public int characterID;
    public string characterName;
    public string characterTitle;
    
    [Header("Character Basic Sprites")]
    public Sprite BaseSprite;
    public Sprite NeutralSprite;
    public Sprite HappySprite;
    public Sprite SadSprite;
    public Sprite AngrySprite;
    public Sprite SurprisedSprite;
    public Sprite ThinkingSprite;
    public Sprite ConfusedSprite;

    [Header("Character Unique Sprites")]
    public List<CustomExpression> characterUniqueSprites;



    [System.Serializable]
    public class CustomExpression
    {
        public string expressionName;
        public Sprite expressionSprite;
    }
}
