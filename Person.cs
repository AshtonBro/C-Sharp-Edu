using System;

namespace AshtonBro.CodeBlog._1
{
    public class Person
    {
        private string _name;
        public string Name
        { get
            {
                return _name;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentNullException("The name isn't can empty");
                }

            }
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            
            
        }
    }
}
