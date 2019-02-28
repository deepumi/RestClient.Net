using System.Collections.Generic;
using System.Net.Http;

namespace RestClient.Net
{
    internal sealed class FormPostParameter
    {
        private IDictionary<string, string> _formDictionary;

        internal void AddFormValue(string name, string value)
        {
            if (_formDictionary == null) _formDictionary = new Dictionary<string, string>();

            if (!_formDictionary.ContainsKey(name))
            {
                _formDictionary.Add(name, value);
            }
        }

        internal void AddFormValue(IDictionary<string, string> dictionary)
        {
            if (dictionary == null || dictionary.Count == 0) return;

            foreach (var item in dictionary)
            {
                AddFormValue(item.Key, item.Value); // //https://stackoverflow.com/questions/43158250/how-to-post-using-httpclient-content-type-application-x-www-form-urlencoded
            }
        }

        internal FormUrlEncodedContent FormContent
        {
            get
            {
                if (_formDictionary?.Count > 0)
                {
                    return new FormUrlEncodedContent(_formDictionary);
                }

                return null;
            }
        }
    }
}
