using System;
using System.Collections.Generic;
using System.Text;

namespace iTrash
{

    public class Rootobject
    {
        public Container[] Property1 { get; set; }
    }

    public class Container
    {
        public int capaciy { get; set; }
        public int id { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public float used_capacity { get; set; }
    }
}
