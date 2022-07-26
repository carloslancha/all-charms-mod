using System;
using Modding;

namespace AllCharms {
    public class AllCharms : Mod {
        private const string version = "1.5.78-Rev1";
        private Charms.Charms Charms_ = new Charms.Charms();
        public override void Initialize() {
            ModHooks.CharmUpdateHook += CharmUpdate;
        }
        private void CharmUpdate(PlayerData pd, HeroController hc) {
            try {
                Log(pd.equippedCharms.Count.ToString());
                Charms_.updateCharms(pd.equippedCharms);
            } catch(Exception Ex) {
                Log("Exception: " + Ex.ToString());
            }
        }
        public override string GetVersion() => version;
    }
}