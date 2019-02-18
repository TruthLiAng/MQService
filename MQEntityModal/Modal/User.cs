using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MQEntityModal.Modal
{
    [Serializable]
    public class User
    {
        [Key]
        public Guid guid { get; set; }

        public string Name { get; set; }
        public string Memo { get; set; }
    }
}