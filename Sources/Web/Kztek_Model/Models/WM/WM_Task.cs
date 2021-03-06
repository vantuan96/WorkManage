﻿using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Kztek_Model.Models.WM
{
    public class WM_Task
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateStart { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateEnd { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateCreated { get; set; }

        public int Status { get; set; } // 0 - Đang tiến hành, 1 - Hoàn thành, 2 - Tạm dừng

        public string UserCreatedId { get; set; }
    }
}
