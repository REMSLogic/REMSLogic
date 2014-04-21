﻿using System;

namespace RemsLogic.Model
{
    public class PrescriberEoc : Entity
    {
        public long PrescriberProfileId {get; set;}
        public long DrugId {get; set;}
        public long EocId {get; set;}
        public DateTime? CompletedAt {get; set;}
        public bool Deleted {get; set;}
    }
}