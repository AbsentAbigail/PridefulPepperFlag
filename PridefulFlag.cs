using Deadpan.Enums.Engine.Components.Modding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WildfrostHopeMod;
using WildfrostHopeMod.Configs;

namespace PridefulPepperFlag
{
    public class PridefulFlag : WildfrostMod
    {
        public PridefulFlag(string modDirectory) : base(modDirectory)
        {
        }

        public override string GUID => "absentabigail.wildfrost.pridefulflag";

        public override string[] Depends => new string[]
        {
            "hope.wildfrost.configs"
        };

        public override string Title => "Prideful Flag";

        public override string Description => "Makes the Pepper Flag a little more spicy";

        private Sprite[] sprites;
        private List<Sprite> alternateSprites;
        private bool loaded = false;

        [ConfigSlider(0, 1)]
        [ConfigItem(0.9f, null, "Chance to replace default with pride flag")]
        public float replacementOdds;

        [ConfigInput]
        [ConfigItem(true, null, "Enable asexual flag resprite")]
        public bool asexualFlag;

        [ConfigInput]
        [ConfigItem(true, null, "Enable bisexual flag resprite")]
        public bool bisexualFlag;

        [ConfigInput]
        [ConfigItem(true, null, "Enable lesbian flag resprite")]
        public bool lesbianFlag;

        [ConfigInput]
        [ConfigItem(true, null, "Enable non-binary flag resprite")]
        public bool nonbinaryFlag;

        [ConfigInput]
        [ConfigItem(true, null, "Enable pansexual flag resprite")]
        public bool pansexualFlag;

        [ConfigInput]
        [ConfigItem(true, null, "Enable rainbow pride flag resprite")]
        public bool prideFlag;

        [ConfigInput]
        [ConfigItem(true, null, "Enable progressive pride flag resprite")]
        public bool progressFlag;

        [ConfigInput]
        [ConfigItem(true, null, "Enable transgender flag resprite")]
        public bool transFlag;

        [ConfigInput]
        [ConfigItem(true, null, "Enable vincian flag resprite")]
        public bool vincianFlag;

        public override void Load()
        {
            base.Load();
            CreateModAssets();
            Events.OnCardDataCreated += PridefulPepperFlag;
            ConfigManager.GetConfigSection(this).OnConfigChanged += ConfigChanged;
        }
        private void ConfigChanged(ConfigItem configItem, object arg1)
        {
            CreateModAssets();
        }

        public override void Unload()
        {
            base.Unload();
            Events.OnCardDataCreated -= PridefulPepperFlag;
            ConfigManager.GetConfigSection(this).OnConfigChanged -= ConfigChanged;

            EmptyList();
        }

        public void CreateModAssets()
        {
            EmptyList();

            alternateSprites = new List<Sprite>();

            if (asexualFlag)
            {
                alternateSprites.Add(GetSprite("Asexual.png"));
            }
            if (bisexualFlag)
            {
                alternateSprites.Add(GetSprite("Bisexual.png"));
            }
            if (lesbianFlag)
            {
                alternateSprites.Add(GetSprite("Lesbian.png"));
            }
            if (nonbinaryFlag)
            {
                alternateSprites.Add(GetSprite("Nonbinary.png"));
            }
            if (pansexualFlag)
            {
                alternateSprites.Add(GetSprite("Pansexual.png"));
            }
            if (prideFlag)
            {
                alternateSprites.Add(GetSprite("Pride.png"));
            }
            if (progressFlag)
            {
                alternateSprites.Add(GetSprite("Progress.png"));
            }
            if (transFlag)
            {
                alternateSprites.Add(GetSprite("Trans.png"));
            }
            if (vincianFlag)
            {
                alternateSprites.Add(GetSprite("Vincian.png"));
            }
        }

        private void EmptyList()
        {
            if (alternateSprites != null && alternateSprites.Count > 0)
            {
                foreach (var item in alternateSprites)
                {
                    item.Destroy();
                }
            }
        }

        private void PridefulPepperFlag(CardData cardData)
        {
            if (cardData.name != "PepperFlag")
                return;

            if (alternateSprites.Count == 0)
                return;
            
            if (Random.Range(0f, 1f) > replacementOdds)
                return;

            cardData.mainSprite = alternateSprites.RandomItem();
        }

        private Sprite GetSprite(string name)
        {
            return ImagePath(name).ToSprite();
        }
    }
}