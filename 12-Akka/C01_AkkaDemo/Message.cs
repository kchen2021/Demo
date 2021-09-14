using System;

namespace C01_AkkaDemo
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Id:{Id},Name:{Name},Age:{Age}";
        }
    }
}
