using System;
using System.Collections.Generic;

using Slb.Ocean.Core;

namespace MultipleSave
{
    public class Plugin : Slb.Ocean.Core.Plugin
    {
        public override string AppVersion
        {
            get { return "2024.1"; }
        }

        public override string Author
        {
            get { return "diogosctn"; }
        }

        public override string Contact
        {
            get { return "diogosctn@protonmail.com"; }
        }

        public override IEnumerable<PluginIdentifier> Dependencies
        {
            get { return null; }
        }

        public override string Description
        {
            get { return "A toy model plugin designed to validate data persistence and hierarchical storage within Petrel. It captures text and numeric inputs to generate and retrieve custom domain objects in the Input Tree."; }
        }

        public override string ImageResourceName
        {
            get { return null; }
        }

        public override Uri PluginUri
        {
            get { return new Uri("https://www.pluginuri.info"); }
        }

        public override IEnumerable<ModuleReference> Modules
        {
            get 
            {
                yield return new ModuleReference(typeof(MultipleSave.Module)); 
                // Please fill this method with your modules with lines like this:
                //yield return new ModuleReference(typeof(Module));

            }
        }

        public override string Name
        {
            get { return "Plugin"; }
        }

        public override PluginIdentifier PluginId
        {
            get { return new PluginIdentifier(GetType().FullName, GetType().Assembly.GetName().Version); }
        }

        public override ModuleTrust Trust
        {
            get { return new ModuleTrust("Default"); }
        }
    }
}
