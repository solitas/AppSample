using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample01
{
    public class TagViewModel : ReactiveObject
    {
        private ObservableCollection<A2750Device> _device;
        public ObservableCollection<A2750Device> Devices
        {
            get => _device;
            set => this.RaiseAndSetIfChanged(ref _device, value);
        }

        public TagViewModel()
        {
            Devices = new ObservableCollection<A2750Device>();
            
            for (var i = 0; i < 10; i++)
            {
                Devices.Add(new A2750Device(i));
            }
        }
    }

    public class A2750Device
    {
        public int Id { get; }
        public string Name { get; }
        public DIO[] DI { get; }
        public DIO[] DO { get; }
        public A2750Device(int id)
        {
            Id = id;
            Name = $"Unit-{id:D2}";

            DI = new DIO[]
            {
                new DIO(DIO.DIOType.DI, 1),
                new DIO(DIO.DIOType.DI, 2),
                new DIO(DIO.DIOType.DI, 3),
                new DIO(DIO.DIOType.DI, 4),
                new DIO(DIO.DIOType.DI, 5),
                new DIO(DIO.DIOType.DI, 6),
                new DIO(DIO.DIOType.DI, 7),
                new DIO(DIO.DIOType.DI, 8),
                new DIO(DIO.DIOType.DI, 9),
                new DIO(DIO.DIOType.DI, 10),
            };

            DO = new DIO[]
            {
                new DIO(DIO.DIOType.DO, 1),
                new DIO(DIO.DIOType.DO, 2),
                new DIO(DIO.DIOType.DO, 3),
                new DIO(DIO.DIOType.DO, 4),
            };
        }
    }

    public class DIO
    {
        public enum DIOType
        {
            DI,
            DO
        }
        public DIOType Type { get; }
        public int Channel { get; }
        public string Label { get; }

        public DIO(DIOType ioType, int channel)
        {
            Type = ioType;
            Channel = channel;
            Label =$"{Type}{Channel:D2}";
        }
    }
}
