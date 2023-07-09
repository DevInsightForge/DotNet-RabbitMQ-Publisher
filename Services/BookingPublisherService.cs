using RabbitMQ.Client;
using System.Text;
using test_dot.Services;

namespace BookingApp.Services
{
    public interface IBookingPublisherService
    {
        Task PublishBooking(string message);
    }
    public class BookingPublisherService: IBookingPublisherService, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;

        public BookingPublisherService(IRabbitMqService rabbitMqService)
        {
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
        }

        public async Task PublishBooking(string message)
        {
           await Task.Run(() => {
                var body = Encoding.UTF8.GetBytes(message);
                _model.BasicPublish(exchange: "booking",
                         routingKey: string.Empty,
                         basicProperties: null,
                         body: body);
                 });

        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
