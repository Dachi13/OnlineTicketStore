namespace Event.Events.CreateEvent;

public interface IEventRepository
{
    Task<Result<long>> AddEventAsync(CreateEventCommand eventCommand);
}