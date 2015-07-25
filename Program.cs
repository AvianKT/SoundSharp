using System;
using System.Media;
using LeagueSharp;
using LeagueSharp.Common;

namespace SoundSharp
{
    public class Program
    {
        public static Menu Config;
        private static readonly SoundPlayer rip = new SoundPlayer(Properties.Resources.rip);
        private static readonly SoundPlayer death1 = new SoundPlayer(Properties.Resources.death1);
        private static readonly SoundPlayer tootanky = new SoundPlayer(Properties.Resources.tootanky);
        private static readonly SoundPlayer noobteam = new SoundPlayer(Properties.Resources.dodge);
        private static readonly SoundPlayer Iunderstand = new SoundPlayer(Properties.Resources.iunderstand);
        private static readonly SoundPlayer strategy = new SoundPlayer(Properties.Resources.strategy);
        private static readonly SoundPlayer DING = new SoundPlayer(Properties.Resources.DING);
        private static readonly SoundPlayer uhr = new SoundPlayer(Properties.Resources.Victory);
        private static readonly SoundPlayer shop = new SoundPlayer(Properties.Resources.Shop);
        private static readonly SoundPlayer recall = new SoundPlayer(Properties.Resources.uhr);
        private static readonly SoundPlayer nooneeverdonedat = new SoundPlayer(Properties.Resources.nooneeverdonedat);
        private static readonly SoundPlayer lucksonofa = new SoundPlayer(Properties.Resources.lucksonofa);
        private static readonly SoundPlayer faker = new SoundPlayer(Properties.Resources.fakerwhatwasdat);
        private static readonly SoundPlayer doublelift = new SoundPlayer(Properties.Resources.Pimpin);
        private static readonly SoundPlayer MYTITSAREONFIRE = new SoundPlayer(Properties.Resources.firetits);
        private static readonly SoundPlayer bright = new SoundPlayer(Properties.Resources.brightside);
        private static readonly SoundPlayer best = new SoundPlayer(Properties.Resources.yourethebest);
        private static readonly SoundPlayer dontstop = new SoundPlayer(Properties.Resources.dontstop);
        private static readonly SoundPlayer moneybitch = new SoundPlayer(Properties.Resources.moneybitch);
       


        private static int LastPlayedSound = 0;
        private static int LastPlay = 0;
        private static int LastPlay1 = 0;
        private static int inshop = 0;
        private static int goldcast = 0;


        public Spell Shopkey;
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Onload;
        }

        private static void Onload(EventArgs args)
        {

            Config = new Menu("Sound#", "Sound#", true);

            var themes = Config.AddSubMenu(new Menu("Hotkeys", "Hotkeys"));
            themes.AddItem(new MenuItem("enable", "Enable Hotkeys?").SetValue(false));
            themes.AddItem(new MenuItem("s1", "My Tits are on fire - BoxBox").SetValue(new KeyBind('.', KeyBindType.Press)));
            themes.AddItem(new MenuItem("s2", "Fuck this noob team - Daniel").SetValue(new KeyBind('.', KeyBindType.Press)));
            themes.AddItem(new MenuItem("s3", "Random German Song").SetValue(new KeyBind('.', KeyBindType.Press)));
            themes.AddItem(new MenuItem("s4", "Fuck that shit, she's too tanky - xPeke").SetValue(new KeyBind('.', KeyBindType.Press)));
            themes.AddItem(new MenuItem("s5", "Victory! - WoW Sound").SetValue(new KeyBind('.', KeyBindType.Press)));
            themes.AddItem(new MenuItem("s6", "Ding! - WoW Sound").SetValue(new KeyBind('.', KeyBindType.Press)));
            themes.AddItem(new MenuItem("s7", "Noone's ever done that before! - Kobe").SetValue(new KeyBind('.', KeyBindType.Press)));

            themes.AddItem(new MenuItem("s11", "Keys are required to be rebinded before playing a sound"));

            Config.AddItem(new MenuItem("sad", "Player Death Sound [BROKEN]").SetValue(false));
            Config.AddItem(new MenuItem("Levelup", "Level Up Sound ~ DING").SetValue(true));
            Config.AddItem(new MenuItem("kill", "On Kill Sound [BROKEN]").SetValue(false));
            Config.AddItem(new MenuItem("gold", "On 2k Gold + Sound").SetValue(true));
            Config.AddItem(new MenuItem("endgame", "GameEnd Sound").SetValue(true));
            Config.AddItem(new MenuItem("shopzone", "Shopzone Sound Effect").SetValue(true));
            Config.AddItem(new MenuItem("ignite", "Player is Ignited sound").SetValue(false));
            Config.AddItem(new MenuItem("recall", "Recall Sound [LOUD!]").SetValue(false));
        
            Config.AddToMainMenu();
            Obj_AI_Hero.OnDamage += Riperino;
            Obj_AI_Hero.OnDamage += Opiskill;
            Obj_AI_Hero.OnLevelUp += Levelup;
            Game.OnUpdate += OnUpdate;
            Game.OnEnd += End;
            Obj_AI_Hero.OnBuffAdd += Ignitebuff;
        }

