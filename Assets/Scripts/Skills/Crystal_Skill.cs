using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private float crystalDuration;
    private GameObject currentCrystal;

    [SerializeField] private float growSpeed;

    [SerializeField] private bool cloneInteadOfCrystal;

    [Header("Explosive crystal")]
    [SerializeField] private bool canExplode;


    [Header("Moving crystal")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;


    [Header("Multi stacking crystal")]
    [SerializeField] private bool canUseMultiStacksCrystal;
    [SerializeField] private int amountOfStacks;
    [SerializeField] private float multiStackCoolDown;
    [SerializeField] private float useTimeWindow;
    [SerializeField] private List<GameObject> crystalLeft = new List<GameObject>();

    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrystal())
            return;

        if (currentCrystal == null)
        {
            CreateCrystal();
        }
        else
        {
            if (canMoveToEnemy)
                return;

            Vector2 playerPos = player.transform.position;
            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos;

            if (cloneInteadOfCrystal)
            {
                SkillManager.instance.clone.CreateClone(currentCrystal.transform, Vector3.zero);
                Destroy(currentCrystal);
            }
            else
            {
                currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
            }

        }
    }

    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);

        Crystal_Skill_Controller currentCrystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();
        currentCrystalScript.SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, growSpeed, FindClosestEnemy(currentCrystal.transform), player);
    }

    public void CurrentCrystalChooseRandomEnemy()
    {
        currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();
    }

    private bool CanUseMultiCrystal()
    {
        if (canUseMultiStacksCrystal)
        {
            if (crystalLeft.Count > 0)
            {
                if (crystalLeft.Count == amountOfStacks)
                    Invoke("ResetAbility", useTimeWindow);

                coolDown = 0;
                GameObject crystalToSpawn = crystalLeft[crystalLeft.Count - 1];

                GameObject newCrystal = Instantiate(crystalToSpawn, player.transform.position, Quaternion.identity);

                crystalLeft.Remove(crystalToSpawn);

                newCrystal.GetComponent<Crystal_Skill_Controller>()
                .SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, growSpeed, FindClosestEnemy(newCrystal.transform),player);

                if (crystalLeft.Count <= 0)
                {
                    coolDown = multiStackCoolDown;
                    RefilCrystal();
                }
                return true;
            }

        }

        return false;
    }

    private void RefilCrystal()
    {
        int amuntToAdd = amountOfStacks - crystalLeft.Count;

        for (int i = 0; i < amuntToAdd; i++)
        {
            crystalLeft.Add(crystalPrefab);
        }
    }

    private void ResetAbility()
    {
        if (coolDownTimer > 0)
            return;

        coolDown = multiStackCoolDown;
        RefilCrystal();
    }
}
