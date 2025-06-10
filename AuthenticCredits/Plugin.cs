using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Loader;
using SiraUtil.Zenject;
using AuthenticCredits.Installers;
using IPALogger = IPA.Logging.Logger;
using Conf = IPA.Config.Config;
using HarmonyLib;
using System.Reflection;
using System.IO;
using UnityEngine;
using AuthenticCredits;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.IO.Compression;

namespace AuthenticCredits
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        [Init]
        public Plugin(IPALogger logger, Conf conf, Zenjector zenjector)
        {
            Config.Instance = conf.Generated<Config>(); //Dont like that I have to do this but transpile moment, prefer to transpile than rewrite the method and ill die on this hill

            zenjector.UseLogger(logger);
            zenjector.Install<AppInstaller>(Location.App, Config.Instance);
            zenjector.Install<MenuInstaller>(Location.Menu);
        }

        [OnStart]
        public void OnStart()
        {
            var harmony = new Harmony("ModdingPink.AuthenticCredits");
            harmony.PatchAll(Assembly);
        }
    }
}
