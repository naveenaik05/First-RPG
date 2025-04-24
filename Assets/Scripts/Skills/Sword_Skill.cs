using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill info")]

    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchDir;
    [SerializeField] private float swordGravity;
}
