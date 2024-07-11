using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public GameObject Skill;
    public GameObject Target;
    public KeyCode inputSkill = KeyCode.LeftShift;
    public float SkillDuration = 5f;

    //Skill
    GameObject _skill;
    float _skillDuration;
    Player player;
    bool _skillActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _skillDuration = SkillDuration;
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputSkill) && _skillDuration > 0 && !_skillActive)
        {
            _skillActive = true;
            Target = player.GetPlayer();
            if (Target != null)
            {
                _skill = Instantiate(Skill, Target.transform.position, Quaternion.identity);
            }
        }

        if (_skill != null)
        {
            // Follow Target
            _skill.transform.position = Target.transform.position;

            _skillDuration -= Time.deltaTime;
            if (_skillDuration <= 0)
            {
                Destroy(_skill);
                _skillDuration = SkillDuration;
                _skillActive = false;
            }

        }
    }
}
