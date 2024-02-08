using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Translator {
    public class Language {
        private Dictionary<string,HashSet<string>> _languages;
        public Language() {
            _languages = new Dictionary<string,HashSet<string>>();
        }
        public void AddSourseLanguage(string sourselanguage) {
            if(!string.IsNullOrEmpty(sourselanguage) && !_languages.ContainsKey(sourselanguage)) {
                _languages.Add(sourselanguage,new HashSet<string>());
            }
        }
        public void AddTargetLanguage(string sourseLanguage,string targetLanguage) {
            if(!string.IsNullOrEmpty(sourseLanguage)) {
                _languages[sourseLanguage].Add(targetLanguage);
            }
        }
        public List<string> GetSourseLanguages() {
            return _languages.Keys.ToList();
        }
        public List<string> GetTargetLanguages(string sourseLanguage) {
            return _languages[sourseLanguage].ToList();
        }
        public void SaveLanguages(string path) {
            string Json = JsonSerializer.Serialize(_languages);
            File.WriteAllText(path,Json);
        }
        public void LoadLanguages(string path) {
            if(File.Exists(path)) {
                string Json = File.ReadAllText(path);
                _languages = JsonSerializer.Deserialize<Dictionary<string,HashSet<string>>>(Json);
            }
        }
    }
}
