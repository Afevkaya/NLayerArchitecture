namespace NLayerArchitecture.Services.ExceptionHandlers;

public class CriticalException(string message) : Exception(message);