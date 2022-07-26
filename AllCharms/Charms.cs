using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Charms {
    enum eCharm {};
    public class Charms {
        private List<int> equippedCharms = null;
        public Charms() { 
            this.equippedCharms = new List<int>(0);
        }
        public void updateCharms(List<int> charmList) {
            try {
                if(massageCharmList(charmList)) { WriteHtmlDataFile(); }
            } catch(Exception Ex) {
                throw Ex;
            }
        }
        private bool massageCharmList(List<int> charmList) {
            List<int> newCharmList = new List<int>(charmList);
            int value = -1;

            if (newCharmList.Contains(23)) {
                value =  PlayerData.instance.fragileHealth_unbreakable ? 45 : 23;

                replaceCharmValue(newCharmList, 23, value);
            }
            if (newCharmList.Contains(24)) {
                value = PlayerData.instance.fragileGreed_unbreakable ? 46 : 24;

                replaceCharmValue(newCharmList, 24, value);
            }
            if (newCharmList.Contains(25)) {
                value = PlayerData.instance.fragileStrength_unbreakable ? 47 : 25;

                replaceCharmValue(newCharmList, 25, value);
            }
            if (newCharmList.Contains(36)) {
                value = PlayerData.instance.gotShadeCharm ? 48 : 36;
                
                replaceCharmValue(newCharmList, 36, value);
            }
            if (newCharmList.Contains(40)) {
                value = (40 + PlayerData.instance.grimmChildLevel) -1;
                
                replaceCharmValue(newCharmList, 40, value);
            } 
            
            this.equippedCharms = newCharmList;

            return value != -1 ? true : false;
        }
        private void replaceCharmValue(List<int> charmList, int originalValue, int newValue) {
            if (originalValue != newValue) {
                charmList[charmList.IndexOf(originalValue)] = newValue;
            }
        }
        private string toString() {
            string timeString = DateTime.Now.ToString("hh:mm:ss"),
                    html_ = $"<html><body><script>" +
                            $"const data = {{" +
                            $"charms: [{String.Join(", ", this.equippedCharms.Select(x => x.ToString()).ToArray())}]," +
                            $"lastUpdateTime: '{timeString}'" +
                            $"}};" +
                            $"parent.postMessage(data, \"*\");" +
                            $"</script></body></html>";
            return html_;
        }
        private void WriteHtmlDataFile() {
            string path = Directory.GetCurrentDirectory() + @"\hollow_knight_Data\Managed\Mods\AllCharms\Overlay";
            string fileName = "data.html";
            string fullPath = path + "\\" + fileName;

            // Create folder if doesn't exist 
            System.IO.Directory.CreateDirectory(path);

            try {    
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fullPath)) { File.Delete(fullPath); }  
    
                // Create a new file     
                using (FileStream fs = File.Create(fullPath)) {
                    string html_ = toString();
                    
                    fs.Write(new UTF8Encoding(true).GetBytes(html_), 0, html_.Length);
                }
            } catch (Exception Ex) { throw Ex; }
        }
    }
}