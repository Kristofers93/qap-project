using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public abstract class Parameter<T>
    {
        readonly string name;
        //readonly Type type;
        T value;


        public Parameter(string _name, T _value)
        {
            this.name = _name;
            this.value = _value;

        }

        public abstract bool IsValid();

    }


}
