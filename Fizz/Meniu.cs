using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using KappAIO_Reborn.Common.Databases.Spells;
using KappAIO_Reborn.Common.Utility;
using System.Collections.Generic;
using System.Linq;
using EloBuddy.SDK.Menu.Values;

namespace Fizz
{
    class Meniu
    {
        public static Menu Menu, MenuCombo, MenuHarass, MenuLane, MenuJungle, MenuMisc,MenuE, MenuDraw;
        public static bool evadeEnabled => MenuE.CheckBoxValue("enable");
        public static bool BlockExecute => MenuE.CheckBoxValue("executeBlock");
        public static bool BlockAA => MenuE.CheckBoxValue("AABlock");
        public static bool BlockBuff => MenuE.CheckBoxValue("buffBlock");
        public static bool BlockTargeted => MenuE.CheckBoxValue("targetedBlock");
        public static bool BlockSkillshots => MenuE.CheckBoxValue("skillshotBlock");
        public static bool BlockSpecial => MenuE.CheckBoxValue("specialBlock");
        public static int harassmode => MenuHarass.ComboBoxValue("harass.mode");
        public static int combomode => MenuCombo.ComboBoxValue("comboRMode");

        public static void Ini()
        {
            Menu = MainMenu.AddMenu("Fizz", "Silke");
            MenuCombo = Menu.AddSubMenu("Combo", "Varom");
            MenuCombo.Add("Combo.Q.Use", new CheckBox("Use Q"));
            MenuCombo.Add("Combo.W.Use", new CheckBox("Use W"));
            MenuCombo.Add("Combo.Ww.Use", new CheckBox("Use W Only for triple damage"));
            MenuCombo.Add("Combo.E.Use", new CheckBox("Use E"));
            MenuCombo.Add("Combo.R.Use", new CheckBox("Use R"));
            MenuCombo.Add("comboRMode", new ComboBox("R Mode:", 0, new string[] { "Always", "Only if killable" }));
            MenuHarass = Menu.AddSubMenu("Harass", "Lengvai");
            MenuHarass.Add("harass.mode", new ComboBox("Harass Mode: ", 1, new string[] { "Agressive Mode", "Safe Mode","Use W To Harass" }));
            MenuLane = Menu.AddSubMenu("LaneClear", "Lane_clearmenu");
            MenuLane.Add("Lane.Q.Use", new CheckBox("Use Q"));
            MenuLane.Add("Lane.Q.Mana", new Slider("Min Mana Use Q", 60, 0, 100));
            MenuLane.Add("Lane.W.Use", new CheckBox("Use W"));
            MenuLane.Add("Lane.W.Mana", new Slider("Min Mana Use W", 60, 0, 100));
            MenuLane.Add("Lane.E.Use", new CheckBox("Use E"));
            MenuLane.Add("lcUseEMinion", new Slider("Use E at atleast {0} minions", 3, 1, 6));
            MenuLane.Add("Lane.E.Mana", new Slider("Min Mana Use E", 60, 0, 100));
            MenuJungle = Menu.AddSubMenu("JungleClear", "Jungle_clear");
            MenuJungle.Add("Jungle.Q.Use", new CheckBox("Use Q"));
            MenuJungle.Add("Jungle.Q.Mana", new Slider("Min Mana Use Q", 60, 0, 100));
            MenuJungle.Add("Jungle.W.Use", new CheckBox("Use W"));
            MenuJungle.Add("Jungle.W.Mana", new Slider("Min Mana Use W", 60, 0, 100));
            MenuJungle.Add("Jungle.E.Use", new CheckBox("Use E"));
            MenuJungle.Add("Jungle.E.Mana", new Slider("Min Mana Use E", 60, 0, 100));
            MenuMisc = Menu.AddSubMenu("Misc", "asa");
            MenuMisc.CreateCheckBox("e.turret", "Use E on Turret Shots");
            MenuMisc.CreateCheckBox("damage.hp", "Show Damage Indicator");
            MenuMisc.CreateCheckBox("damage.percent", "Show Damage Percents");
            MenuE = Menu.AddSubMenu("Fizz E Evade");
            MenuE.CreateCheckBox("enable", "Enable SpellBlock");
            MenuE.CreateCheckBox("executeBlock", "Block Any Spell if it will Kill Player");
            MenuDraw = Menu.AddSubMenu("Draw", "Draw_menu");
            MenuDraw.Add("drawq", new CheckBox("Draw Q", false));
            MenuDraw.Add("draww", new CheckBox("Draw W", false));
            MenuDraw.Add("drawe", new CheckBox("Draw E", false));
            MenuDraw.Add("drawr", new CheckBox("Draw R", false));
            var enabledSpells = new List<SpellBlocker.EnabledSpell>();

            #region AutoAttacks
            var validAttacks = EmpowerdAttackDatabase.Current.Where(x => EntityManager.Heroes.Enemies.Any(h => h.Hero.Equals(x.Hero))).ToArray();
            if (validAttacks.Any())
            {
                MenuE.AddGroupLabel("Empowered Attacks");
                MenuE.CreateCheckBox("AABlock", "Block Empowered Attacks");
                foreach (var s in validAttacks.OrderBy(s => s.Hero))
                {
                    var spellname = s.MenuItemName;
                    if (!SpellBlocker.EnabledSpells.Any(x => x.SpellName.Equals(spellname)))
                    {
                        MenuE.AddLabel(spellname);
                        MenuE.CreateCheckBox("enable" + spellname, "Enable", s.DangerLevel > 1 || s.CrowdControl);
                        MenuE.CreateSlider("danger" + spellname, "Danger Level", s.DangerLevel, 1, 5);
                        enabledSpells.Add(new SpellBlocker.EnabledSpell(spellname));
                        MenuE.AddSeparator(0);
                    }
                }
            }
            #endregion AutoAttacks

            #region buffs
            var validBuffs = DangerBuffDataDatabase.Current.Where(x => EntityManager.Heroes.Enemies.Any(h => h.Hero.Equals(x.Hero))).ToArray();
            if (validBuffs.Any())
            {
                MenuE.AddSeparator(5);
                MenuE.AddGroupLabel("Danger Buffs");
                MenuE.CreateCheckBox("buffBlock", "Block Danger Buffs");

                foreach (var s in validBuffs.OrderBy(s => s.Hero))
                {
                    var spellname = s.MenuItemName;
                    if (!SpellBlocker.EnabledSpells.Any(x => x.SpellName.Equals(spellname)))
                    {
                        MenuE.AddLabel(spellname);
                        MenuE.CreateCheckBox("enable" + spellname, "Enable", s.DangerLevel > 1);
                        if (s.HasStackCount)
                        {
                            var stackCount = MenuE.CreateSlider("stackCount", "Block at Stack Count", s.StackCount, 1, s.MaxStackCount);
                            s.StackCountFromMenu = () => stackCount.CurrentValue;
                        }
                        MenuE.CreateSlider("danger" + spellname, "Danger Level", s.DangerLevel, 1, 5);
                        enabledSpells.Add(new SpellBlocker.EnabledSpell(spellname));
                        MenuE.AddSeparator(0);
                    }
                }
            }
            #endregion buffs

            #region Targeted
            var validTargeted = TargetedSpellDatabase.Current.Where(x => EntityManager.Heroes.Enemies.Any(h => h.Hero.Equals(x.hero))).ToArray();
            if (validTargeted.Any())
            {
                MenuE.AddSeparator(5);
                MenuE.AddGroupLabel("Targeted Spells");
                MenuE.CreateCheckBox("targetedBlock", "Block Targeted Spells");
                foreach (var s in validTargeted.OrderBy(s => s.hero))
                {
                    var spellname = s.MenuItemName;
                    if (!SpellBlocker.EnabledSpells.Any(x => x.SpellName.Equals(spellname)))
                    {
                        MenuE.AddLabel(spellname);
                        MenuE.CreateCheckBox("enable" + spellname, "Enable", s.DangerLevel > 1);
                        MenuE.CreateCheckBox("fast" + spellname, "Fast Block (Instant)", s.FastEvade);
                        MenuE.CreateSlider("danger" + spellname, "Danger Level", s.DangerLevel, 1, 5);
                        enabledSpells.Add(new SpellBlocker.EnabledSpell(spellname));
                        MenuE.AddSeparator(0);
                    }
                }
            }
            #endregion Targeted

            #region Speical spells
            var specialSpells = SpecialSpellsDatabase.Current.Where(s => EntityManager.Heroes.Enemies.Any(h => s.Hero.Equals(h.Hero))).ToArray();
            if (specialSpells.Any())
            {
                MenuE.AddSeparator(5);
                MenuE.AddGroupLabel("Special Spells");
                MenuE.CreateCheckBox("specialBlock", "Block Special Spells");
                foreach (var s in specialSpells)
                {
                    var display = s.MenuItemName;
                    if (!SpellBlocker.EnabledSpells.Any(x => x.SpellName.Equals(display)))
                    {
                        MenuE.AddLabel(display);
                        MenuE.CreateCheckBox($"enable{display}", "Enable", s.DangerLevel > 1);
                        MenuE.CreateCheckBox($"fast{display}", "Fast Block (Instant)", s.DangerLevel > 2);
                        MenuE.CreateSlider($"danger{display}", "Danger Level", s.DangerLevel, 1, 5);
                        enabledSpells.Add(new SpellBlocker.EnabledSpell(display));
                    }
                }
            }
            #endregion Speical spells

            #region SkillShots
            var validskillshots =
                SkillshotDatabase.Current.Where(s => (s.GameType.Equals(GameType.Normal) || s.GameType.Equals(Game.Type))
                && EntityManager.Heroes.Enemies.Any(h => s.IsCasterName(Champion.Unknown) || s.IsCasterName(h.Hero))).OrderBy(s => s.CasterNames[0]);
            if (validskillshots.Any())
            {
                MenuE.AddSeparator(5);
                MenuE.AddGroupLabel("SkillShots");
                MenuE.CreateCheckBox("skillshotBlock", "Block SkillShots");

                foreach (var s in validskillshots)
                {
                    var display = s.MenuItemName;
                    if (!SpellBlocker.EnabledSpells.Any(x => x.SpellName.Equals(display)))
                    {
                        MenuE.AddLabel(display);
                        MenuE.CreateCheckBox($"enable{display}", "Enable", s.DangerLevel > 1);
                        MenuE.CreateCheckBox($"fast{display}", "Fast Block (Instant)", s.FastEvade);
                        MenuE.CreateSlider($"danger{display}", "Danger Level", s.DangerLevel, 1, 5);
                        enabledSpells.Add(new SpellBlocker.EnabledSpell(display));
                    }
                }
            }
            #endregion SkillShots

            SpellBlocker.EnabledSpells = enabledSpells.ToArray();
        }
    }
}
