// Developed by: Leland Ede
// Created: 2025-01-21
// Updated: 2025-01-21
// Source: https://github.com/lede701/Credal.Net

namespace Credal.Net.Exceptions
{
    public class ConversationException : Exception
    {
        public ConversationException() { }
        public ConversationException(string message) : base(message) { }
        public ConversationException(string message, Exception inner) : base(message, inner) { }
    }
}