using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Models
{
    //public class Payload
    //{
    //}

    public class Button
    {
        public string Title { get; set; }
        //public Payload Payload { get; set; }
        public Dictionary<string, string> Payload { get; set; }
        public string Url { get; set; }
        public bool Hide { get; set; }
    }

    public class Response
    {
        public string Text { get; set; }
        public string Tts { get; set; }
        public IList<Button> Buttons { get; set; }
        public bool EndSession { get; set; }
    }

    //public class Session
    //{
    //    public string SessionId { get; set; }
    //    public int MessageId { get; set; }
    //    public string UserId { get; set; }
    //}

    public class AliceResponse
    {
        public Response Response { get; set; } = new Response();
        public Session Session { get; set; }
        public string Version { get; set; }

        public AliceResponse(AliceRequest request)
        {
            Session = request.Session;
        }
    }


}
