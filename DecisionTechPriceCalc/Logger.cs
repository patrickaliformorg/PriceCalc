namespace DecisionTechPriceCalc
{
    public interface ILog
    {
        void LogMessage(string message);// Include Alert Level etc
        // Read implementation would take a correlationId and a alert/warning level
        // void LogMessage(string message, AlertLevel level, Guid correlationId);
    }
    public class Logger : ILog
    {
        public void LogMessage(string message)
        {
            // Implement....
        }
    }


}
