using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace PsyTests
{
    public class LinkedNode<T>
    {
        Shkala shkala;
        ObservableCollection<T> collection = new();
        LinkedNode(Shkala shkala)
        {
            this.shkala = shkala;
        }
    }
    [DataContract]
    public struct TestMetaData
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Opisanie")]
        public string Opisanie { get; set; }
        [DataMember(Name = "PathToImg")]
        public string PathToImg { get; set; }
        [DataMember(Name = "Algorithm")]
        public string Algorithm { get; set; }
    }

    [DataContract]
  public class OprosnicTest
  {
        public OprosnicTest(TestMetaData metaData, ObservableCollection<Shkala> shakali, ObservableCollection<Key> keys)
        {
            this.metaData = metaData;
            Shakli = shakali;
            Keys = keys;
        }
        //public OprosnicTest() { }
        public TestMetaData metaData { get; set; }
        public ObservableCollection<Shkala> Shakli { get; set; }
        public ObservableCollection<Key> Keys { get; set; }
    }
    [DataContract]
    public class Shkala
    {
        public Shkala(string name)
        {
            Name = name;
            //Keys = new ObservableCollection<Key>();
        }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
       
        //[DataMember(Name = "Keys")]
        //public ObservableCollection<Key> Keys { get; set; }
       
        
        //public int GetNumberOfLinkedQuestions()
        //{
        //    return Keys.Count;
        //}
    }

    public class TestShkala:Shkala
    {
        private int value = 0;

        TestShkala(string name):base(name) 
        {}
        public void ChangeValueOfShkala(int value)
        {
            this.value += value;
            if (value < 0)
            {
                SetValueToZero();
            }
        }
        public int GetValue()
        {
            return value;
        }
        public void SetValueToZero()
        {
            value = 0;
        }
    }

    [DataContract]
    public class Key
    {
        [DataMember(Name = "shkala")]
        public Shkala shkala { get; set; }
        public Key(string name,Shkala shkala)
        {
            Name = name;
            this.shkala = shkala;
            Value = false;
        }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Value")]
        public bool Value { get; set; }
    }
    public class TestProcess
    {
            public TestProcess(List<Shkala> shkalas,List<Key> keys)
            {
            this.shkalas = shkalas;
            this.keys = keys;
                Random rng = new();
                int n = keys.Count;
                while(n>1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    Key value = keys[k];
                    keys[k] = keys[n];
                    keys[n] = value;
                }
            }
            public List<Shkala> shkalas=new();
            public List<Key> keys =new();
          
            int GetNumberOfQuestions()
            {
                return keys.Count;
            }
        }
}
