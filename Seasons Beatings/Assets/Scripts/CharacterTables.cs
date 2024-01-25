using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterTable", menuName = "CharacterTables")]
public class CharacterTables : ScriptableObject
{
    public Sprite[] spritesNormal;
    public Sprite[] spritesSlightlyDamaged;
    public Sprite[] spritesHeavyDamaged;
    public ParticleSystem particleEffect;
    public GameObject Headless;
}