        private static void Ignitebuff(Obj_AI_Base sender, Obj_AI_BaseBuffAddEventArgs args)
        {
            if (sender.IsMe && args.Buff.Name == "summonerdot" && Config.Item("ignite").GetValue<bool>() && Environment.TickCount - LastPlayedSound > 8000)
            PlaySound1(MYTITSAREONFIRE);
            //MY TITS ARE ON FIREEEE!!!


        }

        private static void OnUpdate(EventArgs args)
        {
            ObjectManager.Player.HasBuffOfType(BuffType.Poison);
            if (ObjectManager.Player.InShop() && Config.Item("shopzone").GetValue<bool>() && inshop != 1)
            {
                PlaySound1(shop);
                Utility.DelayAction.Add(100, () => inshop = 1);
            }
            if (!ObjectManager.Player.InShop())
            {
                inshop = 0;
            }
            if (ObjectManager.Player.IsRecalling() && Environment.TickCount - LastPlay1 > 20000 && Config.Item("recall").GetValue<bool>())
            {
                PlaySound1(recall);
                LastPlay1 = Environment.TickCount;
            }
            if (ObjectManager.Player.Gold > 2000 && Config.Item("gold").GetValue<bool>() && goldcast != 1)
            {
                PlaySound1(doublelift);
                LastPlayedSound = Environment.TickCount;
                Utility.DelayAction.Add(100, () => goldcast = 1);
            }
            if (ObjectManager.Player.Gold <= 2000)
            {
                goldcast = 0;
            }
            if (Config.Item("enable").GetValue<bool>())
            {
                if (Config.Item("s1").GetValue<KeyBind>().Active)
                    PlaySound1(MYTITSAREONFIRE);
                if (Config.Item("s2").GetValue<KeyBind>().Active)
                   PlaySound1(noobteam);
                if (Config.Item("s3").GetValue<KeyBind>().Active)
                    PlaySound1(recall);
                if (Config.Item("s4").GetValue<KeyBind>().Active)
                    PlaySound1(tootanky);
                if (Config.Item("s5").GetValue<KeyBind>().Active)
                    PlaySound1(uhr);
                if (Config.Item("s6").GetValue<KeyBind>().Active)
                    PlaySound1(DING);
                if (Config.Item("s7").GetValue<KeyBind>().Active)
                    PlaySound1(nooneeverdonedat);
            }
        }
        private static void End(GameEndEventArgs args)
        {

            if (args.WinningTeam != ObjectManager.Player.Team)
                return;

            if (Config.Item("endgame").GetValue<bool>())
            PlaySound1(uhr);
        }

        private static void Opiskill(AttackableUnit sender, AttackableUnitDamageEventArgs args)
        {
            if (sender.IsEnemy && args.Damage > sender.Health && Config.Item("kill").GetValue<bool>() && sender.IsValid<Obj_AI_Hero>())
            {
                OnKill();
            }
        }

        private static void Levelup(Obj_AI_Base sender, EventArgs args)
        {
            if (sender.IsMe && Config.Item("Levelup").GetValue<bool>() && ObjectManager.Player.Level != 6)
            {
                PlaySound1(DING);
                LastPlayedSound = Environment.TickCount;
            }
        }


        private static void Riperino(AttackableUnit sender, AttackableUnitDamageEventArgs args)
        {
            if (sender.IsMe && args.Damage > ObjectManager.Player.Health && Config.Item("sad").GetValue<bool>())
                OnDeath();
        }

        public static void PlaySound1(SoundPlayer sound)
        {
                sound.Play();
        }

        private static void OnDeath(SoundPlayer sound = null)
        {
            if (sound != null)
            {
                try
                {
                    sound.Play();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                var rnd = new Random();
                switch (rnd.Next(1, 7))
                {
                    case 1:
                        OnDeath(tootanky);
                        break;
                    case 2:
                        OnDeath(noobteam);
                        break;
                    case 3:
                        OnDeath(death1);
                        break;
                    case 4:
                        OnDeath(strategy);
                        break;
                    case 5:
                        OnDeath(Iunderstand);
                        break;
                    case 6:
                        OnDeath(bright);
                        break;
                    case 7:
                        OnDeath(lucksonofa);
                        break;

                }
            }
        }



        private static void OnKill(SoundPlayer sound = null)
        {
            if (sound != null)
            {
                try
                {
                    sound.Play();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else if (Environment.TickCount - LastPlayedSound > 8000)
            {
                var rnd = new Random();
                switch (rnd.Next(1, 5))
                {
                    case 1:
                        OnKill(nooneeverdonedat);
                        break;
                    case 2:
                        OnKill(faker);
                        break;
                    case 3:
                        OnKill(moneybitch);
                        break;
                    case 4:
                        OnKill(best);
                        break;
                    case 5:
                        OnKill(dontstop);
                        break;
                }
                LastPlayedSound = Environment.TickCount;
            }
        }
    }
}
                
            
       
    


            
        


            
        
    


