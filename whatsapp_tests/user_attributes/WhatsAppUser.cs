using SharpCompress.Common;

namespace whatsapp_tests.user_attributes
{
    public class WhatsAppUser
        // this class is for storing whatever info
        // whatsapp will send serialized in those cURL
        // HttpRequest headers (JSON)
        // and be able to manipulate it at will

    { //getters and setters as per C# costumes
        public string @object { get; set; }
        public Entry[] entry { get; set; }

        public class Entry
        {
            public Change[] changes { get; set; }
        }

        public class Change
        {
            public Value value { get; set; }
        }

        public class Value
        {
           public Message[] messages { get; set; }
        }

        public class  Text
        {
            public string body { get; set; }
        }

        public class Message
        {
            public string from { get; set; }
            public string id { get; set; }
            public string timestamp { get; set; }
            public Text text { get; set; }

        }
    }
}
