using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJudge.Models;

namespace OnlineJudge.ResponseModels {
    public class ProgrammingLanguageData{
        public string Name { set; get; }
        public int Id;


        public ProgrammingLanguageData(ProgrammingLanguage data){
            this.Name = data.Name;
            this.Id = (int) data.Id;
        }

        public static List<ProgrammingLanguageData> MapTo(IEnumerable<ProgrammingLanguage> data){
            var mapped = new List<ProgrammingLanguageData>();
            foreach (var row in data){
                mapped.Add(new ProgrammingLanguageData(row));
            }

            return mapped;
        }
    }
}