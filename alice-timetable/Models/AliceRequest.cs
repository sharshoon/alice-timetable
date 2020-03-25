using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alice_Timetable.Models
{
    public class Screen
    {
    }
    public class Interfaces
    {
        public Screen Screen { get; set; }
    }
    public class Meta
    {
        public string Locale { get; set; }
        public string Timezone { get; set; }
        public string ClientId { get; set; }
        public Interfaces Interfaces { get; set; }
    }
    public class Markup
    {
        public bool DangerousContext { get; set; }
    }
    //public class Payload
    //{
    //}
    public class Tokens
    {
        public int Start { get; set; }
        public int End { get; set; }
    }
    public class Entity
    {
        public Tokens Tokens { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
    }
    public class Nlu
    {
        public IList<string> Tokens { get; set; }
        public IList<Entity> Entities { get; set; }
    }
    public class Request
    {
        public string Command { get; set; }
        public string OriginalUtterance { get; set; }
        public string Type { get; set; }
        public Markup Markup { get; set; }
        //public Payload Payload { get; set; }
        public Dictionary<string, string> Payload { get; set; }
        public Nlu Nlu { get; set; }
    }
    public class Session
    {
        public bool New { get; set; }
        public int MessageId { get; set; }
        public string SessionId { get; set; }
        public string SkillId { get; set; }
        public string UserId { get; set; }
    }
    public class AliceRequest
    {
        public Meta Meta { get; set; }
        public Request Request { get; set; }
        public Session Session { get; set; }
        public string Version { get; set; }
    }
}
