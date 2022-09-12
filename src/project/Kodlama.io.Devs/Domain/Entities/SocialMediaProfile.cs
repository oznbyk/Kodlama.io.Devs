using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SocialMediaProfile : Entity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual User User { get; set; }


        public SocialMediaProfile()
        {

        }

        public SocialMediaProfile(int id,string name, string url): this()
        {
            Id = id;
            Name = name;
            Url = url;
        }
    }
}
