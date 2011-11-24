using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public abstract class Parameter
    {

        public string Name { get; protected set; }

        public abstract bool IsValid();

    }

    public abstract class Parameter<T> : Parameter
    {
        public T Value;


        public Parameter(string _name, T _value)
        {
            this.Name = _name;
            this.Value = _value;

        }


    }


}
