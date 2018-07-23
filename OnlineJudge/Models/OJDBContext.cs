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
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Announcement> Announcements { get; set; }


        public DbSet<User> Users { get; set; }
//        public DbSet<UserType> UserTypes { get; set; }
    }

    public class OjDBInitializer :  DropCreateDatabaseAlways<OjDBContext>{
//    public class OjDBInitializer :  DropCreateDatabaseIfModelChanges<OjDBContext>{
        protected override void Seed(OjDBContext context){
            
            // Important: Order of seeding data is important
            // Important: Some seed data rely on other seed data, so context.SaveChanges() has to be called multiple time

            // seed for production
            context.Users.AddRange(SeedDataRepository.getUsers());
            context.SaveChanges();

            // seed only for development
            context.Announcements.AddRange(SeedDataRepository.getAnnouncements());
            context.SaveChanges();

            // seed only for development
            context.Problems.AddRange(SeedDataRepository.getProblems());
            context.SaveChanges();
            

            // this must be at the end of this function
            base.Seed(context);
        }

    }
}