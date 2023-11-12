using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;

namespace PsyTests
{
    public class LinkedNode
    {
       public Shkala shkala { get; set; }
       public ObservableCollection<Key> collection = new();
       public LinkedNode(Shkala shkala)
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
        public TestMetaData(string Name, string Opisanie, string PathToImg,string Algorithm) 
        {
            this.Name=Name;
            this.Opisanie = Opisanie;
            this.PathToImg = PathToImg;
            this.Algorithm = Algorithm;
        }
    }
    //проверка данных на заполнение
    //автоматическое обновление тестов в списке
    //разные ссылки это не один и тот же объект
    //уникальные шкалы
    [DataContract]
  public class OprosnicTest
  {
        public OprosnicTest(TestMetaData metaData, List<Shkala> shakli, List<Key> keys)
        {
            this.metaData = metaData;
            Shakli = shakli;
            Keys = keys;
        }
        //public OprosnicTest() { }
        [DataMember]
        public TestMetaData metaData { get; set; }
        [DataMember]
        public List<Shkala> Shakli { get; set; }
        [DataMember]
        public List<Key> Keys { get; set; }
    }
    [DataContract]
    public class Shkala
    {
        public Shkala(string name)
        {
            Name = name;
            value = 0;
            //Keys = new ObservableCollection<Key>();
        }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        public int value;
        public void ChangeValueOfShkala(int value)
        {
            this.value += value;
            //if (value < 0)
            //{
            //    SetValueToZero();
            //}
        }
        public int GetValue()
        {
            return value;
        }
        public void SetValueToZero()
        {
            value = 0;
        }
        //[DataMember(Name = "Keys")]
        //public ObservableCollection<Key> Keys { get; set; }


        //
    }

    //public class TestShkala:Shkala
    //{
      
    //    TestShkala(Shkala shkala) : base(shkala.Name)
    //    {

    //    }
    //    //TestShkala(string name):base(name) 
    //    //{}
       
    //}

    [DataContract]
    public class Key
    {
        public Key(string name,Shkala shkala)
        {
            Name = name;
            this.shkala = shkala;
            Value = false;
        }

        [DataMember(Name = "shkala")]
        public Shkala shkala { get; set; }
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
            foreach(Shkala shkala in shkalas)
            {
                foreach(Key key in keys)
                {
                    if(key.shkala.Name==shkala.Name)
                    {
                        key.shkala=shkala;
                    }
                }
            }
                ShuffleKeys();
            }
            public List<Shkala> shkalas=new();
            public List<Key> keys =new();
            void ShuffleKeys()
            {
                Random rng = new();
                int n = keys.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    Key value = keys[k];
                    keys[k] = keys[n];
                    keys[n] = value;
                }
            }   
            public void SetAllValuesToZero()
        {
            foreach (Shkala shkala in shkalas)
            {
                shkala.SetValueToZero();
            }
        }
            public int GetNumberOfQuestions()
            {
                return keys.Count;
            }
        public int GetNumberOfLinkedQuestions(Shkala shkala)
        {
            int result = keys.Count(key => key.shkala == shkala);
            return result;
        }
    }
}
