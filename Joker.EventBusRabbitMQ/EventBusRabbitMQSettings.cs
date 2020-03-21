namespace Joker.EventBusRabbitMQ
{
    public class EventBusRabbitMQSettings
    {
        public string EventBusConnection { get; set; }
        public string EventBusPort { get; set; }
        public string EventBusUserName { get; set; }
        public string EventBusPassword { get; set; }
        public string EventBusRetryCount { get; set; }
        public string SubscriptionClientName { get; set; }
    }
}
