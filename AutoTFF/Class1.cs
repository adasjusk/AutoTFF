using System;
using LabApi.Events;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;

namespace AutoTFF
{
    public sealed class Class1 : Plugin
    {
        public static Class1 Instance { get; private set; }

        public override string Name => "AutoTFF";

        public override string Author => "adasjusk";

        public override Version Version { get; } = new Version(1, 0, 0);

        public override string Description => "Enables friendly fire on round end and disables it on round start.";

        public override Version RequiredApiVersion
        {
            get
            {
                try
                {
                    object compiledVersion = LabApiProperties.CompiledVersion;
                    if (compiledVersion is Version result)
                    {
                        return result;
                    }
                    string text = compiledVersion?.ToString();
                    if (!string.IsNullOrEmpty(text) && Version.TryParse(text, out var result2))
                    {
                        return result2;
                    }
                }
                catch
                {
                }
                return new Version(1, 0, 0);
            }
        }

        public override void Enable()
        {
            //IL_001e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0028: Expected O, but got Unknown
            Instance = this;
            ServerEvents.RoundEnded += OnRoundEnded;
            ServerEvents.RoundStarted += new LabEventHandler(OnRoundStarted);
            try
            {
                Server.FriendlyFire = false;
                Logger.Info("AutoTFF: disabled friendly fire on plugin start.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public override void Disable()
        {
            //IL_0018: Unknown result type (might be due to invalid IL or missing references)
            //IL_0022: Expected O, but got Unknown
            ServerEvents.RoundEnded -= OnRoundEnded;
            ServerEvents.RoundStarted -= new LabEventHandler(OnRoundStarted);
            Instance = null;
        }

        private void OnRoundEnded(RoundEndedEventArgs ev)
        {
            try
            {
                Server.FriendlyFire = true;
                Logger.Info("Enabled friendly fire on round end.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void OnRoundStarted()
        {
            try
            {
                Server.FriendlyFire = false;
                Logger.Info("Disabled friendly fire on round start.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}