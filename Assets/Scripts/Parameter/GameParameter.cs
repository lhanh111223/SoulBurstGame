using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Parameter
{
    public class GameParameter
    {
    }

    // Folder: Player
    public class GameParameterMovementController
    {
        // Movement
        public KeyCode INPUT_UP = KeyCode.W;
        public KeyCode INPUT_DOWN = KeyCode.S;
        public KeyCode INPUT_LEFT = KeyCode.A;
        public KeyCode INPUT_RIGHT = KeyCode.D;
        public float SPEED = 5f;
        //Dash
        public KeyCode INPUT_DASH = KeyCode.Space;
        public float DELAY_DASH_SECOND = 0.09f;
        public float DASH_BOOST = 5f;
        public float DASH_TIME = 0.3f;
        public float TIME_BETWEEN_DASH = 5f;
        // Attack
        public MouseButton INPUT_ATTACK = MouseButton.Right;

    }

    public class GameParameterPlayer
    {
        public int MAX_HEALTH = 100;
        public int MAX_MANA = 200;
    }

    public class GameParameterPlayerHealthBar
    {
        public int MAX_HEALTH = 100;
    }

    public class GameParameterPlayerManaBar
    {
        public int MAX_MANA = 200;
    }

    public class  GameParameterSkillController
    {
        public KeyCode INPUT_SKILL = KeyCode.LeftShift;
        public float SKILL_DURATION = 5f;
        public float TIME_BETWEEN_SKILL = 30f;
    }

    // Folder: Weapon
    public class GameParameterBulletController
    {
        
    }

    public class GameParameterWeaponController
    {
        public string BULLET_TYPE_WEAPON1_BULLET = "Bullet";
        public string BULLET_TYPE_WEAPON2_LAZER = "Lazer";
        public string BULLET_TYPE_WEAPON3_AKA = "Bullet3Aka";
        public string BULLET_TYPE_WEAPON4_SHOTGUN = "Bullet4Shotgun";
        // Need Update
    }

    public class GameParameterWeaponPickup
    {
        public string WEAPON_TYPE_NORMAL_GUN = "Weapon1";
        public string WEAPON_TYPE_LAZER = "Weapon2";
        public string WEAPON_TYPE_AKA = "Weapon3";
        public string WEAPON_TYPE_SHOTGUN = "Weapon4";
    }
}
