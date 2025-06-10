﻿using Zenject;

namespace AuthenticCredits.Installers
{
    public class AppInstaller : Installer
    {
        private readonly Config _config;

        public AppInstaller(Config config) => _config = config;

        public override void InstallBindings()
        {
            Container.BindInstance(_config);
            Container.BindInstance(new Utilities.Credits());
        }
    }
}