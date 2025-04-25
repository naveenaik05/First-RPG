using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill info")]

    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;

    private Vector2 finalDir;

    [Header("Aim dots")]

    [SerializeField] private int numberOfDots;
    [SerializeField] private float spcaeBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dots;

    protected override void Update()
    {
        finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
    }

    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();
        newSwordScript.SetupSword(finalDir, swordGravity);
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;
        return direction;
    }


}
