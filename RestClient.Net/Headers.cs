using System.Collections;
using System.Collections.Generic;

namespace RestClient.Net
{
    public sealed class Headers : IEnumerable<KeyValuePair<string, IList<string>>>
    {
        private readonly IDictionary<string, IList<string>> _headers = new Dictionary<string, IList<string>>();

        public void Add(string name, string value)
        {
            if (value == null) return;

            if (_headers.ContainsKey(name))
            {
                _headers[name].Add(value);
            }
            else
            {
                _headers.Add(name, new List<string>(1) { value });
            }
        }

        public IList<string> GetValues(string key)
        {
            if (_headers.ContainsKey(key)) return _headers[key];

            return default(IList<string>);
        }

        public string GetFirstValue(string key)
        {
            if (_headers.ContainsKey(key)) return _headers[key][0];

            return default(string);
        }

        internal void Add(string name, IEnumerable<string> values)
        {
            if (values == null) return;

            if (_headers.ContainsKey(name))
            {
                (_headers[name] as List<string>)?.AddRange(values);
            }
            else
            {
                _headers.Add(name, new List<string>(values));
            }
        }

        internal void AddRange(IEnumerable<KeyValuePair<string, IEnumerable<string>>> items)
        {
            foreach (var item in items)
            {
                Add(item.Key, new List<string>(item.Value));
            }
        }

        public IEnumerator<KeyValuePair<string, IList<string>>> GetEnumerator() => _headers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _headers.GetEnumerator();
    }
}

#region archive
/*   public sealed class Headers : IDictionary<string, List<string>>
    {
        private readonly Dictionary<string, List<string>> _headers = new Dictionary<string, List<string>>();

        public ICollection<string> Keys => _headers.Keys;

        public ICollection<List<string>> Values => _headers.Values;

        public int Count => _headers.Count;

        public bool IsReadOnly => true;

        public List<string> this[string key]
        {
            get => _headers[key];
            set { }
        }

        public void Add(string name, string value)
        {
            if (value == null) return;

            if (_headers.ContainsKey(name))
            {
                _headers[name].Add(value);
            }
            else
            {
                _headers.Add(name, new List<string>(1) { value });
            }
        }

        public IList<string> GetValues(string key)
        {
            if (_headers.ContainsKey(key)) return _headers[key];

            return default(IList<string>);
        }

        public string GetValue(string key)
        {
            if (_headers.ContainsKey(key)) return _headers[key][0];
           
            return default(string);
        }

        internal void Add(string name, IEnumerable<string> values)
        {
            if (values == null) return;

            if (_headers.ContainsKey(name))
            {
                _headers[name].AddRange(values);
            }
            else
            {
                _headers.Add(name, new List<string>(values));
            }
        }

        internal void AddRange(IEnumerable<KeyValuePair<string, IEnumerable<string>>> items)
        {
            foreach (var item in items)
            {
                Add(item.Key, new List<string>(item.Value));
            }
        }

        public void Add(string key, List<string> value)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            return _headers.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return false;
        }

        public bool TryGetValue(string key, out List<string> value)
        {
            value = null;
            return false;
        }

        public void Add(KeyValuePair<string, List<string>> item) { }


        public void Clear() { }
        

        public bool Contains(KeyValuePair<string, List<string>> item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, List<string>>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, List<string>> item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, List<string>>> GetEnumerator()
        {
           return  _headers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _headers.GetEnumerator();
        }
    }
*/
//public sealed class HeadersOld : IEnumerable<KeyValuePair<string, IEnumerable<string>>>
//{
//    private readonly Dictionary<string, IEnumerable<string>> _headers = new Dictionary<string, IEnumerable<string>>();

//    public void AddBearer(string token) => Add("Authorization", $"Bearer {token}");

//    public int Count => _headers.Count;

//    public void Add(string name, string value)
//    {

//    }

//    internal void Add(string key, IEnumerable<string> values)
//    {

//    }

//    internal void AddRange(IEnumerable<KeyValuePair<string, IEnumerable<string>>> range)
//    {
//        foreach (var item in range)
//        {
//            Add(item.Key, item.Value);
//        }
//    }

//    public IEnumerable<string> GetValues(string key)
//    {
//        if (_headers.ContainsKey(key)) return _headers[key];

//        return default(IEnumerable<string>);
//    }

//    public string GetValue(string key)
//    {
//        if (_headers.ContainsKey(key))
//        {
//            if (_headers[key] is IList<string> list && list.Count > 0)
//            {
//                return list[0];
//            }

//            foreach (var value in _headers[key])
//            {
//                if (!string.IsNullOrEmpty(value)) return value;
//            }
//        }

//        return default(string);
//    }

//    public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator() => _headers.GetEnumerator();

//    IEnumerator IEnumerable.GetEnumerator() => _headers.GetEnumerator();
//}
#endregion
