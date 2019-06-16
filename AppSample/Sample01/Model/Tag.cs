using ReactiveUI.Fody.Helpers;
namespace Sample01.Model
{
    public interface ITag
    {
        string Name { get; set; }
    }

    public class IncomingTag : ITag
    {
        [Reactive]
        public string Name { get; set; }
        /// <summary>
        /// 01 : Local  A2700M
        /// 02 : Remote  A2700M
        /// 03 : A2750P
        /// 04 : A2750PC
        /// </summary>
        [Reactive]
        public TagDeviceType DeviceType { get; set; }

        /// <summary>
        /// 01 : Memory         (Only A2700M)
        /// 02 : Remote         (Only Remote)
        /// 03 : DI             (Only A2750P,PC)
        /// 04 : DO             (Only A2750P,PC)
        /// 05 : MOTOR          (Only A2750P,PC)
        /// 06 : Logic Control  (Only A2750P,PC)
        /// 07 : Logic Status   (Only A2750P,PC)
        /// </summary>
        [Reactive]
        public Channel Channel { get; set; }
        /// <summary>
        /// 01 ~ 40
        /// </summary>
        [Reactive]
        public ushort ChannelId { get; set; }
        /// <summary>
        /// 01 : BOOL
        /// 02 : DINT
        /// 03 : UDINT
        /// 04 : REAL
        /// 05 : TIME
        /// </summary>
        [Reactive]
        public ElementaryDataType DataType { get; set; }

        public override string ToString()
        {
            return $"{Name} , {DeviceType}, {Channel}, {ChannelId}, {DataType}";
        }
    }
    public enum TagDeviceType
    {
        A2700M,
        Remote,
        A2750P,
        A2750PC
    }
    public enum Channel
    {
        None,
        Memory,
        Remote,
        DI,
        DO,
        MOTOR,
        LogicControl,
        LogicStatus,
    }

    public enum ElementaryDataType
    {
        None    /**/ = 0x00,
        Bool    /**/ = 0x01,
        Dint    /**/ = 0x02,
        Udint   /**/ = 0x03,
        Real    /**/ = 0x04,
        Time    /**/ = 0x05
    }
}
