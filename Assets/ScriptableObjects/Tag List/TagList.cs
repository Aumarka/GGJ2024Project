using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tag List", menuName = "ScriptableObjects/Tag List")]
public class TagList : ScriptableObject
{
    public string[] tagList;
}
