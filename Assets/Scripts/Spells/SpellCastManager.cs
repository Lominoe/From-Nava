using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellCastManager : MonoBehaviour
{
    //Responsible for spawning spells

    [Serializable]
    public struct EnumToSpell
    {
        public SpellType spellType;
        public Spell spell;
    }

    [SerializeField] private EnumToSpell[] enumToSpell;

    private Dictionary<SpellType, Spell> spellDict = new Dictionary<SpellType, Spell>();



    private void Awake() {
        foreach(EnumToSpell e in enumToSpell)
            spellDict[e.spellType] = e.spell;
    }

    private void OnEnable() {
        NotepadLogic.OnSpellCast += OnSpellCast;
    }

    private void OnDisable() {
        NotepadLogic.OnSpellCast -= OnSpellCast;
    }


    private void OnSpellCast(object sender, NotepadLogic.OnSpellCastArgs e)
    {
        if(e.spellType == SpellType.NONE)
        {
            return;
        }

        Spell spell = spellDict[e.spellType];
        
        Instantiate(spell, PlayerController.Instance.castPoint.position, Quaternion.identity);
        spell.CastSpell(PlayerController.Instance.FacingDir);
    }

}


public enum SpellType {
    FIREBALL,
    CHAIR,
    ICEBALL,
    NONE
}