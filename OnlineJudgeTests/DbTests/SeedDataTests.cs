using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudgeCodeRunner;
using OnlineJudge.Models;
using Xunit;

namespace OnlineJudgeTests.DbTests {
    public class SeedDataTests {
        [Fact]
        public void UserTypesAreSeeded(){
            var ctx = DbContextFactory.getDbContext();
            var enum_values = Enum.GetValues(typeof(UserTypeEnum));

            int enum_count = enum_values.Length;
            int user_type_count = ctx.UserTypes.Count();

            Assert.Equal(enum_count, user_type_count);

            // checked if all enum values are seeded
            foreach (var user_type in enum_values){
                UserType type = ctx.UserTypes.Find(user_type);
                Assert.NotNull(type);
            }
        }

        
        [Fact]
        public void SubmissionStatusAreSeeded(){
            var ctx = DbContextFactory.getDbContext();
            var enum_values = Enum.GetValues(typeof(Verdict));

            int enum_count = enum_values.Length;
            int user_type_count = ctx.SubmissionStatus.Count();

            Assert.Equal(enum_count, user_type_count);

            // checked if all enum values are seeded
            foreach (var user_type in enum_values){
                SubmissionStatus type = ctx.SubmissionStatus.Find(user_type);
                Assert.NotNull(type);
            }
        }


        [Fact]
        public void ProgrammingLanuageAreSeeded(){
            var ctx = DbContextFactory.getDbContext();
            var enum_values = Enum.GetValues(typeof(ProgrammingLanguageEnum));

            int enum_count = enum_values.Length;
            int user_type_count = ctx.ProgrammingLanguages.Count();

            Assert.Equal(enum_count, user_type_count);

            // checked if all enum values are seeded
            foreach (var user_type in enum_values){
                ProgrammingLanguage type = ctx.ProgrammingLanguages.Find(user_type);
                Assert.NotNull(type);
            }
        }

    }
}
