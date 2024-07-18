using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Parameter;

public class SkillController : MonoBehaviour
{
    static GameParameterSkillController _param = new();

    public GameObject Skill;
    public GameObject Target;
    public KeyCode inputSkill = _param.INPUT_SKILL;
    public float SkillDuration = _param.SKILL_DURATION;
    public float TimeBetweenSkill = _param.TIME_BETWEEN_SKILL;
    //Skill
    GameObject _skill;
    float _skillDuration;
    float _timeBetweenSkill;
    Player player;
    bool _skillActive = false;

    // Animation
    Animator animator;
    int skillLayerEnter;


    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponentInChildren<Animator>();
        this.skillLayerEnter = this.animator.GetLayerIndex("SkillLayer");

        _skillDuration = SkillDuration;
        _timeBetweenSkill = TimeBetweenSkill;
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeBetweenSkill -= Time.deltaTime;

        if (Input.GetKeyDown(inputSkill) && _skillDuration > 0 && !_skillActive && _timeBetweenSkill <= 0)
        {
            if (Skill != null)
            {
                _skillActive = true;
                Target = player.GetPlayer();
                if (Target != null)
                {
                    _skill = Instantiate(Skill, Target.transform.position, Quaternion.identity);
                }

            }
            else
            {
                // Set invincible for player
                StartCoroutine(SkillInvincible());
            }
        }
        // Skill for player ninja follow
        if (_skill != null)
        {
            // Follow Target
            _skill.transform.position = Target.transform.position;
            _skillDuration -= Time.deltaTime;
            if (_skillDuration <= 0)
            {
                Destroy(_skill);
                _skillDuration = SkillDuration;
                _timeBetweenSkill = TimeBetweenSkill;
                _skillActive = false;
            }
        }
    }

    private IEnumerator SkillInvincible()
    {
        gameObject.GetComponent<MovementController>().SetInvincible(true);
        animator.SetLayerWeight(skillLayerEnter, 1);
        yield return new WaitForSeconds(SkillDuration);

        gameObject.GetComponent<MovementController>().SetInvincible(false);
        animator.SetLayerWeight(skillLayerEnter, 0);
        _timeBetweenSkill = TimeBetweenSkill;
    }
}
