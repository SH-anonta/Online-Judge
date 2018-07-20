using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace OnlineJudge.Models {
    // Set table name to OJDB Online Judge data base
    
    public class OjDBContext : DbContext{

        public OjDBContext(): base("name=OjDBConnectionString"){
            Database.SetInitializer<OjDBContext>(new OjDBInitializer());
        }

        // this consturtor is used by testing classes
        public OjDBContext(DbConnection dbConnection) : base(dbConnection, true){
            Database.SetInitializer<OjDBContext>(new OjDBInitializer());
        }

        public DbSet<Problem> Problems { get; set; }
//        public DbSet<User> Users { get; set; }
//        public DbSet<UserType> UserTypes { get; set; }


        
        // for keeping system logs, this table will be used by log4net
        // Commented out intentionally, logs are kept in a text file for now
//        public DbSet<Log> Logs { get; set; }
    }

//    public class OjDBInitializer :  DropCreateDatabaseAlways<OjDBContext>{
    public class OjDBInitializer :  DropCreateDatabaseIfModelChanges<OjDBContext>{
        protected override void Seed(OjDBContext context){
//            seedUserTypeData(context);
            seedProblemsData(context);

            // this must be at the end of this function
            base.Seed(context);
        }

        void seedProblemsData(OjDBContext context){
            var problems = SeedDataRepository.getProblems();

            context.Problems.AddRange(problems);
        }

        // Seed user type data, this is to be included in production
//        void seedUserTypeData(OjDBContext context){
//            context.UserTypes.Add(new UserType(){Name="user"});
//            context.UserTypes.Add(new UserType(){Name="judge"});
//            context.UserTypes.Add(new UserType(){Name="admin"});
//        }

//        void seedUserData(OjDBContext context){
//
//        }
    }
}