using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterTable", menuName = "CharacterTables")]
public class CharacterTables : ScriptableObject
{
    public Sprite[] spritesNormal;
    public GameObject spritesSlightlyDamagedTurso;
    public GameObject spritesSlightlyDamagedHead;
    public GameObject spritesHeavyDamagedTurso;
    public GameObject spritesHeavyDamagedHead;
    public GameObject Flyinghead;
}
