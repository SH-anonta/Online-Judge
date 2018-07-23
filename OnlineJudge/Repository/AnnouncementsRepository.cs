using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineJudge.ResponseModels;

namespace OnlineJudge.Repository {
    public partial class DataRepository {
        // returns the n most recent announcements
        public static void GetRecentAnnouncements(int n = 20){
            //todo implement

        }

        public static List<AnnouncementsResponseData> GetAllAnnouncements(){
            var ctx = getContext();
            return AnnouncementsResponseData.MapTo(ctx.Announcements);
        }
    }
}