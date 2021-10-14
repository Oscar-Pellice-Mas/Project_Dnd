using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        classes = new Dictionary<string, int>();
    }


    // -------------------- ABILITY SCORES --------------------
    #region abilityScores
    private class AbilityScore
    {
        private int score;
        private bool savingProficiency;

        public AbilityScore(int score)
        {
            this.score = score;
        }

        public int GetScore()
        {
            return score;
        }

        public bool GetSavingProf()
        {
            return savingProficiency;
        }

        public void SetScore(int score)
        {
            this.score = score;
        }

        public void SetProficiency(bool prof)
        {
            this.savingProficiency = prof;
        }
    }

    private Dictionary<string, AbilityScore> abilityScores;

    private List<string> senses;

    public int GetAbilityScore(string scoreName)
    {
        AbilityScore ability = this.abilityScores[scoreName];
        return ability.GetScore();
    }

    public int GetAbilityModifier(string scoreName)
    {
        int score = GetAbilityScore(scoreName);
        return (score - 10) / 2;
    }

    public int GetSavingThrow(string savingName)
    {
        AbilityScore abilityScore = abilityScores[savingName];
        int value = (abilityScore.GetScore() - 10) / 2;
        if (abilityScore.GetSavingProf())
        {
            value += proficiencyBonus;
        }
        return value;
    }

    public int GetSense(string senseName)
    {
        return GetSkillModifier(senseName) + 10;
    }

    public List<string> GetSenses()
    {
        return senses;
    }

    public void AddSense(string sense)
    {
        if (!senses.Contains(sense))
        {
            senses.Add(sense);
        }
    }

    public void RemoveSense(string sense)
    {
        senses.Remove(sense);
    }

    // Ability Score info getter on DB
    #endregion abilityScores

    // -------------------- SKILLS --------------------
    #region skills
    private class Skill
    {
        private readonly PlayerCharacter identifier;
        private int modifier;
        private bool proficency = false;
        private readonly string ability;

        public Skill(PlayerCharacter id, int modifier, string ability)
        {
            this.modifier = modifier;
            this.ability = ability;
            this.identifier = id;
        }

        public int GetModifier()
        {
            return modifier;
        }

        public bool GetProficiency()
        {
            return proficency;
        }

        public string GetAbility()
        {
            return ability;
        }

        public void SetProfiency(bool proficency)
        {
            this.proficency = proficency;
        }

        public void SetModifier(int modifier)
        {
            this.modifier = modifier;
        }

        internal void Calculate()
        {
            modifier = identifier.GetAbilityModifier(ability);
            if (proficency)
            {
                modifier += identifier.GetProficiencyBonus();
            }
        }
    }

    private Dictionary<string, Skill> skills;

    public int GetSkillModifier(string name)
    {
        Skill skill = this.skills[name];
        return skill.GetModifier();
    }

    public void SetProficiencies(List<string> proficiencies)
    {
        foreach (string name in proficiencies)
        {
            skills[name].SetProfiency(true);
        }
    }

    public void CalculateSkills()
    {
        foreach (Skill skill in skills.Values)
        {
            skill.Calculate();
        }
    }

    // Skill info getter on DB

    #endregion skills

    // -------------------- PROFICIENCY --------------------
    #region others
    private int proficiencyBonus;

    public int GetProficiencyBonus()
    {
        return proficiencyBonus;
    }
    // -------------------- ALIGNMENT --------------------

    private string alignment;

    public string GetAlignment()
    {
        return alignment;
    }

    // Alignment info getter on DB

    // -------------------- BACKGROUND --------------------

    private string background;
    private string personalTraits;
    private string ideals;
    private string bonds;
    private string flaws;

    public string GetBackground()
    {
        return background;
    }

    public void SetBackground(string background)
    {
        this.background = background;
    }

    public string GetPersonalTraits()
    {
        return personalTraits;
    }

    public void SetPersonalTraits(string personalTraits)
    {
        this.personalTraits = personalTraits;
    }

    public string GetIdeals()
    {
        return ideals;
    }

    public void SetIdeals(string ideals)
    {
        this.ideals = ideals;
    }

    public string GetBonds()
    {
        return bonds;
    }

    public void SetBonds(string bonds)
    {
        this.bonds = bonds;
    }

    public string GetFlaws()
    {
        return flaws;
    }

    public void SetFlaws(string flaws)
    {
        this.flaws = flaws;
    }

    // Background info and options from DB

    // ---------------- Proficiencies ----------------------
    private List<string> proficiencyArmor;
    private List<string> proficiencyWeapons;
    private List<string> proficiencyTools;
    private List<string> proficiencyLanguages;

    public List<string> GetProficiencyArmor()
    {
        return proficiencyArmor;
    }

    public void AddProficiencyArmor(string prof)
    {
        if (!proficiencyArmor.Contains(prof))
        {
            proficiencyArmor.Add(prof);
        }
    }

    public void RemoveProficiencyArmor(string prof)
    {
        proficiencyArmor.Remove(prof);
    }

    public List<string> GetProficiencyWeapons()
    {
        return proficiencyWeapons;
    }

    public void AddProficiencyWeapons(string prof)
    {
        if (!proficiencyWeapons.Contains(prof))
        {
            proficiencyWeapons.Add(prof);
        }
    }

    public void RemoveProficiencyWeapons(string prof)
    {
        proficiencyWeapons.Remove(prof);
    }

    public List<string> GetProficiencyTools()
    {
        return proficiencyTools;
    }

    public void AddProficiencyTools(string prof)
    {
        if (!proficiencyTools.Contains(prof))
        {
            proficiencyTools.Add(prof);
        }
    }

    public void RemoveProficiencyTools(string prof)
    {
        proficiencyTools.Remove(prof);
    }

    public List<string> GetProficiencyLanguages()
    {
        return proficiencyLanguages;
    }

    public void AddProficiencyLanguages(string prof)
    {
        if (!proficiencyLanguages.Contains(prof))
        {
            proficiencyLanguages.Add(prof);
        }
    }

    public void RemoveProficiencyLanguages(string prof)
    {
        proficiencyLanguages.Remove(prof);
    }
    #endregion others

    // ------------- HP and Basic Values --------------------
    #region basic
    private int initiative;
    private int armorClass;
    private int hitPoints;
    private int maxPoints;
    private int tempPoints;

    public int GetInitiative()
    {
        return initiative;
    }

    public void SetInitiative(int value)
    {
        initiative = value;
    }

    public int GetArmorClass()
    {
        return armorClass;
    }

    public void SetArmorClass(int value)
    {
        armorClass = value;
    }

    public int GetHitpoints()
    {
        return hitPoints;
    }

    public void SetHitpoints(int value)
    {
        hitPoints = value;
    }

    public int GetMaxHitpoints()
    {
        return maxPoints;
    }

    public void SetMaxHitpoints(int value)
    {
        maxPoints = value;
    }

    public int GetTempHitpoints()
    {
        return tempPoints;
    }

    public void SetTempHitpoints(int value)
    {
        tempPoints = value;
    }
    #endregion basic

    // ------------ RESISTANCES ----------------
    #region resistances
    private List<string> resistances;
    private List<string> vulnerabilities;
    private List<string> immunities;
    private List<string> conditions;

    public void AddResistance(string resistance)
    {
        if (!resistances.Contains(resistance))
        {
            resistances.Add(resistance);
        }
    }

    public void AddVulnerability(string vulnerability)
    {
        if (!vulnerabilities.Contains(vulnerability))
        {
            vulnerabilities.Add(vulnerability);
        }
    }

    public void AddCondition(string condition)
    {
        if (!conditions.Contains(condition))
        {
            conditions.Add(condition);
        }
    }

    public void AddImmunity(string immunity)
    {
        if (!immunities.Contains(immunity))
        {
            immunities.Add(immunity);
        }
    }

    public void RemoveResistance(string resistance)
    {
        resistances.Remove(resistance);
    }

    public void RemoveVulnerability(string vulnerability)
    {
        vulnerabilities.Remove(vulnerability);
    }

    public void RemoveCondition(string condition)
    {
        conditions.Remove(condition);
    }

    public void RemoveImmunity(string immunity)
    {
        immunities.Remove(immunity);
    }

    public List<string> GetResistances()
    {
        return resistances;
    }

    public List<string> GetVulnerabilities()
    {
        return vulnerabilities;
    }

    public List<string> GetConditions()
    {
        return conditions;
    }

    public List<string> GetImmunities()
    {
        return immunities;
    }

    // -------- Speed ----------

    private Dictionary<string, int> speeds;

    public void SetSpeed(string name, int value)
    {
        speeds[name] = value;
    }

    public int GetSpeed(string name)
    {
        return speeds[name];
    }
    #endregion resistances

    // ---------- Class --------------
    #region class
    private Dictionary<string, int> classes;

    public void SetClass(string name, int value)
    {

        classes[name] = value;
    }

    public void AddClass(string name)
    {
        classes.Add(name, 0);
    }

    public void RemoveClass(string name)
    {
        classes.Remove(name);
    }

    public string[] GetClasses()
    {
        string[] returnable = new string[classes.Keys.Count];
        classes.Keys.CopyTo(returnable, 0);
        return returnable;
    }

    public int GetClassLevel(string name)
    {
        return classes[name];
    }
    #endregion class

    // ---------- Race ------------------
    #region race
    private string race;

    public string GetRace()
    {
        return race;
    }

    public void SetRace(string name)
    {
        race = name;
    }
    #endregion race

    
}
