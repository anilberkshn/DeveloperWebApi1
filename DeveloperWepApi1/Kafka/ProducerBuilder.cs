using System;
using System.Text;
using Confluent.Kafka;
using MongoDB.Bson.Serialization.Serializers;

namespace DeveloperWepApi1.Kafka
{
    public class ProducerBuilder
    {
        private ProducerConfig _producerConfig;
        
        public ProducerBuilder(ProducerConfig producerConfig)
        {
            _producerConfig = producerConfig;
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

    }
}