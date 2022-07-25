using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Charms {
    public class Charms {
        private List<int> equippedCharms;
        public Charms() {
            equippedCharms = null;
        }
        public void massageCharmList(List<int> charmList) {
            List<int> newCharmList = new List<int>(charmList);

            if (newCharmList.Contains(23)) {
                int value = 23;

                if (PlayerData.instance.fragileHealth_unbreakable) { value = 45; }

                replaceCharmValue(newCharmList, 23, value);
            }
            
            if (newCharmList.Contains(24)) {
                int value = 24;

                if (PlayerData.instance.fragileGreed_unbreakable) { value = 46; }

                replaceCharmValue(newCharmList, 24, value);
            }
            if (newCharmList.Contains(25)) {
                int value = 25;

                if (PlayerData.instance.fragileStrength_unbreakable) { value = 47; }

                replaceCharmValue(newCharmList, 25, value);
            }
            if (newCharmList.Contains(36)) {
                int value = 36;

                if (PlayerData.instance.gotShadeCharm) { value = 48; }
                
                replaceCharmValue(newCharmList, 36, value);
            }
            if (newCharmList.Contains(40)) {
                int value = (40 + PlayerData.instance.grimmChildLevel) -1;
                
                replaceCharmValue(newCharmList, 40, value);
            }
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
                            $"charms: [{String.Join(", ", equippedCharms.Select(x => x.ToString()).ToArray())}]," +
                            $"lastUpdateTime: '{timeString}'" +
                            $"}};" +
                            $"parent.postMessage(data, \"*\");" +
                            $"</script></body></html>";
            return html_;
        }
        private void WriteHtmlDataFile() {
            string path = Directory.GetCurrentDirectory() + @"\hollow_knight_Data\Managed\Mods\AllCharms";
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