using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class BaseModel
    {
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private DateTime _createAt;

        public DateTime CreateAt
        {
            get { return _createAt; }
            set
            {
                _createAt = value == null ? DateTime.UtcNow : value;
            }
        }

        private DateTime dateTime;

        public DateTime UpdateTime
        {
            get { return dateTime; }
            set
            {
                dateTime = value == null ? DateTime.UtcNow : value;
            }
        }
    }

}

