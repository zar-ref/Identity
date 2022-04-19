using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DTO.WebSocketModels
{
    public class SocketMessageDTO
    {

        public string MessageType { get; set; }
        public string CustomerType { get; set; } //Needed for ACK
        public string ApplicationContextName { get; set; } //Needed for ACK + Needed for Broadcast
        public string Base64Image { get; set; }
        public string MessageTitle { get; set; }
        public string MessageBody { get; set; }
        [JsonIgnore]
        public string JsonMessage { get; set; }
        public string WriteToJson()
        {
            JsonMessage = JsonConvert.SerializeObject(this);
            return JsonMessage;
        }
    }
}
