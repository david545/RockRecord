using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using RockRecord.Models;
namespace RockRecord.Filters
{
    public class AllowEditReviewAttribute:AllowEditAttribute
    {

        protected override string GetCompareEmail(int id)
        {
            
            Review review = db.Reviews.Find(id);
            if (review == null) return "";
            else return review.Member.Email;
        }
    }
}
