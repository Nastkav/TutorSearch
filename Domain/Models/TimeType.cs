namespace Domain.Models;

public enum TimeType
{
    Available, // only from Tutor settings
    Busy, // have another work
    Unavailable //  Tutor dont work on this time
}