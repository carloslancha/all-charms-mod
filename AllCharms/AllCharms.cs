using System;
using Modding;
using Charms;

namespace AllCharms {
    public class AllCharms : Mod {
        private const string version = "Beta-1.5.78-Rev1";
        private Charms.Charms Charms_ = new Charms.Charms();
        public override void Initialize() {
            ModHooks.CharmUpdateHook += CharmUpdate;
        }
        private void CharmUpdate(PlayerData pd, HeroController hc) {
            try {
                Charms_.massageCharmList(PlayerData.instance.equippedCharms);
            } catch(Exception Ex) {
                Log(Ex.ToString());
            }
        }
        public override string GetVersion() => version;
    }
}