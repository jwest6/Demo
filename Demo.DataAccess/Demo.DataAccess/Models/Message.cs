using System.ComponentModel.DataAnnotations;

namespace Demo.DataAccess.Models
{
    public class Message
    {
        public Message() : this(MessageType.HelloWorld) { }

        public Message(MessageType type)
        {

        }
        public int Id { get; set; }


        [Required]
        public string MessageValue { get; set; }

        [Required]
        public MessageType MessageType { get; set; }
    }

    public enum MessageType
    {
        HelloWorld,
        Urgent
    }
}
