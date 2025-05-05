using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{

    private Animator animator => GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();
    private Player player;

    private float crystalExitTimer;

    private bool canExplode;
    private bool canMove;
    private float moveSpeed;

    private float growSpeed;
    private bool canGrow;

    private Transform closestEnemy;
    [SerializeField] private LayerMask whatisEnemy;
    public void SetupCrystal(float _crystalDuration, bool _canExplode, bool _canMoveToEnemy, float _moveSpeed, float _growSpeed, Transform _closestEnemy, Player _player)
    {
        crystalExitTimer = _crystalDuration;
        canExplode = _canExplode;
        canMove = _canMoveToEnemy;
        moveSpeed = _moveSpeed;
        growSpeed = _growSpeed;
        closestEnemy = _closestEnemy;
        player = _player;
    }

    public void ChooseRandomEnemy()
    {
        float radius = SkillManager.instance.blackhole.GetBlackholeRadius();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius,whatisEnemy);
        if (colliders.Length > 0)
        {
            closestEnemy = colliders[Random.Range(0, colliders.Length)].transform;
        }
    }

    private void Update()
    {
        crystalExitTimer -= Time.deltaTime;

        if (crystalExitTimer < 0)
        {
            FinishCrystal();
        }

        if (canMove && closestEnemy != null) //Added extra constraint
        {
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, closestEnemy.position) < 1)
            {
                FinishCrystal();
                canMove = false;
            }
        }

        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), growSpeed * Time.deltaTime);
        }
    }

    private void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                player.stats.DoMagicalDamge(hit.GetComponent<CharacterStats>());
            }
        }
    }

    public void FinishCrystal()
    {
        if (canExplode)
        {
            canGrow = true;
            animator.SetTrigger("Explode");
        }
        else
            SelfDestroy();
    }

    public void SelfDestroy() => Destroy(gameObject);
}
