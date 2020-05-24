using alice_timetable.Models;
using Alice_Timetable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace alice_timetable.Engine
{
    public class UpdateSchedule : IUpdateSchedule
    {
        private ISchedulesRepository schedulesRepo;
        private static readonly string groupsRequestString = "https://journal.bsuir.by/api/v1/groups";
        private static readonly string groupLastUpdateRequestString = "https://journal.bsuir.by/api/v1/studentGroup/lastUpdateDate?studentGroup=";
        private static readonly string scheduleRequestString = "https://journal.bsuir.by/api/v1/studentGroup/schedule?studentGroup=";
        public UpdateSchedule()
        {
        }
        public void UpdateGroupSchedule(ISchedulesRepository repo)
        {
            schedulesRepo = repo;
            using var client = new HttpClient();
            var response = client.GetAsync(groupsRequestString).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var groups = JsonConvert.DeserializeObject<List<GroupInfo>>(responseString);

                if (groups != null)
                {
                    foreach (var group in groups)
                    {
                        var scheduleLastUpdate = client.GetAsync($"{groupLastUpdateRequestString}{group.name}").Result;
                        var groupFromDb = schedulesRepo.GetSchedule(int.Parse(group.name)).Result;

                        if (scheduleLastUpdate.IsSuccessStatusCode && groupFromDb != null)
                        {
                            var lastUpdate = JsonConvert.DeserializeObject<LastUpdateInfo>(scheduleLastUpdate.Content.ReadAsStringAsync().Result);

                            if (lastUpdate != null && lastUpdate.lastUpdateDate != groupFromDb.lastUpdate)
                            {
                                var schedule = client.GetAsync($"{scheduleRequestString}{group.name}").Result;
                                if (schedule.IsSuccessStatusCode)
                                {
                                    var newSchedule = JsonConvert.DeserializeObject<BsuirScheduleResponse>(
                                                            schedule.Content.ReadAsStringAsync().Result
                                                         );
                                    newSchedule.lastUpdate = lastUpdate.lastUpdateDate;
                                    schedulesRepo.UpdateSchedule(newSchedule.Group, newSchedule);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
