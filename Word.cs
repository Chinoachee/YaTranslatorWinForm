using System.Collections.Generic;
using System.Linq;

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
        public string GetTargetWord(string sourseWord) {
            return _words[sourseWord].First();
        }
    }
}
