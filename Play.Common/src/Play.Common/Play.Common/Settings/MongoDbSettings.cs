﻿namespace Play.Common.Settings
{
    public class MongoDbSettings
    {
        public string Host {  get; init; }
        public int Port { get; init; }

        public string ConnectionsString => $"mongodb://{Host}:{Port}";
    }
}
