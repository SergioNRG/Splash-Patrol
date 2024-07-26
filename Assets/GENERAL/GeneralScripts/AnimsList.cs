using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimsList : ScriptableObject
{
    public List<Anim> Anims;

    [Serializable]
    public class Anim
    {
        public string AnimKey;
        public string AnimName;
    }
}
