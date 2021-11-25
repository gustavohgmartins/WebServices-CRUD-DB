using System;

namespace wstest.Models
{
    public class User
    {
        public string name {get;set;}
        public string cpf {get;set;}
        public int age {get;set;}

        public User()
        {
            
        }
        public User(string name, string cpf, int age)
        {
            this.name = name;
            this.cpf = cpf;
            this.age = age;
        }
    }
}