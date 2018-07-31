using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJudge.Models;

namespace OnlineJudge.ResponseModels {
    class ContestListItem{
        public int Id{ set; get; }
        public string Title { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public string Creator { set; get; }

        public ContestListItem(Contest contest){
            this.Id = Id;
            this.Title = Title; 
            this.StartDate = StartDate; 
            this.EndDate = EndDate; 
            this.Creator = Creator; 
        }


        public static List<ContestListItem> MapTo(IEnumerable<Contest> contests){
            var mapped = new List<ContestListItem>();

            foreach (var contest in contests){
                mapped.Add(new ContestListItem(contest));
            }

            return mapped;
        }
    }

    

    
}