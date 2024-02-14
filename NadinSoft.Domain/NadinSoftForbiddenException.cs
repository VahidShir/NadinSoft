namespace NadinSoft.Domain;

public class NadinSoftForbiddenException : NadinSoftBusinessException
{
    public NadinSoftForbiddenException(string message) : base(message)
    {
        
    }
}