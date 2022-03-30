using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPotionFactory : ItemFactory
{
    public override ItemClass GetItemClass()
    {
        return new StrengthPotion();
    }
}