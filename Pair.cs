using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Pair<K,V>
    {
        public K key;
        public V value; 
        
        public Pair(K key, V value)
        {
            this.key = key;
            this.value = value; 
        }
    }
}
