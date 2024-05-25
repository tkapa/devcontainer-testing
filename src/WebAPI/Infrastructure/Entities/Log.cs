namespace WebAPI.Infrastructure.Entities;

public record Log (
    string Id,
    string Message,
    string Level,
    string Timestamp
    );