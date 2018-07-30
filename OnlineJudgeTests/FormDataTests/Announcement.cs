using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineJudge.FormModels;
using Xunit;

namespace OnlineJudgeTests{
    public class AnnouncementFormDataTests{
        [Fact]
        public void AnnounementFormDataValidation_IsValid_ForValidData(){
            var data = new AnnouncementFormData()
            {
                Title = "Hello there",
                Description = "Testing 1 2 3"
            };


            FormDataValidationResult result = data.Validate();

            // data should be considered valid 
            Assert.True(result.IsValid);

            // no error message should be set
            Assert.Empty(result.ErrorMessages);
        }

        [Fact]
        public void AnnounementFormDataValidation_IsInvalid_ForInvalidData(){
            var data = new AnnouncementFormData()
            {
                Title = "",
                Description = null
            };

            FormDataValidationResult result = data.Validate();

            // data should be considered invalid 
            Assert.False(result.IsValid);

            // two error messages should be set
            Assert.Equal(2, result.ErrorMessages.Count);
        }
    }
}
