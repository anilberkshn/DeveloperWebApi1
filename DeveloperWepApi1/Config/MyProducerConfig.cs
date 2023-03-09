using System.Runtime.InteropServices;

namespace DeveloperWepApi1.Config
{
    public class MyProducerConfig
    {
       public string TopicName { get; set; }
       public string KafkaBootStrapServers { get; set; }
       public string Username { get; set; }
       public string Password { get; set; }

    }
}