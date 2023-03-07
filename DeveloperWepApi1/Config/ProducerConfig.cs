using System.Runtime.InteropServices;

namespace DeveloperWepApi1.Config
{
    public class ProducerConfig
    {
       public string topicName { get; set; }
       public string kafkaBootStrapServers { get; set; }
       public string username { get; set; }
       public string password { get; set; }

    }
}