using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;

namespace AuthenticCredits
{
    public class Config
    {
        public static Config? Instance { get; set; }

        public virtual bool enabled { get; set; } = true;
        public virtual string creditsPath { get; set; } = "UserData/MapperCredits/";
        public virtual string creditsFile { get; set; } = "credits.json";
    }
}