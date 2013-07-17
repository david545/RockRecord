using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RockRecord.Models;
namespace RockRecord.Filters
{
    public class AllowEditMemberProfileAttribute:AllowEditAttribute
    {

        protected override string GetCompareEmail(int id)
        {
            
            Member member = db.Members.Find(id);
            if (member == null) return "";
            else return member.Email;
        }
    }
}