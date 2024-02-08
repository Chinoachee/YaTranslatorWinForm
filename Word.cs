using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Translator {
    public class Word {
        private Dictionary<string,HashSet<string>> _words;
        public Word() {
            _words = new Dictionary<string,HashSet<string>>();
        }
        public void AddSourseWord(string sourseWord) {
            if(!string.IsNullOrEmpty(sourseWord) && !_words.ContainsKey(sourseWord)) {
                _words.Add(sourseWord,new HashSet<string>());
            }
        }
        public void AddTargetWord(string sourseWord,string targetWord) {
            if(!string.IsNullOrEmpty(targetWord)) {
                _words[sourseWord].Add(targetWord);
            }
        }
        public List<string> GetSourseWords() {
            return _words.Keys.ToList();
        }
        public string GetTargetWord(string sourseWord) {
            return _words[sourseWord].First();
        }
        public void SaveLanguages(string path) {
            string Json = JsonSerializer.Serialize(_words);
            File.WriteAllText(path,Json);
        }
        public void LoadLanguages(string path) {
            if(File.Exists(path)) {
                string Json = File.ReadAllText(path);
                _words = JsonSerializer.Deserialize<Dictionary<string,HashSet<string>>>(Json);
            }
        }
    }
}
