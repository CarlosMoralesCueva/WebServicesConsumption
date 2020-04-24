using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    [Serializable]
    public class ServerInfo
    {
        public string name;

        public string slug;

        public string hostname;

        public string region_tag;

        public List<string> locales;

        public List<Service> services;
    }

    [Serializable]
    public class Service
    {
        public string name;

        public string slug;

        public string status;

        public List<Incident> incidents;
    }

    [Serializable]
    public class Incident
    {
        public bool active;

        public string created_at;

        public long id;

        public List<Message> updates;
    }

    [Serializable]
    public class Message
    {
        public string severity;

        public string updated_at;

        public string author;

        public List<Translation> translations;

        public string created_at;

        public string id;

        public string content;
    }

    [Serializable]
    public class Translation
    {
        public string locale;

        public string updated_at;

        public string content;
    }
}