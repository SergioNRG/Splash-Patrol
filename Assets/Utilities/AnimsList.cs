using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimsList : ScriptableObject
{
    public List<Anim> Anims;

    // public string IDLE;
    // public string ATTACK;

    [Serializable]
    public class Anim
    {
        public string AnimKey;
        public string AnimName;
    }
}
