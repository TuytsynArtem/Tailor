using SQLite;
using System;

namespace Tailor.Models
{
    public class Client
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Mail { get; set; }
        public string DlinaIzdeliya { get; set; }
        public string Grud { get; set; }
        public string Sheya { get; set; }
        public string Plecho { get; set; } 
        public string ShirinaPereda { get; set; }
        public string ShirinaSpiny { get; set; }


        

        public override string ToString()
        {
            return this.Name +"\n"+ this.LastName + "\n" + this.Number + "\n" + this.Mail; 
        }
    }
}