using System;
using System.Runtime.InteropServices;
using System.Text;
using Confluent.Kafka;
using DeveloperWepApi1.Config;
using DeveloperWepApi1.Model.Entities;
using DeveloperWepApi1.Model.RequestModels;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;

namespace DeveloperWepApi1.Kafka
{
    public class MyProducerBuilder
    {
        private MyProducerConfig _myProducerConfig;
        
        public MyProducerBuilder(MyProducerConfig myProducerConfig)
        {
            _myProducerConfig = myProducerConfig;

            var kafkaConfig = new ProducerConfig()
            {
                BootstrapServers = _myProducerConfig.KafkaBootStrapServers,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = _myProducerConfig.Username,
                SaslPassword = _myProducerConfig.Password
            };

            using var producer = new ProducerBuilder<string, string>(kafkaConfig).Build();

            var developerDto = new CreateDeveloperDto()
            {
                Name = "Kaydınız başarı ile gerçekleşmiştir. ",
                Surname = "",
                Department = "departmanına j",
            };

            var jstr = JsonConvert.SerializeObject(developerDto);

            var kafkaMessage = new Message<string, string>
            {
                Key = developerDto.Department,
                Value = jstr
            };

            var result =  producer.ProduceAsync(_myProducerConfig.TopicName, kafkaMessage).GetAwaiter().GetResult();

            Console.WriteLine($"Status: {result.Status}");
        }
    }
}










// ProducerBuilder<string,string> builder = new ProducerBuilder<string, string>(ProducerConfig)
//         .SetValueSerializer(new StringSerializer(Encoding.UTF8))
//         .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"));
//
//     using IProducer<string, string> producer = builder.Build();
//
// Message<Null, string> message = new Message<Null, string> { Value = "Hello, Kafka!" };
// DeliveryResult<string, string> result = await producer.ProduceAsync("my-topic", message);
//
//
// Console.WriteLine($"Message sent (value: {message.Value}), status: {result.Status}");
