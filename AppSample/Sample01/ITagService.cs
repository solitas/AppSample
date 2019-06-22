using DynamicData;
using Sample01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample01
{
    public interface ITagService
    {
        SourceList<IncomingTag> Tags { get; }
        IObservable<bool> IsValid { get; }

        void LoadTagFromFile(string fileName);
        void SaveTagToFile(string fileName);
    }
}
