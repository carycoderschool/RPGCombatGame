﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemAction : ScriptableObject
{
    public abstract void Act(baseStats attacker);
}