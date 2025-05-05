using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator;
    private Player player;
    [SerializeField] private float colorLoosingSpeed;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    
    private bool canDuplicateClone;
    private float chanceDuplicate;
    private Transform closestEnemy;
    private float facingDir = 1;

    private float cloneTimer;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (colorLoosingSpeed * Time.deltaTime));

            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }
    public void SetupClone(Transform _newPosition, float _cloneDuration, bool _canAttack, Vector3 _offset, Transform _closestEnemy, bool _canDuplicate,float _chanceDuplicate,Player _player)
    {
        if (_canAttack)
        {
            animator.SetInteger("AttackNumber", Random.Range(1,3));
        }
        transform.position = _newPosition.position + _offset;
        player = _player;
        cloneTimer = _cloneDuration;
        closestEnemy = _closestEnemy;

        canDuplicateClone = _canDuplicate;
        chanceDuplicate = _chanceDuplicate;
        FaceClosesTarget();
    }

    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                // hit.GetComponent<Enemy>().DamageEffect();
                player.stats.DoDamage(hit.GetComponent<CharacterStats>());

                if (canDuplicateClone)
                {
                    if (Random.Range(0, 100) < chanceDuplicate)
                        SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(.5f * facingDir, 0));
                }
            }
        }
    }

    private void FaceClosesTarget()
    {
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }

    }
}
